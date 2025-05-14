using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InitializeDropdown_StopLossTypeSystem : MonoBehaviour
{
    TMP_Dropdown dropdown;
    void Awake()
    {
        dropdown = GetComponent<TMP_Dropdown>();

        dropdown.ClearOptions();
        List<string> array = new() {
            StopLossTypeEnum.PRICE.ToString(),
            StopLossTypeEnum.ATR.ToString(),
        };
        dropdown.AddOptions(array);
    }
}