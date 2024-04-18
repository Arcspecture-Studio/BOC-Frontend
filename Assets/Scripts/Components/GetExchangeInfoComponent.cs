using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GetExchangeInfoComponent : MonoBehaviour
{
    public Dictionary<string, General.WebsocketGetExchangeInfo> processExchangeInfo
    {
        set { onChange_processExchangeInfo.Invoke(value); }
    }
    [HideInInspector] public UnityEvent<Dictionary<string, General.WebsocketGetExchangeInfo>> onChange_processExchangeInfo = new();
}