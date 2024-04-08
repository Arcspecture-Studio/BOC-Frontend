using UnityEngine;

public class HideAllPanelSystem : MonoBehaviour
{
    HideAllPanelComponent hideAllPanelComponent;
    QuickTabComponent quickTabComponent;
    SettingPageComponent settingPageComponent;
    TradingBotTabComponent tradingBotTabComponent;

    void Start()
    {
        hideAllPanelComponent = GlobalComponent.instance.hideAllPanelComponent;
        quickTabComponent = GlobalComponent.instance.quickTabComponent;
        settingPageComponent = GlobalComponent.instance.settingPageComponent;
        tradingBotTabComponent = GlobalComponent.instance.tradingBotTabComponent;

        hideAllPanelComponent.onChange_hideNow.AddListener((value) =>
        {
            if (value != "quickTabComponent") quickTabComponent.active = false;
            if (value != "settingPageComponent") settingPageComponent.active = false;
            if (value != "tradingBotTabComponent") tradingBotTabComponent.active = false;
        });
    }
}
