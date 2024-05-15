using System;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;

[Serializable]
public class Platform
{
    public PlatformEnum platform;
    public SerializedDictionary<string, double> walletBalances;
    public List<string> allSymbols;
    public Dictionary<string, string> marginAssets;
    public Dictionary<string, long> quantityPrecisions;
    public Dictionary<string, long> pricePrecisions;
    public Dictionary<string, double?> fees;

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