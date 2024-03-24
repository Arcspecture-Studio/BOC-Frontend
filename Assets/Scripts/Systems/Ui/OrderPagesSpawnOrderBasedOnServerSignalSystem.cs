using Newtonsoft.Json;
using UnityEngine;
using WebSocketSharp;

public class OrderPagesSpawnOrderBasedOnServerSignalSystem : MonoBehaviour
{
    WebsocketComponent websocketComponent;
    OrderPagesComponent orderPagesComponent;
    HideAllPanelComponent hideAllPanelComponent;

    void Start()
    {
        websocketComponent = GlobalComponent.instance.websocketComponent;
        orderPagesComponent = GlobalComponent.instance.orderPagesComponent;
        hideAllPanelComponent = GlobalComponent.instance.hideAllPanelComponent;
    }
    void Update()
    {
        string spawnOrderString = websocketComponent.RetrieveGeneralResponses(WebsocketEventTypeEnum.SPAWN_ORDER);
        if (spawnOrderString.IsNullOrEmpty()) return;
        General.WebsocketSpawnOrderResponse response = JsonConvert.DeserializeObject<General.WebsocketSpawnOrderResponse>(spawnOrderString, JsonSerializerConfig.settings);
        websocketComponent.RemovesGeneralResponses(WebsocketEventTypeEnum.SPAWN_ORDER);

        if (response.order == null) // destroy
        {
            foreach (OrderPageComponent orderPageComponent in orderPagesComponent.childOrderPageComponents)
            {
                if (orderPageComponent.orderId == response.orderId)
                {
                    orderPageComponent.destroySelf = true;
                    return;
                }
            }
        }
        else // spawn
        {
            #region Set order pages status and page index
            // orderPagesComponent.status = OrderPagesStatusEnum.IMMERSIVE;
            // orderPagesComponent.currentPageIndex = orderPagesComponent.transform.childCount;
            #endregion

            // TODO: Spawn order

            #region Hide all tabs
            hideAllPanelComponent.hideNow = "true";
            #endregion
        }
    }
}