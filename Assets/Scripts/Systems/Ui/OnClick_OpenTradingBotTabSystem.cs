using UnityEngine;
using UnityEngine.UI;

public class OnClick_OpenTradingBotTabSystem : MonoBehaviour
{
    TradingBotTabComponent tradingBotTabComponent;
    HideAllPanelComponent hideAllPanelComponent;
    Button button;

    void Start()
    {
        tradingBotTabComponent = GlobalComponent.instance.tradingBotTabComponent;
        hideAllPanelComponent = GlobalComponent.instance.hideAllPanelComponent;

        button = GetComponent<Button>();

        button.onClick.AddListener(() =>
        {
            hideAllPanelComponent.hideNow = "tradingBotTabComponent";

            tradingBotTabComponent.active = !tradingBotTabComponent.active;
        });
    }
}
