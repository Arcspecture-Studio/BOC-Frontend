using UnityEngine;
using WebSocketSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Collections.Generic;

public class WebsocketSystem : MonoBehaviour
{
    PlatformComponent platformComponent;
    WebsocketComponent websocketComponent;
    WebrequestComponent webrequestComponent;
    BinanceComponent binanceComponent;
    BinanceComponent binanceTestnetComponent;
    IoComponent ioComponent;
    LoginComponent loginComponent;
    PromptComponent promptComponent;
    RetrieveOrdersComponent retrieveOrdersComponent;

    WebSocket marketSocket;
    WebSocket userDataSocket;
    WebSocket generalSocket;
    string logPrefix = "[WebsocketSystem] ";

    void Start()
    {
        platformComponent = GlobalComponent.instance.platformComponent;
        websocketComponent = GlobalComponent.instance.websocketComponent;
        webrequestComponent = GlobalComponent.instance.webrequestComponent;
        binanceComponent = GlobalComponent.instance.binanceComponent;
        binanceTestnetComponent = GlobalComponent.instance.binanceTestnetComponent;
        ioComponent = GlobalComponent.instance.ioComponent;
        loginComponent = GlobalComponent.instance.loginComponent;
        promptComponent = GlobalComponent.instance.promptComponent;
        retrieveOrdersComponent = GlobalComponent.instance.retrieveOrdersComponent;

        websocketComponent.connectGeneralSocket = true;
    }
    void Update()
    {
        ProcessConnect();
        ProcessMarketRequests();
        ProcessGeneralRequests();
    }
    void OnApplicationQuit()
    {
        if (marketSocket != null && marketSocket.IsAlive) marketSocket.Close();
        if (userDataSocket != null && userDataSocket.IsAlive) userDataSocket.Close();
        if (generalSocket != null && generalSocket.IsAlive) generalSocket.Close();
    }

