using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuickOrderDataRowComponent : MonoBehaviour
{
    [Header("Reference")]
    public TMP_Text symbolText;
    public TMP_Text positionSideText;
    public TMP_Text entryPriceText;
    public TMP_Text atrTimeframeText;
    public Button closeButton;

    [Header("Config")]
    public string orderId;
    public General.WebsocketRetrieveQuickOrdersData data;
}
