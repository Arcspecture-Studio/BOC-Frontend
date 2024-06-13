using UnityEngine;

public class SpawnQuickOrderSystem : MonoBehaviour
{
    SpawnQuickOrderComponent spawnQuickOrderComponent;
    QuickTabComponent quickTabComponent;

    void Start()
    {
        spawnQuickOrderComponent = GlobalComponent.instance.spawnQuickOrderComponent;
        quickTabComponent = GlobalComponent.instance.quickTabComponent;

        spawnQuickOrderComponent.onChange_quickOrderToSpawn.AddListener(SpawnQuickOrder);
    }

    void SpawnQuickOrder(General.WebsocketGetQuickOrderResponse response)
    {
        if (quickTabComponent.spawnedQuickOrderDataObjects.ContainsKey(response.id)) return;

        GameObject quickOrderDataRowObject = Instantiate(quickTabComponent.quickOrderDataRowPrefab);
        quickOrderDataRowObject.transform.SetParent(quickTabComponent.quickOrderDataRowParent, false);
        quickTabComponent.spawnedQuickOrderDataObjects.TryAdd(response.id, quickOrderDataRowObject);

        QuickOrderDataRowComponent quickOrderDataRowComponent = quickOrderDataRowObject.GetComponent<QuickOrderDataRowComponent>();
        quickOrderDataRowComponent.orderId = response.id;
        quickOrderDataRowComponent.setting = response.setting;
        quickOrderDataRowComponent.symbolText.text = response.setting.order.symbol;
        string positionSide = response.isLong ? "LONG" : "SHORT";
        Color positionSideColor = response.isLong ? OrderConfig.DISPLAY_COLOR_GREEN : OrderConfig.DISPLAY_COLOR_RED;
        quickOrderDataRowComponent.positionSideText.text = positionSide;
        quickOrderDataRowComponent.positionSideText.color = positionSideColor;
        quickOrderDataRowComponent.entryPriceText.text = Utils.RoundTwoDecimal(response.entryPrice).ToString();
    }
}