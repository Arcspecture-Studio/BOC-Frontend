using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InitializeDropdown_DisableExitSystem : MonoBehaviour
{
    TMP_Dropdown dropdown;
    void Awake()
    {
        dropdown = GetComponent<TMP_Dropdown>();

        dropdown.ClearOptions();
        List<string> array = new() {
            DisableExitEnum.APP_CONTROL.ToString(),
            DisableExitEnum.MANUAL_CONTROL.ToString()
        };
        dropdown.AddOptions(array);

        dropdown.value = 0;
    }
}