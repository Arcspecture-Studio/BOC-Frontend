using UnityEngine;
using UnityEngine.Events;

public class BinanceComponent : PlatformTemplateComponent
{
    public bool getExchangeInfo
    {
        set { onChange_getExchangeInfo.Invoke(); }
    }
    [HideInInspector] public UnityEvent onChange_getExchangeInfo = new();
}