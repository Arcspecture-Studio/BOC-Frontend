using UnityEngine;
using UnityEngine.UI;

public class OnClick_GetBalanceAndReinstantiateOrdersSystem : MonoBehaviour
{
    PlatformComponent platformComponent;
    HideAllPanelComponent hideAllPanelComponent;
    Button button;

    void Start()
    {
        platformComponent = GlobalComponent.instance.platformComponent;
        hideAllPanelComponent = GlobalComponent.instance.hideAllPanelComponent;
        button = GetComponent<Button>();

        button.onClick.AddListener(() =>
        {
            hideAllPanelComponent.hideNow = "true";

            platformComponent.getBalance = true;
            // TODO: get runtime data (orders, quickOrders, tradingBots)
        });
    }
}
