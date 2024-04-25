using UnityEngine;
using WebSocketSharp;

public class QuickTabUpdatePreferenceDataSystem : MonoBehaviour
{
    QuickTabComponent quickTabComponent;
    ProfileComponent profileComponent;
    SettingPageComponent settingPageComponent;

    void Start()
    {
        quickTabComponent = GlobalComponent.instance.quickTabComponent;
        profileComponent = GlobalComponent.instance.profileComponent;
        settingPageComponent = GlobalComponent.instance.settingPageComponent;

        quickTabComponent.onChange_updatePreferenceUI.AddListener(UpdateUIFromProfile);

        DefineOnUIChangedListeners();
    }

    void UpdateUIFromProfile()
    {
        Perference preference = profileComponent.activeProfile.preference;
        quickTabComponent.updatingUIFromProfile = true;

        quickTabComponent.entryTimesInput.text = preference.quickOrder.quickEntryTimes.ToString();
        quickTabComponent.atrTimeframeDropdown.value = (int)preference.quickOrder.atrTimeframe;
        quickTabComponent.atrLengthInput.text = preference.quickOrder.atrLength.ToString();
        quickTabComponent.atrMultiplierInput.text = preference.quickOrder.atrMultiplier.ToString();

        quickTabComponent.updatingUIFromProfile = false;
    }
    void DefineOnUIChangedListeners()
    {
        quickTabComponent.entryTimesInput.onEndEdit.AddListener(value =>
        {
            if (value.IsNullOrEmpty())
            {
                value = "2";
                quickTabComponent.entryTimesInput.text = value;
            }
            if (profileComponent.activeProfile.preference.quickOrder.quickEntryTimes == int.Parse(value)) return;
            profileComponent.activeProfile.preference.quickOrder.quickEntryTimes = int.Parse(value);
            settingPageComponent.updatePreferenceToServer = true;
        });
        quickTabComponent.atrTimeframeDropdown.onValueChanged.AddListener(value =>
        {
            if (profileComponent.activeProfile.preference.quickOrder.atrTimeframe == (TimeframeEnum)value) return;
            profileComponent.activeProfile.preference.quickOrder.atrTimeframe = (TimeframeEnum)value;
            settingPageComponent.updatePreferenceToServer = true;
        });
        quickTabComponent.atrLengthInput.onEndEdit.AddListener(value =>
        {
            if (value.IsNullOrEmpty())
            {
                value = "13";
                quickTabComponent.atrLengthInput.text = value;
            }
            if (profileComponent.activeProfile.preference.quickOrder.atrLength == int.Parse(value)) return;
            profileComponent.activeProfile.preference.quickOrder.atrLength = int.Parse(value);
            settingPageComponent.updatePreferenceToServer = true;
        });
        quickTabComponent.atrMultiplierInput.onEndEdit.AddListener(value =>
        {
            if (value.IsNullOrEmpty())
            {
                value = "3";
                quickTabComponent.atrMultiplierInput.text = value;
            }
            if (profileComponent.activeProfile.preference.quickOrder.atrMultiplier == double.Parse(value)) return;
            profileComponent.activeProfile.preference.quickOrder.atrMultiplier = double.Parse(value);
            settingPageComponent.updatePreferenceToServer = true;
        });
    }
}