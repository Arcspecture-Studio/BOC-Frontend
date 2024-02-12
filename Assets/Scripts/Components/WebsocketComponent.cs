using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;
using System.Security.Authentication;

public class WebsocketComponent : MonoBehaviour
{
    [Header("Config")]
    public bool localhost = true;
    public bool productionPort = false;
    public bool logging;
    public SslProtocols sslProtocols = SslProtocols.Tls12;

    public WebSocket marketSocket;
    [HideInInspector] public List<object> marketRequests = new();
    public Dictionary<string, List<string>> marketResponses = new();
    [Header("Runtime")]
    public bool connectMarketSocket;
    public bool connectedMarketSocket
    {
        get { return marketSocket != null && marketSocket.IsAlive; }
    }

    public WebSocket userDataSocket;
    public Dictionary<string, List<string>> userDataResponses = new();
    public bool connectUserDataSocket;
    public bool connectedUserDataSocket
    {
        get { return userDataSocket != null && userDataSocket.IsAlive; }
    }

    public WebSocket generalSocket;
    [HideInInspector] public List<object> generalRequests = new();
    //[SerializedDictionary("Event Type", "List Of JSON String")]
    public Dictionary<string, List<string>> generalResponses = new();
    public bool connectGeneralSocket;
    public bool connectedGeneralSocket
    {
        get { return generalSocket != null && generalSocket.IsAlive; }
    }
    [HideInInspector] public string generalSocketConnectionId = null;
    [HideInInspector] public byte[] generalSocketIv = null;
    public bool syncApiKeyToServer;

    public void AddGeneralResponses(string key, string value)
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
    public bool RemovesGeneralResponses(string key)
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
    public string RetrieveGeneralResponses(string key)
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