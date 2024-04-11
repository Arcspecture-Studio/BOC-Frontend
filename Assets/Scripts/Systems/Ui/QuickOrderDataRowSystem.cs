using UnityEngine;

public class QuickOrderDataRowSystem : MonoBehaviour
{
    QuickOrderDataRowComponent quickOrderDataRowComponent;
    QuickTabComponent quickTabComponent;

    void Start()
    {
        quickOrderDataRowComponent = GetComponent<QuickOrderDataRowComponent>();
        quickTabComponent = GlobalComponent.instance.quickTabComponent;

        quickOrderDataRowComponent.closeButton.onClick.AddListener(OnClick_CloseButton);
    }

    void OnClick_CloseButton()
    {
        quickTabComponent.deleteFromServer = quickOrderDataRowComponent.orderId;
        quickOrderDataRowComponent.closeButton.interactable = false;
    }
}
