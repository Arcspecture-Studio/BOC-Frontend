using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using WebSocketSharp;

public class OrderPagesWebsocketResponseSystem : MonoBehaviour
{
    WebsocketComponent websocketComponent;
    OrderPagesComponent orderPagesComponent;
    PlatformComponent platformComponent;
    PromptComponent promptComponent;

    void Start()
    {
        websocketComponent = GlobalComponent.instance.websocketComponent;
        orderPagesComponent = GlobalComponent.instance.orderPagesComponent;
        platformComponent = GlobalComponent.instance.platformComponent;
        promptComponent = GlobalComponent.instance.promptComponent;
    }
    void Update()
    {
        SubmitOrderToServerResponse();
    }

    void PositionInfoUpdateResponse()
    {
        string jsonString = websocketComponent.RetrieveGeneralResponses(WebsocketEventTypeEnum.POSITION_INFO_UPDATE);
        websocketComponent.RemovesGeneralResponses(WebsocketEventTypeEnum.POSITION_INFO_UPDATE);
        if (jsonString.IsNullOrEmpty()) return;
    }
    void SubmitOrderToServerResponse()
    {
        string jsonString = websocketComponent.RetrieveGeneralResponses(WebsocketEventTypeEnum.SUBMIT_ORDER);
        websocketComponent.RemovesGeneralResponses(WebsocketEventTypeEnum.SUBMIT_ORDER);
        if (jsonString.IsNullOrEmpty()) return;

        General.WebsocketSubmitOrderResponse response = JsonConvert.DeserializeObject
        <General.WebsocketSubmitOrderResponse>(jsonString, JsonSerializerConfig.settings);

        OrderPageComponent orderPageComponent = null;
        foreach (OrderPageComponent component in orderPagesComponent.childOrderPageComponents)
        {
            if (component.orderId == response.orderId)
            {
                orderPageComponent = component;
            }
        }
        if (orderPageComponent == null) return;

        orderPageComponent.orderStatus = response.status;
        orderPageComponent.orderStatusError = response.statusError;
        if (orderPageComponent.resultComponent.orderInfoDataObject != null)
        {
            orderPageComponent.resultComponent.orderInfoDataObject.transform.GetChild(3).GetComponent<TMP_Text>().text = orderPageComponent.orderStatus.ToString();
        }
        if (orderPageComponent.orderStatusError &&
        !response.errorJsonString.IsNullOrEmpty())
        {
            switch (platformComponent.activePlatform)
            {
                case PlatformEnum.BINANCE:
                case PlatformEnum.BINANCE_TESTNET:
                    Binance.WebrequestGeneralResponse errorResponse = JsonConvert.DeserializeObject<Binance.WebrequestGeneralResponse>(response.errorJsonString, JsonSerializerConfig.settings);
                    if (errorResponse.code.HasValue)
                    {
                        string message = errorResponse.msg + " (Binance Error Code: " + errorResponse.code.Value + ")";
                        promptComponent.ShowPrompt(PromptConstant.ERROR, message, () =>
                        {
                            promptComponent.active = false;
                        });
                    }
                    break;
            }
        }
    }
}
