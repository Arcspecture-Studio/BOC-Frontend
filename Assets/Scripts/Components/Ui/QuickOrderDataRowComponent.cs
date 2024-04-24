using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuickOrderDataRowComponent : MonoBehaviour
{
    [Header("Reference")]
    public TMP_Text orderIdText;
    public TMP_Text positionSideText;
    public TMP_Text entryPriceText;
    public Button closeButton;

    [Header("Config")]
    public string orderId;
}
