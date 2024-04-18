using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlatformTemplateComponent : MonoBehaviour
{
    public bool loggedIn;
    public Dictionary<string, double> walletBalances = new();
    public List<string> allSymbols = new();
    public Dictionary<string, string> marginAssets = new();
    public Dictionary<string, long> quantityPrecisions = new();
    public Dictionary<string, long> pricePrecisions = new();
    public Dictionary<string, double?> fees = new();
    public bool getBalance
    {
        set { onChange_getBalance.Invoke(); }
    }
    [HideInInspector] public UnityEvent onChange_getBalance = new();
}