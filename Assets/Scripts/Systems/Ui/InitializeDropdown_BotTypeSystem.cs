using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InitializeDropdown_BotTypeSystem : MonoBehaviour
{
    TMP_Dropdown dropdown;
    void Start()
    {
        dropdown = GetComponent<TMP_Dropdown>();

        dropdown.ClearOptions();
        List<string> array = new() {
            BotTypeEnum.PREMIUM_INDEX.ToString(),
            BotTypeEnum.MCDX.ToString()
        };
        dropdown.AddOptions(array);
    }
}