using UnityEngine;
using UnityEngine.UI;

public class EnableOrderPageSelectButtonSystem : MonoBehaviour
{
    OrderPagesComponent orderPagesComponent;
    Image image;

    OrderPagesStatusEnum status;

    void Start()
    {
        orderPagesComponent = GlobalComponent.instance.orderPagesComponent;
        image = GetComponent<Image>();
        image.enabled = status == OrderPagesStatusEnum.DETACH;
    }

    void Update()
    {
        if (status == orderPagesComponent.status) return;
        status = orderPagesComponent.status;
        image.enabled = status == OrderPagesStatusEnum.DETACH;
    }
}
