using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InitializeDropdown_TakeProfitTypeSystem : MonoBehaviour
{
    [SerializeField] bool showNone = true;

    TMP_Dropdown dropdown;
    void Awake()
    {
        dropdown = GetComponent<TMP_Dropdown>();

        dropdown.ClearOptions();
        List<string> array = new() {
            TakeProfitTypeEnum.NONE.ToString(),
            TakeProfitTypeEnum.TAKE_IMMEDIATELY.ToString(),
            TakeProfitTypeEnum.TRAILING.ToString()
        };
        List<string> array2 = new() {
            TakeProfitTypeEnum.TAKE_IMMEDIATELY.ToString(),
            TakeProfitTypeEnum.TRAILING.ToString()
        };
        if (showNone) dropdown.AddOptions(array);
        else dropdown.AddOptions(array2);
    }
}