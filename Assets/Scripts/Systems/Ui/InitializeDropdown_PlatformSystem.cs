using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InitializeDropdown_PlatformSystem : MonoBehaviour
{
    TMP_Dropdown dropdown;
    void Start()
    {
        dropdown = GetComponent<TMP_Dropdown>();

        dropdown.ClearOptions();
        List<string> array = new() {
            PlatformEnum.BINANCE.ToString(),
            PlatformEnum.BINANCE_TESTNET.ToString()
        };
        dropdown.AddOptions(array);
    }
}