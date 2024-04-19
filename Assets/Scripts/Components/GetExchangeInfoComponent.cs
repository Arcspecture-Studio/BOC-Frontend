using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GetExchangeInfoComponent : MonoBehaviour
{
    public bool getExchangeInfo
    {
        set { onChange_getExchangeInfo.Invoke(); }
    }
    [HideInInspector] public UnityEvent onChange_getExchangeInfo = new();
    public Dictionary<string, General.WebsocketGetExchangeInfo> processExchangeInfo
    {
        set { onChange_processExchangeInfo.Invoke(value); }
    }
    [HideInInspector] public UnityEvent<Dictionary<string, General.WebsocketGetExchangeInfo>> onChange_processExchangeInfo = new();
}