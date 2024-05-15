using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;
using System.Security.Authentication;

public class WebsocketComponent : MonoBehaviour
{
    [Header("Config")]
    public bool localhost = true;
    public EnvPort envPort = EnvPort.DEVELOP;
    public bool logging;
    public SslProtocols sslProtocols = SslProtocols.Tls12;
    public bool encryption = true;

    [Header("Runtime")]
    public WebSocket generalSocket;
    [HideInInspector] public List<object> generalRequests = new();
    //[SerializedDictionary("Event Type", "List Of JSON String")]
    public Dictionary<WebsocketEventTypeEnum, List<string>> generalResponses = new();
    public bool connectGeneralSocket;
    public bool connectedGeneralSocket
    {
        get { return generalSocket != null && generalSocket.IsAlive; }
    }
    [HideInInspector] public byte[] generalSocketIv;

    public void AddGeneralResponses(WebsocketEventTypeEnum key, string value)
    {
        if (generalResponses.ContainsKey(key))
        {
            generalResponses[key].Add(value);
        }
        else
        {
            List<string> datas = new() { value };
            generalResponses.Add(key, datas);
        }
    }
    public bool RemovesGeneralResponses(WebsocketEventTypeEnum key)
    {
        if (generalResponses.ContainsKey(key))
        {
            generalResponses[key].RemoveAt(0);
            if (generalResponses[key].Count == 0)
            {
                generalResponses.Remove(key);
            }
            return true;
        }
        else
        {
            return false;
        }
    }
    public string RetrieveGeneralResponses(WebsocketEventTypeEnum key)
    {
        if (generalResponses.ContainsKey(key))
        {
            if (generalResponses[key].Count == 0)
            {
                generalResponses.Remove(key);
                return null;
            }
            else
            {
                return generalResponses[key][0];
            }
        }
        else
        {
            return null;
        }
    }
}