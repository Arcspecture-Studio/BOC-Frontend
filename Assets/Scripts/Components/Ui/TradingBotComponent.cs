using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class TradingBotComponent : MonoBehaviour
{
    [Header("Reference")]
    public TMP_Dropdown tradingBotDropdown;

    [Header("Runtime")]
    public bool doNotInvokeTradingBotDropdown;
    public bool getTradingBots
    {
        set
        {
            if (value) onChange_getTradingBots.Invoke();
        }
    }
    [HideInInspector] public UnityEvent onChange_getTradingBots = new();
}
