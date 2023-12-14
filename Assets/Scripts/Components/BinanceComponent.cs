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
    public Dictionary<string, long> quantityPrecisions = new Dictionary<string, long>();
    public Dictionary<string, long> pricePrecisions = new Dictionary<string, long>();
    public Dictionary<string, double?> fees = new Dictionary<string, double?>();
    public Dictionary<string, double> walletBalances;
    public bool getAccInfo;
    public bool getExchangeInfo;
    public bool getBalance;
}