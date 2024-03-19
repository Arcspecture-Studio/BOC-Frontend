using UnityEngine;

public class QuickTabUpdatePreferenceDataSystem : MonoBehaviour
{
    // PreferenceComponent preferenceComponent;
    QuickTabComponent quickTabComponent;
    IoComponent ioComponent;

    void Start()
    {
        // preferenceComponent = GlobalComponent.instance.preferenceComponent;
        quickTabComponent = GlobalComponent.instance.quickTabComponent;
        ioComponent = GlobalComponent.instance.ioComponent;

        SyncDataToPreference();
        quickTabComponent.syncDataFromPreference = true;
    }
    void Update()
    {
        SyncDataFromPreference();
    }

    void SyncDataFromPreference()
    {
        if (!quickTabComponent.syncDataFromPreference) return;
        quickTabComponent.syncDataFromPreference = false;

        // quickTabComponent.entryTimesInput.text = preferenceComponent.quickEntryTimes.ToString();
        // quickTabComponent.atrTimeframeDropdown.value = (int)preferenceComponent.atrTimeframe;
        // quickTabComponent.atrLengthInput.text = preferenceComponent.atrLength.ToString();
        // quickTabComponent.atrMultiplierInput.text = preferenceComponent.atrMultiplier.ToString();
    }
    void SyncDataToPreference()
    {
        quickTabComponent.entryTimesInput.onEndEdit.AddListener(value =>
        {
            // preferenceComponent.quickEntryTimes = int.Parse(value);
            // ioComponent.writePreferences = true;
        });
        quickTabComponent.atrTimeframeDropdown.onValueChanged.AddListener(value =>
        {
            // preferenceComponent.atrTimeframe = (TimeframeEnum)value;
            // ioComponent.writePreferences = true;
        });
        quickTabComponent.atrLengthInput.onEndEdit.AddListener(value =>
        {
            // preferenceComponent.atrLength = int.Parse(value);
            // ioComponent.writePreferences = true;
        });
        quickTabComponent.atrMultiplierInput.onEndEdit.AddListener(value =>
        {
            // preferenceComponent.atrMultiplier = double.Parse(value);
            // ioComponent.writePreferences = true;
        });
    }
}