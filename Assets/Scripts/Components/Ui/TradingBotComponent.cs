using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class TradingBotComponent : MonoBehaviour
{
    [Header("Reference")]
    public TMP_Dropdown tradingBotDropdown;

    // [Header("Runtime")]
    public bool getTradingBots
    {
        set
        {
            if (value) onChange_getTradingBots.Invoke();
        }
    }
    [HideInInspector] public UnityEvent onChange_getTradingBots = new();
    public bool sendSignalToServer // Used by tradingBotDropdown
    {
        set
        {
            if (value) onChange_sendSignalToServer.Invoke();
        }
    }
    [HideInInspector] public UnityEvent onChange_sendSignalToServer = new();
}