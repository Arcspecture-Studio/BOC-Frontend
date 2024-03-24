using UnityEngine;

public class QuickOrderDataRowSystem : MonoBehaviour
{
    QuickOrderDataRowComponent quickOrderDataRowComponent;
    WebsocketComponent websocketComponent;
    PlatformComponent platformComponent;

    void Start()
    {
        quickOrderDataRowComponent = GetComponent<QuickOrderDataRowComponent>();
        websocketComponent = GlobalComponent.instance.websocketComponent;
        platformComponent = GlobalComponent.instance.platformComponent;

        quickOrderDataRowComponent.closeButton.onClick.AddListener(OnClick_CloseButton);
    }

    void OnClick_CloseButton()
    {
        websocketComponent.generalRequests.Add(new General.WebsocketSaveQuickOrderRequest(platformComponent.activePlatform, quickOrderDataRowComponent.orderId));
        quickOrderDataRowComponent.closeButton.interactable = false;
    }
}
