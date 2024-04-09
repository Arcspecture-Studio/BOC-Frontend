using UnityEngine;

public class HideAllPanelSystem : MonoBehaviour
{
    HideAllPanelComponent hideAllPanelComponent;
    QuickTabComponent quickTabComponent;
    SettingPageComponent settingPageComponent;
    BotTabComponent botTabComponent;

    void Start()
    {
        hideAllPanelComponent = GlobalComponent.instance.hideAllPanelComponent;
        quickTabComponent = GlobalComponent.instance.quickTabComponent;
        settingPageComponent = GlobalComponent.instance.settingPageComponent;
        botTabComponent = GlobalComponent.instance.botTabComponent;

        hideAllPanelComponent.onChange_hideNow.AddListener((value) =>
        {
            if (value != "quickTabComponent") quickTabComponent.active = false;
            if (value != "settingPageComponent") settingPageComponent.active = false;
            if (value != "botTabComponent") botTabComponent.active = false;
        });
    }
}
