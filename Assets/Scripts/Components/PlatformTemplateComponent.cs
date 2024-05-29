using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using UnityEngine;

public class PlatformTemplateComponent : MonoBehaviour
{
    public bool loggedIn;
    public PlatformEnum activePlatform;
    public SerializedDictionary<string, float> walletBalances = new();
    public List<string> allSymbols = new();
    public Dictionary<string, string> marginAssets = new();
    public Dictionary<string, int> quantityPrecisions = new();
    public Dictionary<string, int> pricePrecisions = new();
    public Dictionary<string, float?> fees = new();
}