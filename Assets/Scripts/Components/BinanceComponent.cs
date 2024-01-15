using System.Collections.Generic;
using UnityEngine;

public class BinanceComponent : MonoBehaviour
{
    public string apiKey = null;
    public string apiSecret = null;
    public string loginPhrase = null;
    public bool loggedIn = false;
    public string listenKey;
    public List<string> allSymbols;
    public Dictionary<string, string> marginAssets = new();
    public Dictionary<string, long> quantityPrecisions = new();
    public Dictionary<string, long> pricePrecisions = new();
    public Dictionary<string, double?> fees = new();
    public Dictionary<string, double> walletBalances;
    public bool getAccInfo;
    public bool getExchangeInfo;
    public bool getBalance;
}