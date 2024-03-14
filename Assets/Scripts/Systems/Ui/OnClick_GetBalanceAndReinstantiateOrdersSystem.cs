using UnityEngine;
using UnityEngine.UI;

public class OnClick_GetBalanceAndReinstantiateOrdersSystem : MonoBehaviour
{
    PlatformComponentOld platformComponentOld;
    RetrieveOrdersComponent retrieveOrdersComponent;
    HideAllPanelComponent hideAllPanelComponent;
    TradingBotComponent tradingBotComponent;
    MiniPromptComponent miniPromptComponent;
    Button button;

    void Start()
    {
        platformComponentOld = GlobalComponent.instance.platformComponentOld;
        retrieveOrdersComponent = GlobalComponent.instance.retrieveOrdersComponent;
        hideAllPanelComponent = GlobalComponent.instance.hideAllPanelComponent;
        tradingBotComponent = GlobalComponent.instance.tradingBotComponent;
        miniPromptComponent = GlobalComponent.instance.miniPromptComponent;
        button = GetComponent<Button>();

        button.onClick.AddListener(() =>
        {
            hideAllPanelComponent.hideNow = "true";

            platformComponentOld.getBalance = true;
            retrieveOrdersComponent.destroyOrders = true;
            retrieveOrdersComponent.instantiateOrders = true;
            tradingBotComponent.getTradingBots = true;
            // miniPromptComponent.message = "Refreshed";
        });
    }
}
