using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InitializeDropdown_FundingFeeHandlerSystem : MonoBehaviour
{
    TMP_Dropdown dropdown;
    void Awake()
    {
        dropdown = GetComponent<TMP_Dropdown>();

        dropdown.ClearOptions();
        List<string> array = new() {
            FundingFeeHandlerEnum.ADJUST_STOP_LOSS.ToString(),
            FundingFeeHandlerEnum.ADJUST_TAKE_PROFIT.ToString(),
            FundingFeeHandlerEnum.DO_NOTHING.ToString()
        };
        dropdown.AddOptions(array);
    }
}