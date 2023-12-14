using UnityEngine;
using UnityEngine.UI;

public class OnClick_GetBalanceAndReinstantiateOrdersSystem : MonoBehaviour
{
    PlatformComponent platformComponent;
    RetrieveOrdersComponent retrieveOrdersComponent;
    Button button;

    void Start()
    {
        platformComponent = GlobalComponent.instance.platformComponent;
        retrieveOrdersComponent = GlobalComponent.instance.retrieveOrdersComponent;
        button = GetComponent<Button>();

        button.onClick.AddListener(() =>
        {
            platformComponent.getBalance = true;
            retrieveOrdersComponent.destroyOrders = true;
            retrieveOrdersComponent.instantiateOrders = true;
        });
    }
}
