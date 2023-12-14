using UnityEngine;
using UnityEngine.UI;

public class OnClick_SpawnOrderPageSystem : MonoBehaviour
{
    OrderPagesComponent orderPagesComponent;
    Button button;

    void Start()
    {
        orderPagesComponent = GlobalComponent.instance.orderPagesComponent;
        button = GetComponent<Button>();

        button.onClick.AddListener(() =>
        {
            orderPagesComponent.status = OrderPagesStatusEnum.IMMERSIVE;
            orderPagesComponent.currentPageIndex = orderPagesComponent.transform.childCount;
            GameObject orderPageObject = Instantiate(orderPagesComponent.orderPagePrefab);
            orderPageObject.transform.SetParent(orderPagesComponent.transform, false);
        });
    }
}
