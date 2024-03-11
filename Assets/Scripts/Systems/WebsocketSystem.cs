using UnityEngine;
using WebSocketSharp;
using Newtonsoft.Json;

public class WebsocketSystem : MonoBehaviour
{
    WebsocketComponent websocketComponent;
    WebrequestComponent webrequestComponent;
    IoComponent ioComponent;
    LoginComponentOld loginComponent;
    PromptComponent promptComponent;

    WebSocket generalSocket;
    string logPrefix = "[WebsocketSystem] ";

    void Start()
    {
        websocketComponent = GlobalComponent.instance.websocketComponent;
        webrequestComponent = GlobalComponent.instance.webrequestComponent;
        ioComponent = GlobalComponent.instance.ioComponent;
        loginComponent = GlobalComponent.instance.loginComponentOld;
        promptComponent = GlobalComponent.instance.promptComponent;

        websocketComponent.connectGeneralSocket = true;
    }
    void Update()
    {
        ProcessConnect();
        ProcessGeneralRequests();
    }
    void OnApplicationQuit()
    {
        if (generalSocket != null && generalSocket.IsAlive) generalSocket.Close();
    }

    void ListenGeneralSocket()
    {
        generalSocket.OnOpen += (sender, e) =>
        {
            if (websocketComponent.logging) Debug.Log(logPrefix + "Connection open: " + ((WebSocket)sender).Url);
        };
        generalSocket.OnMessage += (sender, e) =>
        {
            if (websocketComponent.logging) Debug.Log(logPrefix + "Incoming message from: " + ((WebSocket)sender).Url + ", Data: " + e.Data);

            General.WebsocketGeneralResponse response = JsonConvert.DeserializeObject<General.WebsocketGeneralResponse>(e.Data, JsonSerializerConfig.settings);
            if (response.eventType.Equals(WebsocketEventTypeEnum.CONNECTION_ID.ToString()))
            {
                General.WebsocketConnectionEstablishedResponse connectionEstablishedResponse = JsonConvert.DeserializeObject<General.WebsocketConnectionEstablishedResponse>(e.Data, JsonSerializerConfig.settings);
                websocketComponent.generalSocketIv = connectionEstablishedResponse.iv.data;

                // TODO: change to account mechanism
                if (loginComponent.loggedIn)
                {
                    // ioComponent.writeApiKey = true;
                    websocketComponent.syncApiKeyToServer = true;
                    // retrieveOrdersComponent.updateOrderStatus = true;
                }
            }
            else if (response.eventType.Equals(WebsocketEventTypeEnum.VERSION_CHECKING.ToString()))
            {
                // PENDING: now is when received this eventType VERSION_CHECKING straight means outdated, later need to check the body if the version is matching by {valid: true}
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
        if (websocketComponent.connectGeneralSocket)
        {
            websocketComponent.connectGeneralSocket = false;
#if UNITY_EDITOR
            string host = websocketComponent.localhost ? WebsocketConfig.GENERAL_HOST_LOCAL : WebsocketConfig.GENERAL_HOST;
#else
            string host = WebsocketConfig.GENERAL_HOST;
#endif
            generalSocket = new WebSocket(host + ":" + (websocketComponent.productionPort ? WebsocketConfig.GENERAL_PORT_PRODUCTION : WebsocketConfig.GENERAL_PORT));
            //if (!websocketComponent.localhost) generalSocket.SslConfiguration.EnabledSslProtocols = websocketComponent.sslProtocols;
            ListenGeneralSocket();
            websocketComponent.generalSocket = generalSocket;
            if (!generalSocket.IsAlive) generalSocket.ConnectAsync();
        }
    }
    void ProcessGeneralRequests()
    {
        if (websocketComponent.generalRequests.Count == 0) return;
        if (!websocketComponent.connectedGeneralSocket) return;
        if (websocketComponent.generalSocketIv == null) return;
        websocketComponent.generalRequests.ForEach(request =>
        {
            Send(request, generalSocket, true);
        });
        websocketComponent.generalRequests.Clear();
    }
    void Send(object request, WebSocket socket, bool encrypt = false)
    {
        string jsonStr = JsonConvert.SerializeObject(request, JsonSerializerConfig.settings);
        if (websocketComponent.logging) Debug.Log(logPrefix + "Send message: " + jsonStr);
        if (encrypt) jsonStr = Encryption.Encrypt(jsonStr, SecretConfig.ENCRYPTION_ACCESS_TOKEN_32, websocketComponent.generalSocketIv);
        socket.Send(jsonStr);
    }
}
