using TMPro;
using UnityEngine;

public class QuickOrderDataRowComponent : MonoBehaviour
{
    [Header("Reference")]
    public TMP_Text symbolText;
    public TMP_Text positionSideText;
    public TMP_Text entryPriceText;
    public TMP_Text atrTimeframeText;

    [Header("Config")]
    public string orderId;
    public General.WebsocketRetrieveQuickOrdersData data;
}
