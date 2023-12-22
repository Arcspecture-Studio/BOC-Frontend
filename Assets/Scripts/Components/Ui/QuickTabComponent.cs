using DG.Tweening;
using TMPro;
using UnityEngine;

public class QuickTabComponent : MonoBehaviour
{
    [Header("Reference")]
    public RectTransform rectTransform;
    public TMP_InputField entryPriceInput;
    public TMP_InputField entryTimesInput;
    public TMP_Dropdown atrTimeframeDropdown;
    public TMP_InputField atrLengthInput;
    public TMP_InputField atrMultiplierInput;
    public GameObject quickOrdersObject;
    public Transform orderInfoTransform;
    public GameObject quickOrderDataRowPrefab;

    [Header("Config")]
    public float pageMoveDuration;
    public Ease pageMoveEase;

    [Header("Runtime")]
    public bool active = false;
}