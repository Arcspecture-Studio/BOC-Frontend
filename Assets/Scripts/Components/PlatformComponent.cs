using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlatformComponent : PlatformTemplateComponent
{
    [Header("Reference")]
    public TMP_Dropdown platformsDropdown;
    public TMP_InputField apiKeyInput;
    public GameObject apiKeyObj;
    public TMP_InputField apiSecretInput;
    public GameObject apiSecretObj;
    public Button proceedButton;
    public TMP_Text proceedButtonText;
    public Button backButton;
    public GameObject backButtonObj;

    [Header("Runtime")]
    [HideInInspector] public UnityEvent onEnable = new();
    public PlatformEnum activePlatform
    {
        get
        {
            return (PlatformEnum)platformsDropdown.value;
        }
        set
        {
            platformsDropdown.value = (int)value;
        }
    }
    public bool activePlatformTestnet
    {
        get
        {
            return activePlatform.ToString().Contains("TESTNET");
        }
    }
    public new bool loggedIn
    {
        get
        {
            bool data = false;
            switch (activePlatform)
            {
                case PlatformEnum.BINANCE:
                    data = GlobalComponent.instance.binanceComponent.loggedIn;
                    break;
                case PlatformEnum.BINANCE_TESTNET:
                    data = GlobalComponent.instance.binanceTestnetComponent.loggedIn;
                    break;
            }
            return data;
        }
        set
        {
            switch (activePlatform)
            {
                case PlatformEnum.BINANCE:
                    GlobalComponent.instance.binanceComponent.loggedIn = value;
                    break;
                case PlatformEnum.BINANCE_TESTNET:
                    GlobalComponent.instance.binanceTestnetComponent.loggedIn = value;
                    break;
            }
        }
    }
    public new List<string> allSymbols
    {
        get
        {
            List<string> data = new();
            switch (activePlatform)
            {
                case PlatformEnum.BINANCE:
                    data = GlobalComponent.instance.binanceComponent.allSymbols;
                    break;
                case PlatformEnum.BINANCE_TESTNET:
                    data = GlobalComponent.instance.binanceTestnetComponent.allSymbols;
                    break;
            }
            return data;
        }
        set
        {
            switch (activePlatform)
            {
                case PlatformEnum.BINANCE:
                    GlobalComponent.instance.binanceComponent.allSymbols = value;
                    break;
                case PlatformEnum.BINANCE_TESTNET:
                    GlobalComponent.instance.binanceTestnetComponent.allSymbols = value;
                    break;
            }
        }
    }
    public new Dictionary<string, string> marginAssets
    {
        get
        {
            Dictionary<string, string> data = new();
            switch (activePlatform)
            {
                case PlatformEnum.BINANCE:
                    data = GlobalComponent.instance.binanceComponent.marginAssets;
                    break;
                case PlatformEnum.BINANCE_TESTNET:
                    data = GlobalComponent.instance.binanceTestnetComponent.marginAssets;
                    break;
            }
            return data;
        }
        set
        {
            switch (activePlatform)
            {
                case PlatformEnum.BINANCE:
                    GlobalComponent.instance.binanceComponent.marginAssets = value;
                    break;
                case PlatformEnum.BINANCE_TESTNET:
                    GlobalComponent.instance.binanceTestnetComponent.marginAssets = value;
                    break;
            }
        }
    }
    public new Dictionary<string, long> quantityPrecisions
    {
        get
        {
            Dictionary<string, long> data = new();
            switch (activePlatform)
            {
                case PlatformEnum.BINANCE:
                    data = GlobalComponent.instance.binanceComponent.quantityPrecisions;
                    break;
                case PlatformEnum.BINANCE_TESTNET:
                    data = GlobalComponent.instance.binanceTestnetComponent.quantityPrecisions;
                    break;
            }
            return data;
        }
        set
        {
            switch (activePlatform)
            {
                case PlatformEnum.BINANCE:
                    GlobalComponent.instance.binanceComponent.quantityPrecisions = value;
                    break;
                case PlatformEnum.BINANCE_TESTNET:
                    GlobalComponent.instance.binanceTestnetComponent.quantityPrecisions = value;
                    break;
            }
        }
    }
    public new Dictionary<string, long> pricePrecisions
    {
        get
        {
            Dictionary<string, long> data = new();
            switch (activePlatform)
            {
                case PlatformEnum.BINANCE:
                    data = GlobalComponent.instance.binanceComponent.pricePrecisions;
                    break;
                case PlatformEnum.BINANCE_TESTNET:
                    data = GlobalComponent.instance.binanceTestnetComponent.pricePrecisions;
                    break;
            }
            return data;
        }
        set
        {
            switch (activePlatform)
            {
                case PlatformEnum.BINANCE:
                    GlobalComponent.instance.binanceComponent.pricePrecisions = value;
                    break;
                case PlatformEnum.BINANCE_TESTNET:
                    GlobalComponent.instance.binanceTestnetComponent.pricePrecisions = value;
                    break;
            }
        }
    }
    public new Dictionary<string, double?> fees
    {
        get
        {
            Dictionary<string, double?> data = new();
            switch (activePlatform)
            {
                case PlatformEnum.BINANCE:
                    data = GlobalComponent.instance.binanceComponent.fees;
                    break;
                case PlatformEnum.BINANCE_TESTNET:
                    data = GlobalComponent.instance.binanceTestnetComponent.fees;
                    break;
            }
            return data;
        }
        set
        {
            switch (activePlatform)
            {
                case PlatformEnum.BINANCE:
                    GlobalComponent.instance.binanceComponent.fees = value;
                    break;
                case PlatformEnum.BINANCE_TESTNET:
                    GlobalComponent.instance.binanceTestnetComponent.fees = value;
                    break;
            }
        }
    }
    public new Dictionary<string, double> walletBalances
    {
        get
        {
            Dictionary<string, double> data = new();
            switch (activePlatform)
            {
                case PlatformEnum.BINANCE:
                    data = GlobalComponent.instance.binanceComponent.walletBalances;
                    break;
                case PlatformEnum.BINANCE_TESTNET:
                    data = GlobalComponent.instance.binanceTestnetComponent.walletBalances;
                    break;
            }
            return data;
        }
        set
        {
            switch (activePlatform)
            {
                case PlatformEnum.BINANCE:
                    GlobalComponent.instance.binanceComponent.walletBalances = value;
                    break;
                case PlatformEnum.BINANCE_TESTNET:
                    GlobalComponent.instance.binanceTestnetComponent.walletBalances = value;
                    break;
            }
        }
    }
    public new bool getBalance
    {
        set
        {
            switch (activePlatform)
            {
                case PlatformEnum.BINANCE:
                    GlobalComponent.instance.binanceComponent.getBalance = value;
                    break;
                case PlatformEnum.BINANCE_TESTNET:
                    GlobalComponent.instance.binanceTestnetComponent.getBalance = value;
                    break;
            }
        }
    }
    public new General.WebsocketGetInitialDataResponse processInitialData
    {
        set
        {
            switch (activePlatform)
            {
                case PlatformEnum.BINANCE:
                    GlobalComponent.instance.binanceComponent.processInitialData = value;
                    break;
                case PlatformEnum.BINANCE_TESTNET:
                    GlobalComponent.instance.binanceTestnetComponent.processInitialData = value;
                    break;
            }
        }
    }

    public void OnEnable()
    {
        onEnable.Invoke();
    }
}