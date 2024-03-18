#pragma warning disable CS0162
#pragma warning disable CS8632
#pragma warning disable CS0168

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using WebSocketSharp;

public class WebrequestSystem : MonoBehaviour
{
    WebrequestComponent webrequestComponent;
    WebsocketComponent websocketComponent;
    PlatformComponentOld platformComponentOld;
    PromptComponent promptComponent;
    string logPrefix = "[WebrequestSystem] ";

    void Start()
    {
        webrequestComponent = GlobalComponent.instance.webrequestComponent;
        websocketComponent = GlobalComponent.instance.websocketComponent;
        platformComponentOld = GlobalComponent.instance.platformComponentOld;
        promptComponent = GlobalComponent.instance.promptComponent;

        // TODO:
    }
    void Update()
    {
        ProcessRequests();
        ListenForIncomingResponse();
    }

    IEnumerator GetRequest(Request request)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(request.uri))
        {
            SetHeader(webRequest);
            yield return webRequest.SendWebRequest();
            ResponseHandler(webRequest, request.id);
        }
    }
    IEnumerator PostRequest(Request request)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Post(request.uri, ""))
        {
            SetHeader(webRequest);
            yield return webRequest.SendWebRequest();
            ResponseHandler(webRequest, request.id);
        }
    }
    IEnumerator PutRequest(Request request)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Put(request.uri, ""))
        {
            SetHeader(webRequest);
            yield return webRequest.SendWebRequest();
            ResponseHandler(webRequest, request.id);
        }
    }
    IEnumerator DeleteRequest(Request request)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Delete(request.uri))
        {
            webRequest.downloadHandler = new DownloadHandlerBuffer();
            SetHeader(webRequest);
            yield return webRequest.SendWebRequest();
            ResponseHandler(webRequest, request.id);
        }
    }
    void SetHeader(UnityWebRequest webRequest)
    {
        webRequest.SetRequestHeader("Content-Type", "application/json");
        // webRequest.SetRequestHeader("X-MBX-APIKEY", platformComponentOld.apiKey);
    }
    void ResponseHandler(UnityWebRequest webRequest, string id)
    {
        string logStatus = "Received";
        switch (webRequest.result)
        {
            case UnityWebRequest.Result.ConnectionError:
            case UnityWebRequest.Result.DataProcessingError:
                logStatus = "Error";
                break;
            case UnityWebRequest.Result.ProtocolError:
                logStatus = "HTTP Error";
                break;
        }
        webrequestComponent.rawResponses.Add(id, new Response(id, logStatus, webRequest.downloadHandler.text));
    }
    void ProcessRequests()
    {
        if (webrequestComponent.requests.Count == 0) return;
        webrequestComponent.requests.ForEach(request =>
        {
            if (webrequestComponent.processRequestAtServer)
            {
                General.WebsocketCallApiRequest callApiRequest = new General.WebsocketCallApiRequest(request);
                websocketComponent.generalRequests.Add(callApiRequest);
            }
            else
            {
                switch (request.requestType)
                {
                    case WebrequestRequestTypeEnum.GET:
                        StartCoroutine(GetRequest(request));
                        break;
                    case WebrequestRequestTypeEnum.POST:
                        StartCoroutine(PostRequest(request));
                        break;
                    case WebrequestRequestTypeEnum.PUT:
                        StartCoroutine(PutRequest(request));
                        break;
                    case WebrequestRequestTypeEnum.DELETE:
                        StartCoroutine(DeleteRequest(request));
                        break;
                }
            }
        });
        webrequestComponent.requests.Clear();
    }
    void ListenForIncomingResponse()
    {
        if (webrequestComponent.rawResponses.Count == 0) return;
        Dictionary<string, Response> rawResponses = new Dictionary<string, Response>(webrequestComponent.rawResponses);
        foreach (KeyValuePair<string, Response> data in rawResponses)
        {
            Response rawResponse = data.Value;
            if (webrequestComponent.logging) Debug.Log(logPrefix + "Incoming message on Id: " + rawResponse.id + ", " + rawResponse.logStatus + ": " + rawResponse.responseJsonString);
            if (!rawResponse.id.IsNullOrEmpty())
            {
                if (!webrequestComponent.responses.TryAdd(rawResponse.id, rawResponse.responseJsonString))
                {
                    webrequestComponent.responses[rawResponse.id] = rawResponse.responseJsonString;
                }
            }
            try
            {
                JObject response = JsonConvert.DeserializeObject<JObject>(rawResponse.responseJsonString, JsonSerializerConfig.settings);
                string message = null;
                switch (platformComponentOld.activePlatform)
                {
                    case PlatformEnum.BINANCE:
                    case PlatformEnum.BINANCE_TESTNET:
                        long? code = response.ContainsKey("code") ? (long)response["code"] : null;
                        string? msg = response.ContainsKey("msg") ? (string)response["msg"] : null;
                        if (code.HasValue || !msg.IsNullOrEmpty())
                        {
                            message = msg;
                            if (code.HasValue)
                            {
                                message = msg + " (Binance Error Code: " + code.Value + ")";
                                switch (code.Value)
                                {
                                    case -2014: // API-key format invalid.
                                    case -2015: // Invalid API-key, IP, or permissions for action, request ip: 130.176.146.87
                                    case -1022: // Signature for this request is not valid.
                                        // platformComponentOld.apiKey = null;
                                        // platformComponentOld.apiSecret = null;
                                        if (code.Value == -2014 || code.Value == -2015)
                                        {
                                            message = "Login failed with invalid or expired api key, please try to login again. (Binance Error Code: " + code.Value + ")";
                                        }
                                        else
                                        {
                                            message = "Login failed with invalid secret key, please try to login again. (Binance Error Code: " + code.Value + ")";
                                        }
                                        break;
                                }
                            }
                        }
                        break;
                }
                if (!message.IsNullOrEmpty())
                {
                    // loginComponent.allowInput = true;
                    promptComponent.ShowPrompt(PromptConstant.ERROR, message, () =>
                    {
                        promptComponent.active = false;
                    });
                }
            }
            catch (Exception ex)
            {
                // if (webrequestComponent.logging) Debug.Log(logPrefix + "Response json string deserialization error");
            }
            webrequestComponent.rawResponses.Remove(rawResponse.id);
        };
    }
}