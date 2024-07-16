using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InitializeDropdown_OrderTypeSystem : MonoBehaviour
{
    TMP_Dropdown dropdown;
    void Start()
    {
        dropdown = GetComponent<TMP_Dropdown>();

        dropdown.ClearOptions();
        List<string> array = new() {
            OrderTypeEnum.MARKET.ToString(),
            OrderTypeEnum.LIMIT.ToString(),
            OrderTypeEnum.CONDITIONAL.ToString()
        };
        dropdown.AddOptions(array);
    }
}