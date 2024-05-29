
using UnityEngine;
using WebSocketSharp;

public class SettingPagePreferenceSystem : MonoBehaviour
{
    SettingPageComponent settingPageComponent;
    ProfileComponent profileComponent;
    LoginComponent loginComponent;
    WebsocketComponent websocketComponent;
    QuickTabComponent quickTabComponent;
    BotTabComponent botTabComponent;

    bool updatingUIFromProfile = false;
    void Start()
    {
        settingPageComponent = GlobalComponent.instance.settingPageComponent;
        profileComponent = GlobalComponent.instance.profileComponent;
        loginComponent = GlobalComponent.instance.loginComponent;
        websocketComponent = GlobalComponent.instance.websocketComponent;
        quickTabComponent = GlobalComponent.instance.quickTabComponent;
        botTabComponent = GlobalComponent.instance.botTabComponent;

        settingPageComponent.onChange_updatePreferenceUI.AddListener(UpdateUIFromProfile);
        settingPageComponent.onChange_updatePreferenceToServer.AddListener(UpdatePreferenceToServer);

        DefineOnUIChangedListeners();
    }

    void DefineOnUIChangedListeners()
    {
        settingPageComponent.symbolInput.onValueChanged.AddListener(value => settingPageComponent.symbolInput.text = value.ToUpper());
        settingPageComponent.symbolInput.onEndEdit.AddListener(value =>
        {
            if (value.IsNullOrEmpty())
            {
                value = profileComponent.activeProfile.preference.order.symbol;
                settingPageComponent.symbolInput.text = value;
            }
            if (profileComponent.activeProfile.preference.order.symbol == value) return;
            profileComponent.activeProfile.preference.order.symbol = value;
            settingPageComponent.updatePreferenceToServer = true;
        });
        settingPageComponent.lossPercentageInput.onEndEdit.AddListener(value =>
        {
            if (value.IsNullOrEmpty())
            {
                value = profileComponent.activeProfile.preference.order.lossPercentage.ToString();
                settingPageComponent.lossPercentageInput.text = value;
            }
            if (profileComponent.activeProfile.preference.order.lossPercentage == float.Parse(value)) return;
            profileComponent.activeProfile.preference.order.lossPercentage = float.Parse(value);
            settingPageComponent.updatePreferenceToServer = true;
        });
        settingPageComponent.lossAmountInput.onEndEdit.AddListener(value =>
        {
            if (value.IsNullOrEmpty()) value = "0";
            if (profileComponent.activeProfile.preference.order.lossAmount == float.Parse(value)) return;
            profileComponent.activeProfile.preference.order.lossAmount = float.Parse(value);
            settingPageComponent.updatePreferenceToServer = true;
        });
        settingPageComponent.marginDistributionModeDropdown.onValueChanged.AddListener(value =>
        {
            profileComponent.activeProfile.preference.order.marginDistributionMode = (MarginDistributionModeEnum)value;
            settingPageComponent.updatePreferenceToServer = true;
        });
        settingPageComponent.takeProfitTypeDropdown.onValueChanged.AddListener(value =>
        {
            profileComponent.activeProfile.preference.order.takeProfitType = (TakeProfitTypeEnum)value;
            settingPageComponent.updatePreferenceToServer = true;
        });
        settingPageComponent.riskRewardRatioInput.onEndEdit.AddListener(value =>
        {
            if (value.IsNullOrEmpty())
            {
                value = profileComponent.activeProfile.preference.order.riskRewardRatio.ToString();
                settingPageComponent.riskRewardRatioInput.text = value;
            }
            if (profileComponent.activeProfile.preference.order.riskRewardRatio == float.Parse(value)) return;
            profileComponent.activeProfile.preference.order.riskRewardRatio = float.Parse(value);
            settingPageComponent.updatePreferenceToServer = true;
        });
        settingPageComponent.orderTypeDropdown.onValueChanged.AddListener(value =>
        {
            profileComponent.activeProfile.preference.order.orderType = (OrderTypeEnum)value;
            settingPageComponent.updatePreferenceToServer = true;
        });
    }
    void UpdatePreferenceToServer()
    {
        if (updatingUIFromProfile || quickTabComponent.updatingUIFromProfile ||
        botTabComponent.updatingUIFromProfile) return;

        General.WebsocketUpdateProfileRequest request = new(loginComponent.token, profileComponent.activeProfileId, profileComponent.activeProfile.preference);
        websocketComponent.generalRequests.Add(request);
    }
    void UpdateUIFromProfile()
    {
        PreferenceOrder preferenceOrder = profileComponent.activeProfile.preference.order;
        updatingUIFromProfile = true;

        settingPageComponent.symbolInput.text = preferenceOrder.symbol;
        settingPageComponent.lossPercentageInput.text = preferenceOrder.lossPercentage == 0 ?
        "" : preferenceOrder.lossPercentage.ToString();
        settingPageComponent.lossAmountInput.text = preferenceOrder.lossAmount == 0 ?
        "" : preferenceOrder.lossAmount.ToString();
        settingPageComponent.marginDistributionModeDropdown.value = (int)preferenceOrder.marginDistributionMode;
        settingPageComponent.marginWeightDistributionValueCustomSlider.SetValue(preferenceOrder.marginWeightDistributionValue);
        settingPageComponent.takeProfitTypeDropdown.value = (int)preferenceOrder.takeProfitType;
        settingPageComponent.riskRewardRatioInput.text = preferenceOrder.riskRewardRatio.ToString();
        settingPageComponent.takeProfitTrailingCallbackPercentageCustomSlider.SetValue(preferenceOrder.takeProfitTrailingCallbackPercentage);
        settingPageComponent.orderTypeDropdown.value = (int)preferenceOrder.orderType;

        updatingUIFromProfile = false;
    }
}