    void ListenMarketSocket()
    {
        marketSocket.OnOpen += (sender, e) =>
        {
            if (websocketComponent.logging) Debug.Log(logPrefix + "Connection open: " + ((WebSocket)sender).Url);
        };
        marketSocket.OnMessage += (sender, e) =>
        {
            if (websocketComponent.logging) Debug.Log(logPrefix + "Message received from: " + ((WebSocket)sender).Url + ", Data: " + e.Data);

            Binance.WebsocketMarketResponse response = JsonConvert.DeserializeObject<Binance.WebsocketMarketResponse>(e.Data, JsonSerializerConfig.settings);
            if (response.stream != null)
            {
                if (websocketComponent.marketResponses.ContainsKey(response.stream)){
                    websocketComponent.marketResponses[response.stream].Add(e.Data);
                }
                else
                {
                    List<string> datas = new() { e.Data };
                    websocketComponent.marketResponses.Add(response.stream, datas);
                }
            }
        };
        marketSocket.OnError += (sender, e) =>
        {
            if (websocketComponent.logging) Debug.LogError(logPrefix + "Error at: " + ((WebSocket)sender).Url + ", Message: " + e.Message + ", Exception: " + e.Exception);
        };
        marketSocket.OnClose += (sender, e) =>
        {
            if (websocketComponent.logging) Debug.Log(logPrefix + "Connection closed at: " + ((WebSocket)sender).Url + ", Code: " + e.Code + ", WasClean: " + e.WasClean + ", Reason: " + e.Reason);
            if (!e.WasClean) websocketComponent.connectMarketSocket = true;
        };
    }
    void ListenUserDataSocket()
    {
        userDataSocket.OnOpen += (sender, e) =>
        {
            if (websocketComponent.logging) Debug.Log(logPrefix + "Connection open: " + ((WebSocket)sender).Url);
        };
        userDataSocket.OnMessage += (sender, e) =>
        {
            if (websocketComponent.logging) Debug.Log(logPrefix + "Message received from: " + ((WebSocket)sender).Url + ", Data: " + e.Data);

            Binance.WebsocketUserDataResponse response = JsonConvert.DeserializeObject<Binance.WebsocketUserDataResponse>(e.Data, JsonSerializerConfig.settings);
            if (websocketComponent.userDataResponses.ContainsKey(response.eventType))
            {
                websocketComponent.userDataResponses[response.eventType].Add(e.Data);
            }
            else
            {
                List<string> datas = new() { e.Data };
                websocketComponent.userDataResponses.Add(response.eventType, datas);
            }
        };
        userDataSocket.OnError += (sender, e) =>
        {
            if (websocketComponent.logging) Debug.LogError(logPrefix + "Error at: " + ((WebSocket)sender).Url + ", Message: " + e.Message + ", Exception: " + e.Exception);
        };
        userDataSocket.OnClose += (sender, e) =>
        {
            if (websocketComponent.logging) Debug.Log(logPrefix + "Connection closed at: " + ((WebSocket)sender).Url + ", Code: " + e.Code + ", WasClean: " + e.WasClean + ", Reason: " + e.Reason);
            if (!e.WasClean) websocketComponent.connectUserDataSocket = true;
        };
    }
    void ListenGeneralSocket()
    {
        generalSocket.OnOpen += (sender, e) =>
        {
            if (websocketComponent.logging) Debug.Log(logPrefix + "Connection open: " + ((WebSocket)sender).Url);
        };
        generalSocket.OnMessage += (sender, e) =>
        {
            if (websocketComponent.logging) Debug.Log(logPrefix + "Message received from: " + ((WebSocket)sender).Url + ", Data: " + e.Data);

            General.WebsocketGeneralResponse response = JsonConvert.DeserializeObject<General.WebsocketGeneralResponse>(e.Data, JsonSerializerConfig.settings);
            if (response.eventType.Equals(WebsocketEventTypeEnum.CONNECTION_ID.ToString()))
            {
                General.WebsocketConnectionEstablishedResponse connectionEstablishedResponse = JsonConvert.DeserializeObject<General.WebsocketConnectionEstablishedResponse>(e.Data, JsonSerializerConfig.settings);
                websocketComponent.generalSocketConnectionId = connectionEstablishedResponse.connectionId;
                websocketComponent.generalSocketIv = connectionEstablishedResponse.iv.data;
                if (loginComponent.loggedIn)
                {
                    ioComponent.writeApiKey = true;
                    websocketComponent.syncApiKeyToServer = true;
                    retrieveOrdersComponent.updateOrderStatus = true;
                }
            }
            else if (response.eventType.Equals(WebsocketEventTypeEnum.CALL_API.ToString()))
            {
                General.WebsocketCallApiResponse callApiResponse = JsonConvert.DeserializeObject<General.WebsocketCallApiResponse>(e.Data, JsonSerializerConfig.settings);
                string logStatus = "Received";
                if (callApiResponse.rejectByServer.HasValue)
                {
                    if (callApiResponse.rejectByServer.Value)
                    {
                        logStatus = "Server returned error";
                    }
                    else
                    {
                        logStatus = "HTTP Error";
                    }
                }
                if (callApiResponse.id.IsNullOrEmpty()) callApiResponse.id = "";
                webrequestComponent.rawResponses.Add(callApiResponse.id, new Response(callApiResponse.id, logStatus, callApiResponse.responseJsonString));
            }
            else if (response.eventType.Equals(WebsocketEventTypeEnum.ACCOUNT_OVERWRITE.ToString()))
            {
                UnityMainThread.AddJob(() =>
                {
                    promptComponent.ShowPrompt("NOTICE", "Your account has been logged in from other device.", () =>
                    {
#if UNITY_EDITOR
                        UnityEditor.EditorApplication.isPlaying = false;
#else
                        Application.Quit();
#endif
                    });
                });
            }
            else if (response.eventType.Equals(WebsocketEventTypeEnum.INVALID_LOGIN_PHRASE.ToString()))
            {
                UnityMainThread.AddJob(() =>
                {
                    string message = "Login failed with invalid personal secret login phrase, please try to login again.";
                    loginComponent.allowInput = true;
                    promptComponent.ShowPrompt("ERROR", message, () =>
                    {
                        promptComponent.active = false;
                    });
                });
            }
            else if (response.eventType.Equals(WebsocketEventTypeEnum.VERSION_MISMATCH.ToString())){
                UnityMainThread.AddJob(() =>
                {
                    promptComponent.ShowPrompt("NOTICE", "App version is outdated, please update your app to latest version.", () =>
                    {
#if UNITY_EDITOR
                        UnityEditor.EditorApplication.isPlaying = false;
#else
                        Application.Quit();
#endif
                    });
                });
            }
            else
            {
                websocketComponent.AddGeneralResponses(response.eventType, e.Data);
            }
        };
        generalSocket.OnError += (sender, e) =>
        {
            if (websocketComponent.logging) Debug.LogError(logPrefix + "Error at: " + ((WebSocket)sender).Url + ", Message: " + e.Message + ", Exception: " + e.Exception);
        };
        generalSocket.OnClose += (sender, e) =>
        {
            if (websocketComponent.logging) Debug.Log(logPrefix + "Connection closed at: " + ((WebSocket)sender).Url + ", Code: " + e.Code + ", WasClean: " + e.WasClean + ", Reason: " + e.Reason);
            if (!e.WasClean) websocketComponent.connectGeneralSocket = true;
        };
    }
    void ProcessConnect()
    {
        if (websocketComponent.connectMarketSocket)
        {
            websocketComponent.connectMarketSocket = false;
            switch (platformComponent.tradingPlatform)
            {
                case PlatformEnum.BINANCE:
                    marketSocket = new WebSocket(WebsocketConfig.BINANCE_HOST + WebsocketConfig.BINANCE_MARKET_PATH);
                    break;
                case PlatformEnum.BINANCE_TESTNET:
                    marketSocket = new WebSocket(WebsocketConfig.BINANCE_HOST_TEST + WebsocketConfig.BINANCE_MARKET_PATH);
                    break;
                case PlatformEnum.MEXC:
                    break;
            }
            marketSocket.SslConfiguration.EnabledSslProtocols = WebsocketConfig.SSL_PROTOCOLS;
            ListenMarketSocket();
            websocketComponent.marketSocket = marketSocket;
            if (!marketSocket.IsAlive) marketSocket.ConnectAsync();
        }
        if (websocketComponent.connectUserDataSocket)
        {
            websocketComponent.connectUserDataSocket = false;
            switch (platformComponent.tradingPlatform)
            {
                case PlatformEnum.BINANCE:
                    userDataSocket = new WebSocket(WebsocketConfig.BINANCE_HOST + WebsocketConfig.BINANCE_USER_DATA_PATH + binanceComponent.listenKey);
                    break;
                case PlatformEnum.BINANCE_TESTNET:
                    userDataSocket = new WebSocket(WebsocketConfig.BINANCE_HOST_TEST + WebsocketConfig.BINANCE_USER_DATA_PATH + binanceTestnetComponent.listenKey);
                    break;
                case PlatformEnum.MEXC:
                    break;
            }
            userDataSocket.SslConfiguration.EnabledSslProtocols = WebsocketConfig.SSL_PROTOCOLS;
            ListenUserDataSocket();
            websocketComponent.userDataSocket = userDataSocket;
            if (!userDataSocket.IsAlive) userDataSocket.ConnectAsync();
        }
        if (websocketComponent.connectGeneralSocket)
        {
            websocketComponent.connectGeneralSocket = false;
#if UNITY_EDITOR
            string host = websocketComponent.localhost ? WebsocketConfig.GENERAL_HOST_LOCAL : WebsocketConfig.GENERAL_HOST;
#else
            string host = WebsocketConfig.GENERAL_HOST;
#endif
            generalSocket = new WebSocket(host + ":" + (websocketComponent.productionPort ? WebsocketConfig.GENERAL_PORT_PRODUCTION : WebsocketConfig.GENERAL_PORT));
            //if (!websocketComponent.localhost) generalSocket.SslConfiguration.EnabledSslProtocols = WebsocketConfig.sslProtocols;
            ListenGeneralSocket();
            websocketComponent.generalSocket = generalSocket;
            if (!generalSocket.IsAlive) generalSocket.ConnectAsync();
        }
    }
    void ProcessMarketRequests()
    {
        if (websocketComponent.marketRequests.Count == 0) return;
        if (!marketSocket.IsAlive) return;
        websocketComponent.marketRequests.ForEach(request =>
        {
            Send(request, marketSocket);
        });
        websocketComponent.marketRequests.Clear();
    }
    void ProcessGeneralRequests()
    {
        if (websocketComponent.generalRequests.Count == 0) return;
        if (!websocketComponent.connectedGeneralSocket) return;
        if (websocketComponent.generalSocketConnectionId.IsNullOrEmpty() || websocketComponent.generalSocketIv == null) return;
        websocketComponent.generalRequests.ForEach(request =>
        {
            Send(request, generalSocket, true);
        });
        websocketComponent.generalRequests.Clear();
    }
    void Send(object request, WebSocket socket, bool encrypt = false)
    {
        string jsonStr = JsonConvert.SerializeObject(request, JsonSerializerConfig.settings);
        if (websocketComponent.logging) Debug.Log(logPrefix + "Websocket Request: " + jsonStr);
        if (encrypt) jsonStr = Encryption.Encrypt(jsonStr, websocketComponent.generalSocketConnectionId, websocketComponent.generalSocketIv);
        socket.Send(jsonStr);
    }
}
