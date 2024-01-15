using UnityEngine;
using UnityEngine.UI;

public class OnClick_GetBalanceAndReinstantiateOrdersSystem : MonoBehaviour
{
    PlatformComponent platformComponent;
    RetrieveOrdersComponent retrieveOrdersComponent;
    HideAllPanelComponent hideAllPanelComponent;
    Button button;

    void Start()
    {
        platformComponent = GlobalComponent.instance.platformComponent;
        retrieveOrdersComponent = GlobalComponent.instance.retrieveOrdersComponent;
        hideAllPanelComponent = GlobalComponent.instance.hideAllPanelComponent;
        button = GetComponent<Button>();

        button.onClick.AddListener(() =>
        {
            hideAllPanelComponent.hideNow = "true";

            platformComponent.getBalance = true;
            retrieveOrdersComponent.destroyOrders = true;
            retrieveOrdersComponent.instantiateOrders = true;
        });
    }
}
