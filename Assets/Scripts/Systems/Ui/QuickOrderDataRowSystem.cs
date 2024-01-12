using UnityEngine;

public class QuickOrderDataRowSystem : MonoBehaviour
{
    QuickOrderDataRowComponent quickOrderDataRowComponent;

    void Start()
    {
        quickOrderDataRowComponent = GetComponent<QuickOrderDataRowComponent>();

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
}
