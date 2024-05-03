using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuickOrderDataRowComponent : MonoBehaviour
{
    [Header("Reference")]
    public TMP_Text symbolText;
    public TMP_Text positionSideText;
    public TMP_Text entryPriceText;
    public Button infoButton;
    public Button closeButton;
    public GameObject infoPanel;
    public Transform infoPanelContent;
    public GameObject infoPanelData;

    [Header("Config")]
    public string orderId;
    public Preference setting;

    [Header("Runtime")]
    public bool infoPanelInstantiated = false;
}
