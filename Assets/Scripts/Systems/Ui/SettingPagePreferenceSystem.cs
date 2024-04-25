
using UnityEngine;
using WebSocketSharp;

public class SettingPagePreferenceSystem : MonoBehaviour
{
    SettingPageComponent settingPageComponent;
    ProfileComponent profileComponent;
    LoginComponent loginComponent;
    WebsocketComponent websocketComponent;
    QuickTabComponent quickTabComponent;

    bool updatingUIFromProfile = false;
    void Start()
    {
        settingPageComponent = GlobalComponent.instance.settingPageComponent;
        profileComponent = GlobalComponent.instance.profileComponent;
        loginComponent = GlobalComponent.instance.loginComponent;
        websocketComponent = GlobalComponent.instance.websocketComponent;
        quickTabComponent = GlobalComponent.instance.quickTabComponent;

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
                value = "BTCUSDT";
                settingPageComponent.symbolInput.text = value;
            }
            if (profileComponent.activeProfile.preference.order.symbol == value) return;
            profileComponent.activeProfile.preference.order.symbol = value;
            settingPageComponent.updatePreferenceToServer = true;
        });
        settingPageComponent.lossPercentageInput.onEndEdit.AddListener(value =>
        {
            if (value.IsNullOrEmpty()) value = "0";
            if (profileComponent.activeProfile.preference.order.lossPercentage == double.Parse(value)) return;
            profileComponent.activeProfile.preference.order.lossPercentage = double.Parse(value);
            settingPageComponent.updatePreferenceToServer = true;
        });
        settingPageComponent.lossAmountInput.onEndEdit.AddListener(value =>
        {
            if (value.IsNullOrEmpty()) value = "0";
            if (profileComponent.activeProfile.preference.order.lossAmount == double.Parse(value)) return;
            profileComponent.activeProfile.preference.order.lossAmount = double.Parse(value);
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
                value = "1";
                settingPageComponent.riskRewardRatioInput.text = value;
            }
            if (profileComponent.activeProfile.preference.order.riskRewardRatio == double.Parse(value)) return;
            profileComponent.activeProfile.preference.order.riskRewardRatio = double.Parse(value);
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
        if (updatingUIFromProfile || quickTabComponent.updatingUIFromProfile) return;

        General.WebsocketUpdateProfileRequest request = new(loginComponent.token, profileComponent.activeProfile._id, profileComponent.activeProfile.preference);
        websocketComponent.generalRequests.Add(request);
    }
    void UpdateUIFromProfile()
    {
        Perference preference = profileComponent.activeProfile.preference;
        updatingUIFromProfile = true;

        settingPageComponent.symbolInput.text = preference.order.symbol;
        settingPageComponent.lossPercentageInput.text = preference.order.lossPercentage == 0 ?
        "" : preference.order.lossPercentage.ToString();
        settingPageComponent.lossAmountInput.text = preference.order.lossAmount == 0 ?
        "" : preference.order.lossAmount.ToString();
        settingPageComponent.marginDistributionModeDropdown.value = (int)preference.order.marginDistributionMode;
        settingPageComponent.marginWeightDistributionValueSlider.value = (float)preference.order.marginWeightDistributionValue;
        settingPageComponent.marginWeightDistributionValueInput.text = preference.order.marginWeightDistributionValue.ToString();
        settingPageComponent.takeProfitTypeDropdown.value = (int)preference.order.takeProfitType;
        settingPageComponent.riskRewardRatioInput.text = preference.order.riskRewardRatio.ToString();
        settingPageComponent.takeProfitTrailingCallbackPercentageSlider.value = (float)preference.order.takeProfitTrailingCallbackPercentage;
        settingPageComponent.takeProfitTrailingCallbackPercentageInput.text = preference.order.takeProfitTrailingCallbackPercentage.ToString();
        settingPageComponent.orderTypeDropdown.value = (int)preference.order.orderType;

        updatingUIFromProfile = false;
    }
}
