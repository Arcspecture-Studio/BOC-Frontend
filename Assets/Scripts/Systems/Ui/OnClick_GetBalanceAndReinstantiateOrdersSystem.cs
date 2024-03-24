using UnityEngine;
using UnityEngine.UI;

public class OnClick_GetBalanceAndReinstantiateOrdersSystem : MonoBehaviour
{
    PlatformComponent platformComponent;
    HideAllPanelComponent hideAllPanelComponent;
    TradingBotComponent tradingBotComponent;
    MiniPromptComponent miniPromptComponent;
    Button button;

    void Start()
    {
        platformComponent = GlobalComponent.instance.platformComponent;
        hideAllPanelComponent = GlobalComponent.instance.hideAllPanelComponent;
        tradingBotComponent = GlobalComponent.instance.tradingBotComponent;
        miniPromptComponent = GlobalComponent.instance.miniPromptComponent;
        button = GetComponent<Button>();

        button.onClick.AddListener(() =>
        {
            hideAllPanelComponent.hideNow = "true";

            platformComponent.getBalance = true;
            // TODO: get runtime data
            tradingBotComponent.getTradingBots = true;
            // miniPromptComponent.message = "Refreshed";
        });
    }
}
