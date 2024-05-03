using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using UnityEngine;

public class PlatformTemplateComponent : MonoBehaviour
{
    public bool loggedIn;
    public SerializedDictionary<string, double> walletBalances = new();
    public List<string> allSymbols = new();
    public Dictionary<string, string> marginAssets = new();
    public Dictionary<string, long> quantityPrecisions = new();
    public Dictionary<string, long> pricePrecisions = new();
    public Dictionary<string, double?> fees = new();
}