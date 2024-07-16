using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InitializeDropdown_MarginDistributionModeSystem : MonoBehaviour
{
    TMP_Dropdown dropdown;
    void Start()
    {
        dropdown = GetComponent<TMP_Dropdown>();

        dropdown.ClearOptions();
        List<string> array = new() {
            MarginDistributionModeEnum.EQUAL.ToString(),
            MarginDistributionModeEnum.WEIGHTED.ToString()
        };
        dropdown.AddOptions(array);
    }
}