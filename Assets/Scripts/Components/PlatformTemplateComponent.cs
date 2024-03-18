using System.Collections.Generic;
using UnityEngine;

public class PlatformTemplateComponent : MonoBehaviour
{
    public bool loggedIn;
    public List<string> allSymbols;
    public Dictionary<string, string> marginAssets;
    public Dictionary<string, long> quantityPrecisions;
    public Dictionary<string, long> pricePrecisions;
    public Dictionary<string, double?> fees;
    public Dictionary<string, double> walletBalances;
    public bool getBalance;
}