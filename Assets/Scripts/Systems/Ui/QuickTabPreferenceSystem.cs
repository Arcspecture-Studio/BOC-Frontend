using UnityEngine;
using WebSocketSharp;

public class QuickTabPreferenceSystem : MonoBehaviour
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
        PreferenceQuickOrder preferenceQuickOrder = profileComponent.activeProfile.preference.quickOrder;
        quickTabComponent.updatingUIFromProfile = true;

        quickTabComponent.entryTimesInput.text = preferenceQuickOrder.quickEntryTimes.ToString();
        quickTabComponent.atrTimeframeDropdown.value = (int)preferenceQuickOrder.atrTimeframe;
        quickTabComponent.atrLengthInput.text = preferenceQuickOrder.atrLength.ToString();
        quickTabComponent.atrMultiplierInput.text = preferenceQuickOrder.atrMultiplier.ToString();

        quickTabComponent.updatingUIFromProfile = false;
    }
    void DefineOnUIChangedListeners()
    {
        quickTabComponent.entryTimesInput.onEndEdit.AddListener(value =>
        {
            if (value.IsNullOrEmpty())
            {
                value = profileComponent.activeProfile.preference.quickOrder.quickEntryTimes.ToString();
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
                value = profileComponent.activeProfile.preference.quickOrder.atrLength.ToString();
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
                value = profileComponent.activeProfile.preference.quickOrder.atrMultiplier.ToString();
                quickTabComponent.atrMultiplierInput.text = value;
            }
            if (profileComponent.activeProfile.preference.quickOrder.atrMultiplier == float.Parse(value)) return;
            profileComponent.activeProfile.preference.quickOrder.atrMultiplier = float.Parse(value);
            settingPageComponent.updatePreferenceToServer = true;
        });
    }
}