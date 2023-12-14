using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OrderPageInputEntryPricesComponent : MonoBehaviour
{
    [Header("Reference")]
    public GameObject priceInput;

    [Header("Config")]
    public Transform parent;
    public Button addButtons;

    [Header("Runtime")]
    public List<TMP_InputField> entryPriceInputs;
    public List<Button> entryPriceCloseButtons;
}
