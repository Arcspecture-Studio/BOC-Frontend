using UnityEngine;
using UnityEngine.UI;

public class OnClick_SpawnOrderPageSystem : MonoBehaviour
{
    OrderPagesComponent orderPagesComponent;
    HideAllPanelComponent hideAllPanelComponent;
    Button button;

    void Start()
    {
        orderPagesComponent = GlobalComponent.instance.orderPagesComponent;
        hideAllPanelComponent = GlobalComponent.instance.hideAllPanelComponent;
        button = GetComponent<Button>();

        button.onClick.AddListener(() =>
        {
            hideAllPanelComponent.hideNow = "true";

            orderPagesComponent.status = OrderPagesStatusEnum.IMMERSIVE;
            orderPagesComponent.currentPageIndex = orderPagesComponent.transform.childCount;
            GameObject orderPageObject = Instantiate(orderPagesComponent.orderPagePrefab);
            orderPageObject.transform.SetParent(orderPagesComponent.transform, false);
        });
    }
}
