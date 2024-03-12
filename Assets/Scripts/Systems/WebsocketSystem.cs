using UnityEngine;
using WebSocketSharp;
using Newtonsoft.Json;
using System.Collections;

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
        StartCoroutine(ProcessGeneralRequests());
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
            string rawData = e.Data;
            if (!Utils.IsJson(e.Data)) rawData = Encryption.Decrypt(e.Data, SecretConfig.ENCRYPTION_ACCESS_TOKEN_32, websocketComponent.generalSocketIv);
            if (websocketComponent.logging) Debug.Log(logPrefix + "Incoming message from: " + ((WebSocket)sender).Url + ", Data: " + rawData);

            General.WebsocketGeneralResponse response = JsonConvert.DeserializeObject<General.WebsocketGeneralResponse>(rawData, JsonSerializerConfig.settings);
            if (response.eventType == WebsocketEventTypeEnum.CONNECTION_ESTABLISH)
            {
                General.WebsocketConnectionEstablishedResponse connectionEstablishedResponse = JsonConvert.DeserializeObject
                <General.WebsocketConnectionEstablishedResponse>(rawData, JsonSerializerConfig.settings);
                websocketComponent.generalSocketIv = connectionEstablishedResponse.iv.data;
            }
            else if (response.eventType == WebsocketEventTypeEnum.VERSION_CHECKING)
            {
                if (!response.success)
                {
                    UnityMainThread.AddJob(() =>
                    {
                        promptComponent.ShowPrompt("NOTICE", response.message, () =>
                        {
#if UNITY_EDITOR
                            UnityEditor.EditorApplication.isPlaying = false;
#else
                            Application.Quit();
#endif
                        });
                    });
                }
            }

            //             else if (response.eventType == WebsocketEventTypeEnum.CALL_API)
            //             {
            //                 General.WebsocketCallApiResponse callApiResponse = JsonConvert.DeserializeObject<General.WebsocketCallApiResponse>(rawData, JsonSerializerConfig.settings);
            //                 string logStatus = "Received";
            //                 if (callApiResponse.rejectByServer.HasValue)
            //                 {
            //                     if (callApiResponse.rejectByServer.Value)
            //                     {
            //                         logStatus = "Server returned error";
            //                     }
            //                     else
            //                     {
            //                         logStatus = "HTTP Error";
            //                     }
            //                 }
            //                 if (callApiResponse.id.IsNullOrEmpty()) callApiResponse.id = "";
            //                 webrequestComponent.rawResponses.Add(callApiResponse.id, new Response(callApiResponse.id, logStatus, callApiResponse.responseJsonString));
            //             }
            //             else if (response.eventType == WebsocketEventTypeEnum.ACCOUNT_OVERWRITE)
            //             {
            //                 UnityMainThread.AddJob(() =>
            //                 {
            //                     promptComponent.ShowPrompt("NOTICE", "Your account has been logged in from other device.", () =>
            //                     {
            // #if UNITY_EDITOR
            //                         UnityEditor.EditorApplication.isPlaying = false;
            // #else
            //                         Application.Quit();
            // #endif
            //                     });
            //                 });
            //             }
            //             else if (response.eventType == WebsocketEventTypeEnum.INVALID_LOGIN_PHRASE)
            //             {
            //                 UnityMainThread.AddJob(() =>
            //                 {
            //                     string message = "Login failed with invalid personal secret login phrase, please try to login again.";
            //                     loginComponent.allowInput = true;
            //                     promptComponent.ShowPrompt("ERROR", message, () =>
            //                     {
            //                         promptComponent.active = false;
            //                     });
            //                 });
            //             }
            //             else
            //             {
            //                 websocketComponent.AddGeneralResponses(response.eventType, rawData);
            //             }
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
    IEnumerator ProcessGeneralRequests()
    {
        if (websocketComponent.generalRequests.Count == 0) yield break;
        yield return new WaitUntil(() =>
        websocketComponent.connectedGeneralSocket && websocketComponent.generalSocketIv != null);
        if (websocketComponent.generalRequests.Count == 0) yield break;

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
