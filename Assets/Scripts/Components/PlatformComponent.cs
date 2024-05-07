using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlatformComponent : MonoBehaviour
{
    [Header("Reference")]
    public GameObject registerPlatformObj;
    public GameObject switchPlatformObj;
    public TMP_Dropdown platformsDropdown;
    public TMP_InputField apiKeyInput;
    public TMP_InputField apiSecretInput;
    public Button connectButton;
    public Button cancelRegisterButton;
    public TMP_Dropdown platformIdsDropdown;
    public Button addPlatformButton;
    public Button disconnectButton;
    public Button backButton;
    public Button logoutButton;

    [Header("Runtime")]
    [HideInInspector] public UnityEvent onEnable = new();
    public Dictionary<string, Platform> platforms = new();
    public bool loggedIn
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
    public PlatformEnum activePlatformOnUi
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
    public PlatformEnum activePlatform
    {
        get
        {
            if (GlobalComponent.instance.profileComponent.activeProfile == null ||
                GlobalComponent.instance.profileComponent.activeProfile.platformId == null ||
                !platforms.ContainsKey(GlobalComponent.instance.profileComponent.activeProfile.platformId))
                return activePlatformOnUi;
            return platforms[GlobalComponent.instance.profileComponent.activeProfile.platformId].platform;
        }
        set // TODO: Suppose there are no setter
        {
            if (GlobalComponent.instance.profileComponent.activeProfile == null ||
                GlobalComponent.instance.profileComponent.activeProfile.platformId == null ||
                !platforms.ContainsKey(GlobalComponent.instance.profileComponent.activeProfile.platformId))
                return;
            platforms[GlobalComponent.instance.profileComponent.activeProfile.platformId].platform = value;
        }
    }
    public List<string> allSymbols
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
    public Dictionary<string, string> marginAssets
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
    public Dictionary<string, long> quantityPrecisions
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
    public Dictionary<string, long> pricePrecisions
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
    public Dictionary<string, double?> fees
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
    public SerializedDictionary<string, double> walletBalances
    {
        get
        {
            SerializedDictionary<string, double> data = new();
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

    public void OnEnable()
    {
        onEnable.Invoke();
    }
}