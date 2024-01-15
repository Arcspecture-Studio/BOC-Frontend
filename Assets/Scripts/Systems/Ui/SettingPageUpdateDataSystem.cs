using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SettingPageUpdateDataSystem : MonoBehaviour
{
    PreferenceComponent preferenceComponent;
    SettingPageComponent settingPageComponent;
    IoComponent ioComponent;
    PlatformComponent platformComponent;
    OrderPagesComponent orderPagesComponent;

    PlatformEnum? activePlatform = null;
    double? balanceUsdt = null;
    double? balanceBusd = null;
    int? totalOrders = null;
    string usdt = "USDT";
    string busd = "BUSD";

    void Start()
    {
        preferenceComponent = GlobalComponent.instance.preferenceComponent;
        settingPageComponent = GlobalComponent.instance.settingPageComponent;
        ioComponent = GlobalComponent.instance.ioComponent;
        platformComponent = GlobalComponent.instance.platformComponent;
        orderPagesComponent = GlobalComponent.instance.orderPagesComponent;

        SyncSettingToPreference();
        UpdatePlatformDropdownList();
        settingPageComponent.syncSetting = true;
    }
    void Update()
    {
        SyncSettingFromPreference();
        UpdateInfo();
    }

    void UpdatePlatformDropdownList()
    {
        settingPageComponent.platformsDropdown.ClearOptions();
        List<TMP_Dropdown.OptionData> optionDataList = new List<TMP_Dropdown.OptionData>();

        foreach (PlatformEnum value in Enum.GetValues(typeof(PlatformEnum)))
        {
            optionDataList.Add(new TMP_Dropdown.OptionData(value.ToString().Replace("_", " ")));
        }
        settingPageComponent.platformsDropdown.AddOptions(optionDataList);
        settingPageComponent.platformsDropdown.value = (int)preferenceComponent.tradingPlatform;
    }
    void SyncSettingFromPreference()
    {
        if (!settingPageComponent.syncSetting) return;
        settingPageComponent.syncSetting = false;

        settingPageComponent.platformsDropdown.value = (int)preferenceComponent.tradingPlatform;
        settingPageComponent.symbolInput.text = preferenceComponent.symbol;
        settingPageComponent.lossPercentageInput.text = preferenceComponent.lossPercentage == 0 ? "" : preferenceComponent.lossPercentage.ToString();
        settingPageComponent.lossAmountInput.text = preferenceComponent.lossAmount == 0 ? "" : preferenceComponent.lossAmount.ToString();
        settingPageComponent.marginDistributionModeDropdown.value = (int)preferenceComponent.marginDistributionMode;
        settingPageComponent.marginWeightDistributionValueSlider.value = (float)preferenceComponent.marginWeightDistributionValue;
        settingPageComponent.marginWeightDistributionValueInput.text = preferenceComponent.marginWeightDistributionValue.ToString();
        settingPageComponent.takeProfitTypeDropdown.value = (int)preferenceComponent.takeProfitType;
        settingPageComponent.riskRewardRatioInput.text = preferenceComponent.riskRewardRatio.ToString();
        settingPageComponent.takeProfitTrailingCallbackPercentageSlider.value = (float)preferenceComponent.takeProfitTrailingCallbackPercentage;
        settingPageComponent.takeProfitTrailingCallbackPercentageInput.text = preferenceComponent.takeProfitTrailingCallbackPercentage.ToString();
        settingPageComponent.orderTypeDropdown.value = (int)preferenceComponent.orderType;
    }
    void SyncSettingToPreference()
    {
        settingPageComponent.platformsDropdown.onValueChanged.AddListener(value =>
        {
            preferenceComponent.tradingPlatform = (PlatformEnum)value;
            ioComponent.writePreferences = true;
        });
        settingPageComponent.symbolInput.onValueChanged.AddListener(value =>
        {
            settingPageComponent.symbolInput.text = value.ToUpper();
        });
        settingPageComponent.symbolInput.onEndEdit.AddListener(value =>
        {
            preferenceComponent.symbol = value;
            ioComponent.writePreferences = true;
        });
        settingPageComponent.lossPercentageInput.onEndEdit.AddListener(value =>
        {
            if (value == "") value = "0";
            preferenceComponent.lossPercentage = double.Parse(value);
            ioComponent.writePreferences = true;
        });
        settingPageComponent.lossAmountInput.onEndEdit.AddListener(value =>
        {
            if (value == "") value = "0";
            preferenceComponent.lossAmount = double.Parse(value);
            ioComponent.writePreferences = true;
        });
        settingPageComponent.marginDistributionModeDropdown.onValueChanged.AddListener(value =>
        {
            preferenceComponent.marginDistributionMode = (MarginDistributionModeEnum)value;
            ioComponent.writePreferences = true;
        });
        settingPageComponent.takeProfitTypeDropdown.onValueChanged.AddListener(value =>
        {
            preferenceComponent.takeProfitType = (OrderTakeProfitTypeEnum)value;
            ioComponent.writePreferences = true;
        });
        settingPageComponent.riskRewardRatioInput.onEndEdit.AddListener(value =>
        {
            if (value == "")
            {
                value = "1";
                settingPageComponent.riskRewardRatioInput.text = value;
            }
            preferenceComponent.riskRewardRatio = double.Parse(value);
            ioComponent.writePreferences = true;
        });
        settingPageComponent.orderTypeDropdown.onValueChanged.AddListener(value =>
        {
            preferenceComponent.orderType = (OrderTypeEnum)value;
            ioComponent.writePreferences = true;
        });
    }
    void UpdateInfo()
    {
        if (activePlatform != platformComponent.tradingPlatform)
        {
            activePlatform = platformComponent.tradingPlatform;
            settingPageComponent.activePlatformText.text = activePlatform.ToString().Replace("_", " ");
        }
        if (platformComponent.walletBalances != null)
        {
            if (platformComponent.walletBalances.ContainsKey(usdt))
            {
                if (balanceUsdt != platformComponent.walletBalances[usdt])
                {
                    balanceUsdt = platformComponent.walletBalances[usdt];
                    settingPageComponent.balanceUdstText.text = Utils.TruncTwoDecimal(balanceUsdt.Value).ToString();
                }
            }
            if (platformComponent.walletBalances.ContainsKey(busd))
            {
                if (balanceBusd != platformComponent.walletBalances[busd])
                {
                    balanceBusd = platformComponent.walletBalances[busd];
                    settingPageComponent.balanceBusdText.text = Utils.TruncTwoDecimal(balanceBusd.Value).ToString();
                }
            }
        }
        if (totalOrders != orderPagesComponent.transform.childCount)
        {
            totalOrders = orderPagesComponent.transform.childCount;
            settingPageComponent.totalOrdersText.text = totalOrders.ToString();
        }
    }
}
