using Newtonsoft.Json;
using UnityEngine;
using WebSocketSharp;

public class WebsocketGeneralResponsesCleanUpSystem : MonoBehaviour
{
    WebsocketComponent websocketComponent;
    OrderPagesComponent orderPagesComponent;

    void Start()
    {
        websocketComponent = GlobalComponent.instance.websocketComponent;
        orderPagesComponent = GlobalComponent.instance.orderPagesComponent;

        // TODO: after moved delete this system
    }
    void Update()
    {
        RetrievePositionInfo();
        SaveThrottleOrder();
    }

    void RetrievePositionInfo()
    {
        string retrievePositionInfoString = websocketComponent.RetrieveGeneralResponses(WebsocketEventTypeEnum.POSITION_INFO_UPDATE);
        if (retrievePositionInfoString.IsNullOrEmpty()) return;
        General.WebsocketPositionInfoUpdateResponse response = JsonConvert.DeserializeObject<General.WebsocketPositionInfoUpdateResponse>(retrievePositionInfoString, JsonSerializerConfig.settings);

        bool orderExist = false;
        orderPagesComponent.childOrderPageComponents.ForEach(orderPageComponent =>
        {
            if (response.orderId.Equals(orderPageComponent.orderId))
            {
                orderExist = true;
            }
        });
        if (!orderExist)
        {
            websocketComponent.RemovesGeneralResponses(WebsocketEventTypeEnum.POSITION_INFO_UPDATE);
        }
    }
    void SaveThrottleOrder()
    {
        string saveThrottleOrderString = websocketComponent.RetrieveGeneralResponses(WebsocketEventTypeEnum.SAVE_THROTTLE_ORDER);
        if (saveThrottleOrderString.IsNullOrEmpty()) return;
        General.WebsocketSaveThrottleOrderResponse response = JsonConvert.DeserializeObject<General.WebsocketSaveThrottleOrderResponse>(saveThrottleOrderString, JsonSerializerConfig.settings);

        bool orderExist = false;
        orderPagesComponent.childOrderPageComponents.ForEach(orderPageComponent =>
        {
            orderPageComponent.throttleParentComponent.orderPageThrottleComponents.ForEach(orderPageThrottleComponent =>
            {
                if (response.orderId.Equals(orderPageThrottleComponent.orderId) && response.parentOrderId.Equals(orderPageComponent.orderId))
                {
                    orderExist = true;
                }
            });
        });
        if (!orderExist)
        {
            websocketComponent.RemovesGeneralResponses(WebsocketEventTypeEnum.SAVE_THROTTLE_ORDER);
        }
    }
}
