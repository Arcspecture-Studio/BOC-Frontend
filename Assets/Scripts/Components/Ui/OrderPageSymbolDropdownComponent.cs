using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OrderPageSymbolDropdownComponent : MonoBehaviour
{
    [Header("Config")]
    public TMP_Dropdown dropdown;

    [Header("Runtime")]
    public List<string> symbols;
    public string selectedSymbol;
    public long allSymbolsCount;
}