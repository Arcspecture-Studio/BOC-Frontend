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
        RestoreData();
    }

    void RestoreData()
    {
        quickOrderDataRowComponent.symbolText.text = quickOrderDataRowComponent.data.symbol.ToUpper();
        string positionSide = quickOrderDataRowComponent.data.isLong ? "LONG" : "SHORT";
        Color positionSideColor = quickOrderDataRowComponent.data.isLong ? OrderConfig.DISPLAY_COLOR_GREEN : Color.red;
        quickOrderDataRowComponent.positionSideText.text = positionSide;
        quickOrderDataRowComponent.positionSideText.color = positionSideColor;
        quickOrderDataRowComponent.entryPriceText.text = Utils.RoundTwoDecimal(quickOrderDataRowComponent.data.entryPrice).ToString();
        quickOrderDataRowComponent.atrTimeframeText.text = quickOrderDataRowComponent.data.atrInterval;
    }
    void OnClick_CloseButton()
    {
        websocketComponent.generalRequests.Add(new General.WebsocketSaveQuickOrderRequest(platformComponent.tradingPlatform, quickOrderDataRowComponent.orderId));
    }
}
