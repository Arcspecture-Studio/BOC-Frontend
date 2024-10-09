using System;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;

[Serializable]
public class Platform
{
    public PlatformEnum platform;
    public SerializedDictionary<string, float> walletBalances;
    public List<string> allSymbols;
    public Dictionary<string, string> marginAssets;
    public Dictionary<string, int> quantityPrecisions;
    public Dictionary<string, int> pricePrecisions;
    public Dictionary<string, float?> fees;

    public Platform(PlatformEnum platformEnum)
    {
        platform = platformEnum;
        walletBalances = new();
        allSymbols = new();
        marginAssets = new();
        quantityPrecisions = new();
        pricePrecisions = new();
        fees = new();
    }
}