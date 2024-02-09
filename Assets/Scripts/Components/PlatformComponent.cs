using System.Collections.Generic;
using UnityEngine;

public class PlatformComponent : MonoBehaviour
{
    public PlatformEnum tradingPlatform;
    public bool testnet
    {
        get
        {
            return tradingPlatform.ToString().Contains("TESTNET");
        }
    }
    public string apiKey
    {
        get
        {
            string data = string.Empty;
            switch (tradingPlatform)
            {
                case PlatformEnum.BINANCE:
                    data = GlobalComponent.instance.binanceComponent.apiKey;
                    break;
                case PlatformEnum.BINANCE_TESTNET:
                    data = GlobalComponent.instance.binanceTestnetComponent.apiKey;
                    break;
                case PlatformEnum.MEXC:
                    break;
            }
            return data;
        }
        set
        {
            switch (tradingPlatform)
            {
                case PlatformEnum.BINANCE:
                    GlobalComponent.instance.binanceComponent.apiKey = value;
                    break;
                case PlatformEnum.BINANCE_TESTNET:
                    GlobalComponent.instance.binanceTestnetComponent.apiKey = value;
                    break;
                case PlatformEnum.MEXC:
                    break;
            }
        }
    }
    public string apiSecret
    {
        get
        {
            string data = string.Empty;
            switch (tradingPlatform)
            {
                case PlatformEnum.BINANCE:
                    data = GlobalComponent.instance.binanceComponent.apiSecret;
                    break;
                case PlatformEnum.BINANCE_TESTNET:
                    data = GlobalComponent.instance.binanceTestnetComponent.apiSecret;
                    break;
                case PlatformEnum.MEXC:
                    break;
            }
            return data;
        }
        set
        {
            switch (tradingPlatform)
            {
                case PlatformEnum.BINANCE:
                    GlobalComponent.instance.binanceComponent.apiSecret = value;
                    break;
                case PlatformEnum.BINANCE_TESTNET:
                    GlobalComponent.instance.binanceTestnetComponent.apiSecret = value;
                    break;
                case PlatformEnum.MEXC:
                    break;
            }
        }
    }
    public List<string> allSymbols
    {
        get
        {
            List<string> data = new List<string>();
            switch (tradingPlatform)
            {
                case PlatformEnum.BINANCE:
                    data = GlobalComponent.instance.binanceComponent.allSymbols;
                    break;
                case PlatformEnum.BINANCE_TESTNET:
                    data = GlobalComponent.instance.binanceTestnetComponent.allSymbols;
                    break;
                case PlatformEnum.MEXC:
                    break;
            }
            return data;
        }
        set
        {
            switch (tradingPlatform)
            {
                case PlatformEnum.BINANCE:
                    GlobalComponent.instance.binanceComponent.allSymbols = value;
                    break;
                case PlatformEnum.BINANCE_TESTNET:
                    GlobalComponent.instance.binanceTestnetComponent.allSymbols = value;
                    break;
                case PlatformEnum.MEXC:
                    break;
            }
        }
    }
    public Dictionary<string, string> marginAssets
    {
        get
        {
            Dictionary<string, string> data = new();
            switch (tradingPlatform)
            {
                case PlatformEnum.BINANCE:
                    data = GlobalComponent.instance.binanceComponent.marginAssets;
                    break;
                case PlatformEnum.BINANCE_TESTNET:
                    data = GlobalComponent.instance.binanceTestnetComponent.marginAssets;
                    break;
                case PlatformEnum.MEXC:
                    break;
            }
            return data;
        }
        set
        {
            switch (tradingPlatform)
            {
                case PlatformEnum.BINANCE:
                    GlobalComponent.instance.binanceComponent.marginAssets = value;
                    break;
                case PlatformEnum.BINANCE_TESTNET:
                    GlobalComponent.instance.binanceTestnetComponent.marginAssets = value;
                    break;
                case PlatformEnum.MEXC:
                    break;
            }
        }
    }
    public Dictionary<string, long> quantityPrecisions
    {
        get
        {
            Dictionary<string, long> data = new Dictionary<string, long>();
            switch (tradingPlatform)
            {
                case PlatformEnum.BINANCE:
                    data = GlobalComponent.instance.binanceComponent.quantityPrecisions;
                    break;
                case PlatformEnum.BINANCE_TESTNET:
                    data = GlobalComponent.instance.binanceTestnetComponent.quantityPrecisions;
                    break;
                case PlatformEnum.MEXC:
                    break;
            }
            return data;
        }
        set
        {
            switch (tradingPlatform)
            {
                case PlatformEnum.BINANCE:
                    GlobalComponent.instance.binanceComponent.quantityPrecisions = value;
                    break;
                case PlatformEnum.BINANCE_TESTNET:
                    GlobalComponent.instance.binanceTestnetComponent.quantityPrecisions = value;
                    break;
                case PlatformEnum.MEXC:
                    break;
            }
        }
    }
    public Dictionary<string, long> pricePrecisions
    {
        get
        {
            Dictionary<string, long> data = new Dictionary<string, long>();
            switch (tradingPlatform)
            {
                case PlatformEnum.BINANCE:
                    data = GlobalComponent.instance.binanceComponent.pricePrecisions;
                    break;
                case PlatformEnum.BINANCE_TESTNET:
                    data = GlobalComponent.instance.binanceTestnetComponent.pricePrecisions;
                    break;
                case PlatformEnum.MEXC:
                    break;
            }
            return data;
        }
        set
        {
            switch (tradingPlatform)
            {
                case PlatformEnum.BINANCE:
                    GlobalComponent.instance.binanceComponent.pricePrecisions = value;
                    break;
                case PlatformEnum.BINANCE_TESTNET:
                    GlobalComponent.instance.binanceTestnetComponent.pricePrecisions = value;
                    break;
                case PlatformEnum.MEXC:
                    break;
            }
        }
    }
    public Dictionary<string, double?> fees
    {
        get
        {
            Dictionary<string, double?> data = new Dictionary<string, double?>();
            switch (tradingPlatform)
            {
                case PlatformEnum.BINANCE:
                    data = GlobalComponent.instance.binanceComponent.fees;
                    break;
                case PlatformEnum.BINANCE_TESTNET:
                    data = GlobalComponent.instance.binanceTestnetComponent.fees;
                    break;
                case PlatformEnum.MEXC:
                    break;
            }
            return data;
        }
        set
        {
            switch (tradingPlatform)
            {
                case PlatformEnum.BINANCE:
                    GlobalComponent.instance.binanceComponent.fees = value;
                    break;
                case PlatformEnum.BINANCE_TESTNET:
                    GlobalComponent.instance.binanceTestnetComponent.fees = value;
                    break;
                case PlatformEnum.MEXC:
                    break;
            }
        }
    }
    public Dictionary<string, double> walletBalances
    {
        get
        {
            Dictionary<string, double> data = new Dictionary<string, double>();
            switch (tradingPlatform)
            {
                case PlatformEnum.BINANCE:
                    data = GlobalComponent.instance.binanceComponent.walletBalances;
                    break;
                case PlatformEnum.BINANCE_TESTNET:
                    data = GlobalComponent.instance.binanceTestnetComponent.walletBalances;
                    break;
                case PlatformEnum.MEXC:
                    break;
            }
            return data;
        }
        set
        {
            switch (tradingPlatform)
            {
                case PlatformEnum.BINANCE:
                    GlobalComponent.instance.binanceComponent.walletBalances = value;
                    break;
                case PlatformEnum.BINANCE_TESTNET:
                    GlobalComponent.instance.binanceTestnetComponent.walletBalances = value;
                    break;
                case PlatformEnum.MEXC:
                    break;
            }
        }
    }
    public bool getBalance
    {
        get
        {
            bool data = false;
            switch (tradingPlatform)
            {
                case PlatformEnum.BINANCE:
                    data = GlobalComponent.instance.binanceComponent.getBalance;
                    break;
                case PlatformEnum.BINANCE_TESTNET:
                    data = GlobalComponent.instance.binanceTestnetComponent.getBalance;
                    break;
                case PlatformEnum.MEXC:
                    break;
            }
            return data;
        }
        set
        {
            switch (tradingPlatform)
            {
                case PlatformEnum.BINANCE:
                    GlobalComponent.instance.binanceComponent.getBalance = value;
                    break;
                case PlatformEnum.BINANCE_TESTNET:
                    GlobalComponent.instance.binanceTestnetComponent.getBalance = value;
                    break;
                case PlatformEnum.MEXC:
                    break;
            }
        }
    }
}