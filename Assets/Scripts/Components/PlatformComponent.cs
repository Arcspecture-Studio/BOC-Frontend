using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlatformComponent : MonoBehaviour
{
    [Header("Reference")]
    public GameObject addPlatformObj;
    public GameObject switchOrRemovePlatformObj;
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
    public PlatformPageEnum platformPage
    {
        set { onChange_platformPage.Invoke(value); }
    }
    [HideInInspector] public UnityEvent<PlatformPageEnum> onChange_platformPage = new();
    public Dictionary<string, Platform> platforms = new();
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
            if (GlobalComponent.instance.profileComponent.activeProfile.platformId == null ||
            platforms[GlobalComponent.instance.profileComponent.activeProfile.platformId] == null) return PlatformEnum.NONE;
            return platforms[GlobalComponent.instance.profileComponent.activeProfile.platformId].platform;
        }
    }
    public List<string> allSymbols
    {
        get
        {
            if (GlobalComponent.instance.profileComponent.activeProfile.platformId == null ||
            platforms[GlobalComponent.instance.profileComponent.activeProfile.platformId] == null) return new();
            return platforms[GlobalComponent.instance.profileComponent.activeProfile.platformId].allSymbols;
        }
        set
        {
            if (GlobalComponent.instance.profileComponent.activeProfile.platformId == null ||
            platforms[GlobalComponent.instance.profileComponent.activeProfile.platformId] == null) return;
            platforms[GlobalComponent.instance.profileComponent.activeProfile.platformId].allSymbols = value;
        }
    }
    public Dictionary<string, string> marginAssets
    {
        get
        {
            if (GlobalComponent.instance.profileComponent.activeProfile.platformId == null ||
            platforms[GlobalComponent.instance.profileComponent.activeProfile.platformId] == null) return new();
            return platforms[GlobalComponent.instance.profileComponent.activeProfile.platformId].marginAssets;
        }
        set
        {
            if (GlobalComponent.instance.profileComponent.activeProfile.platformId == null ||
            platforms[GlobalComponent.instance.profileComponent.activeProfile.platformId] == null) return;
            platforms[GlobalComponent.instance.profileComponent.activeProfile.platformId].marginAssets = value;
        }
    }
    public Dictionary<string, long> quantityPrecisions
    {
        get
        {
            if (GlobalComponent.instance.profileComponent.activeProfile.platformId == null ||
            platforms[GlobalComponent.instance.profileComponent.activeProfile.platformId] == null) return new();
            return platforms[GlobalComponent.instance.profileComponent.activeProfile.platformId].quantityPrecisions;
        }
        set
        {
            if (GlobalComponent.instance.profileComponent.activeProfile.platformId == null ||
            platforms[GlobalComponent.instance.profileComponent.activeProfile.platformId] == null) return;
            platforms[GlobalComponent.instance.profileComponent.activeProfile.platformId].quantityPrecisions = value;
        }
    }
    public Dictionary<string, long> pricePrecisions
    {
        get
        {
            if (GlobalComponent.instance.profileComponent.activeProfile.platformId == null ||
            platforms[GlobalComponent.instance.profileComponent.activeProfile.platformId] == null) return new();
            return platforms[GlobalComponent.instance.profileComponent.activeProfile.platformId].pricePrecisions;
        }
        set
        {
            if (GlobalComponent.instance.profileComponent.activeProfile.platformId == null ||
            platforms[GlobalComponent.instance.profileComponent.activeProfile.platformId] == null) return;
            platforms[GlobalComponent.instance.profileComponent.activeProfile.platformId].pricePrecisions = value;
        }
    }
    public Dictionary<string, double?> fees
    {
        get
        {
            if (GlobalComponent.instance.profileComponent.activeProfile.platformId == null ||
            platforms[GlobalComponent.instance.profileComponent.activeProfile.platformId] == null) return new();
            return platforms[GlobalComponent.instance.profileComponent.activeProfile.platformId].fees;
        }
        set
        {
            if (GlobalComponent.instance.profileComponent.activeProfile.platformId == null ||
            platforms[GlobalComponent.instance.profileComponent.activeProfile.platformId] == null) return;
            platforms[GlobalComponent.instance.profileComponent.activeProfile.platformId].fees = value;
        }
    }
    public SerializedDictionary<string, double> walletBalances
    {
        get
        {
            if (GlobalComponent.instance.profileComponent.activeProfile.platformId == null ||
            platforms[GlobalComponent.instance.profileComponent.activeProfile.platformId] == null) return new();
            return platforms[GlobalComponent.instance.profileComponent.activeProfile.platformId].walletBalances;
        }
        set
        {
            if (GlobalComponent.instance.profileComponent.activeProfile.platformId == null ||
            platforms[GlobalComponent.instance.profileComponent.activeProfile.platformId] == null) return;
            platforms[GlobalComponent.instance.profileComponent.activeProfile.platformId].walletBalances = value;
        }
    }

    public void OnEnable()
    {
        onEnable.Invoke();
    }
}