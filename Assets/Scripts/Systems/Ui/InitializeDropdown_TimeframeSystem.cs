using TMPro;
using UnityEngine;

public class InitializeDropdown_TimeframeSystem : MonoBehaviour
{
    TMP_Dropdown dropdown;
    void Awake()
    {
        dropdown = GetComponent<TMP_Dropdown>();

        dropdown.ClearOptions();
        dropdown.AddOptions(TimeframeArray.TIMEFRAME_ARRAY);
    }
}