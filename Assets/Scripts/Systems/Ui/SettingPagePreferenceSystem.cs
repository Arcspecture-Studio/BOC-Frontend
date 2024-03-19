
using UnityEngine;

public class SettingPagePreferenceSystem : MonoBehaviour
{
    SettingPageComponent settingPageComponent;
    ProfileComponent profileComponent;
    LoginComponent loginComponent;
    WebsocketComponent websocketComponent;

    bool updatingUIFromProfile = false;
    void Start()
    {
        settingPageComponent = GlobalComponent.instance.settingPageComponent;
        profileComponent = GlobalComponent.instance.profileComponent;
        loginComponent = GlobalComponent.instance.loginComponent;
        websocketComponent = GlobalComponent.instance.websocketComponent;

        settingPageComponent.onChange_updatePreference.AddListener(UpdateUIFromProfile);

        DefineOnUIChangedListeners();
    }

    void DefineOnUIChangedListeners()
    {
        settingPageComponent.symbolInput.onValueChanged.AddListener(value => settingPageComponent.symbolInput.text = value.ToUpper());
        settingPageComponent.symbolInput.onEndEdit.AddListener(value =>
        {
            profileComponent.activeProfile.preference.symbol = value;
            UpdatePreferenceToServer();
        });
        settingPageComponent.lossPercentageInput.onEndEdit.AddListener(value =>
        {
            if (value == "") value = "0";
            profileComponent.activeProfile.preference.lossPercentage = double.Parse(value);
            UpdatePreferenceToServer();
        });
        settingPageComponent.lossAmountInput.onEndEdit.AddListener(value =>
        {
            if (value == "") value = "0";
            profileComponent.activeProfile.preference.lossAmount = double.Parse(value);
            UpdatePreferenceToServer();
        });
        settingPageComponent.marginDistributionModeDropdown.onValueChanged.AddListener(value =>
        {
            profileComponent.activeProfile.preference.marginDistributionMode = (MarginDistributionModeEnum)value;
            UpdatePreferenceToServer();
        });
        // TODO: marginWeightDistribution
        settingPageComponent.takeProfitTypeDropdown.onValueChanged.AddListener(value =>
        {
            profileComponent.activeProfile.preference.takeProfitType = (TakeProfitTypeEnum)value;
            UpdatePreferenceToServer();
        });
        settingPageComponent.riskRewardRatioInput.onEndEdit.AddListener(value =>
        {
            if (value == "")
            {
                value = "1";
                settingPageComponent.riskRewardRatioInput.text = value;
            }
            profileComponent.activeProfile.preference.riskRewardRatio = double.Parse(value);
            UpdatePreferenceToServer();
        });
        settingPageComponent.orderTypeDropdown.onValueChanged.AddListener(value =>
        {
            profileComponent.activeProfile.preference.orderType = (OrderTypeEnum)value;
            UpdatePreferenceToServer();
        });
    }
    void UpdatePreferenceToServer()
    {
        if (updatingUIFromProfile) return;

        General.WebsocketUpdateProfileRequest request = new(loginComponent.token, profileComponent.activeProfile._id, profileComponent.activeProfile.preference);
        websocketComponent.generalRequests.Add(request);
    }
    void UpdateUIFromProfile()
    {
        ProfilePerference preference = profileComponent.activeProfile.preference;
        updatingUIFromProfile = true;

        settingPageComponent.symbolInput.text = preference.symbol;
        settingPageComponent.lossPercentageInput.text = preference.lossPercentage == 0 ?
        "" : preference.lossPercentage.ToString();
        settingPageComponent.lossAmountInput.text = preference.lossAmount == 0 ?
        "" : preference.lossAmount.ToString();
        settingPageComponent.marginDistributionModeDropdown.value = (int)preference.marginDistributionMode;
        settingPageComponent.marginWeightDistributionValueSlider.value = (float)preference.marginWeightDistributionValue;
        settingPageComponent.marginWeightDistributionValueInput.text = preference.marginWeightDistributionValue.ToString();
        settingPageComponent.takeProfitTypeDropdown.value = (int)preference.takeProfitType;
        settingPageComponent.riskRewardRatioInput.text = preference.riskRewardRatio.ToString();
        settingPageComponent.takeProfitTrailingCallbackPercentageSlider.value = (float)preference.takeProfitTrailingCallbackPercentage;
        settingPageComponent.takeProfitTrailingCallbackPercentageInput.text = preference.takeProfitTrailingCallbackPercentage.ToString();
        settingPageComponent.orderTypeDropdown.value = (int)preference.orderType;

        updatingUIFromProfile = false;
    }
}
