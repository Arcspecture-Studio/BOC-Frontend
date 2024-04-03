using UnityEngine;

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
        ProfilePerference preference = profileComponent.activeProfile.preference;
        quickTabComponent.updatingUIFromProfile = true;

        quickTabComponent.entryTimesInput.text = preference.quickEntryTimes.ToString();
        quickTabComponent.atrTimeframeDropdown.value = (int)preference.atrTimeframe;
        quickTabComponent.atrLengthInput.text = preference.atrLength.ToString();
        quickTabComponent.atrMultiplierInput.text = preference.atrMultiplier.ToString();

        quickTabComponent.updatingUIFromProfile = false;
    }
    void DefineOnUIChangedListeners()
    {
        quickTabComponent.entryTimesInput.onEndEdit.AddListener(value =>
        {
            if (profileComponent.activeProfile.preference.quickEntryTimes == int.Parse(value)) return;
            profileComponent.activeProfile.preference.quickEntryTimes = int.Parse(value);
            settingPageComponent.updatePreferenceToServer = true;
        });
        quickTabComponent.atrTimeframeDropdown.onValueChanged.AddListener(value =>
        {
            if (profileComponent.activeProfile.preference.atrTimeframe == (TimeframeEnum)value) return;
            profileComponent.activeProfile.preference.atrTimeframe = (TimeframeEnum)value;
            settingPageComponent.updatePreferenceToServer = true;
        });
        quickTabComponent.atrLengthInput.onEndEdit.AddListener(value =>
        {
            if (profileComponent.activeProfile.preference.atrLength == int.Parse(value)) return;
            profileComponent.activeProfile.preference.atrLength = int.Parse(value);
            settingPageComponent.updatePreferenceToServer = true;
        });
        quickTabComponent.atrMultiplierInput.onEndEdit.AddListener(value =>
        {
            if (profileComponent.activeProfile.preference.atrMultiplier == double.Parse(value)) return;
            profileComponent.activeProfile.preference.atrMultiplier = double.Parse(value);
            settingPageComponent.updatePreferenceToServer = true;
        });
    }
}