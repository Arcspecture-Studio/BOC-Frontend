using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OrderPageInputEntryPricesSystem : MonoBehaviour
{
    OrderPageInputEntryPricesComponent orderPagetInputEntryPricesComponent;

    void Start()
    {
        orderPagetInputEntryPricesComponent = GetComponent<OrderPageInputEntryPricesComponent>();
    }
    void Update()
    {
        if (orderPagetInputEntryPricesComponent.entryPriceInputs.Count == orderPagetInputEntryPricesComponent.parent.childCount) return;

        orderPagetInputEntryPricesComponent.entryPriceInputs.Clear();
        orderPagetInputEntryPricesComponent.entryPriceCloseButtons.Clear();
        for(int i = 0; i < orderPagetInputEntryPricesComponent.parent.childCount; i++)
        {
            orderPagetInputEntryPricesComponent.entryPriceInputs.Add(orderPagetInputEntryPricesComponent.parent.GetChild(i).GetChild(0).GetComponent<TMP_InputField>());
            orderPagetInputEntryPricesComponent.entryPriceCloseButtons.Add(orderPagetInputEntryPricesComponent.parent.GetChild(i).GetChild(1).GetComponent<Button>());
        }
    }
}
