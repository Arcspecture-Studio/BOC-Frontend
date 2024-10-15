using UnityEngine;
using WebSocketSharp;
using Newtonsoft.Json;
using System.Collections;

public class WebsocketSystem : MonoBehaviour
{
    WebsocketComponent websocketComponent;
    WebrequestComponent webrequestComponent;
    PromptComponent promptComponent;
    IoComponent ioComponent;
    ExitComponent exitComponent;
    MiniPromptComponent miniPromptComponent;

    WebSocket generalSocket;
    string logPrefix = "[WebsocketSystem] ";

    void Start()
    {
        websocketComponent = GlobalComponent.instance.websocketComponent;
        webrequestComponent = GlobalComponent.instance.webrequestComponent;
        promptComponent = GlobalComponent.instance.promptComponent;
        ioComponent = GlobalComponent.instance.ioComponent;
        exitComponent = GlobalComponent.instance.exitComponent;
        miniPromptComponent = GlobalComponent.instance.miniPromptComponent;

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
            if (websocketComponent.envData.enableLog) Debug.Log(logPrefix + "Connection open: " + ((WebSocket)sender).Url);

            UnityMainThread.AddJob(() =>
            {
                miniPromptComponent.message = PromptConstant.CONNECTED;
            });
        };
        generalSocket.OnMessage += (sender, e) =>
        {
            string rawData = e.Data;
            if (websocketComponent.envData.enableEncryption && !Utils.IsJsonString(e.Data))
                rawData = Encryption.Decrypt(e.Data, EncryptionConfig.ENCRYPTION_ACCESS_TOKEN_32, websocketComponent.generalSocketIv);
            if (websocketComponent.envData.enableLog) Debug.Log(logPrefix + "Incoming message from: " + ((WebSocket)sender).Url + ", Data: " + rawData);

            General.WebsocketGeneralResponse response = JsonConvert.DeserializeObject<General.WebsocketGeneralResponse>(rawData, JsonSerializerConfig.settings);

            #region Validate authority
            if (!response.success && !response.message.IsNullOrEmpty() && response.message.Equals(PromptConstant.NOT_AUTHORIZED))
            {
                UnityMainThread.AddJob(() =>
                {
                    promptComponent.ShowPrompt(PromptConstant.ERROR, response.message, () =>
                    {
                        promptComponent.active = false;
                        ioComponent.deleteToken = true;
                        exitComponent.exit = true;
                    });
                });
                return;
            }
            #endregion

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
                        promptComponent.ShowPrompt(PromptConstant.NOTICE, response.message, () =>
                        {
                            exitComponent.exit = true;
                        });
                    });
                }
            }
            else if (response.eventType == WebsocketEventTypeEnum.CALL_API)
            {
                General.WebsocketCallApiResponse callApiResponse = JsonConvert.DeserializeObject<General.WebsocketCallApiResponse>(rawData, JsonSerializerConfig.settings);
                WebrequestStatusEnum status = WebrequestStatusEnum.RECEIVED;
                if (callApiResponse.rejectByServer.HasValue)
                {
                    if (callApiResponse.rejectByServer.Value)
                    {
                        status = WebrequestStatusEnum.SERVER_RETURNED_ERROR;
                    }
                    else
                    {
                        status = WebrequestStatusEnum.HTTP_ERROR;
                    }
                }
                webrequestComponent.rawResponses.Add(callApiResponse.id, new Response(callApiResponse.id, status, callApiResponse.responseJsonString));
            }
            else
            {
                websocketComponent.AddGeneralResponses(response.eventType, rawData);
            }
        };
        generalSocket.OnError += (sender, e) =>
        {
            if (websocketComponent.envData.enableLog) Debug.LogError(logPrefix + "Error at: " + ((WebSocket)sender).Url + ", Message: " + e.Message + ", Exception: " + e.Exception);
        };
        generalSocket.OnClose += (sender, e) =>
        {
            if (websocketComponent.envData.enableLog) Debug.Log(logPrefix + "Connection closed at: " + ((WebSocket)sender).Url + ", Code: " + e.Code + ", WasClean: " + e.WasClean + ", Reason: " + e.Reason);
            if (!e.WasClean) websocketComponent.connectGeneralSocket = true;

            UnityMainThread.AddJob(() =>
            {
                miniPromptComponent.message = PromptConstant.DISCONNECTED;
            });
        };
    }
    void ProcessConnect()
    {
        if (websocketComponent.connectGeneralSocket)
        {
            websocketComponent.connectGeneralSocket = false;
            generalSocket = new WebSocket(websocketComponent.envData.host + ":" + websocketComponent.envData.port);
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
        websocketComponent.connectedGeneralSocket && websocketComponent.generalSocketIv.Length == EncryptionConfig.IV_LENGTH);
        if (websocketComponent.generalRequests.Count == 0) yield break;

        websocketComponent.generalRequests.ForEach(request =>
        {
            Send(request, generalSocket, websocketComponent.envData.enableEncryption);
        });
        websocketComponent.generalRequests.Clear();
    }
    void Send(object request, WebSocket socket, bool encrypt = false)
    {
        string jsonStr = JsonConvert.SerializeObject(request, JsonSerializerConfig.settings);
        if (websocketComponent.envData.enableLog) Debug.Log(logPrefix + "Send message: " + jsonStr);
        if (encrypt) jsonStr = Encryption.Encrypt(jsonStr, EncryptionConfig.ENCRYPTION_ACCESS_TOKEN_32, websocketComponent.generalSocketIv);
        socket.Send(jsonStr);
    }
}
