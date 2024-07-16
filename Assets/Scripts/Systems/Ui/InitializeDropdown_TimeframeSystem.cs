using TMPro;
using UnityEngine;

public class InitializeDropdown_TimeframeSystem : MonoBehaviour
{
    TMP_Dropdown dropdown;
    void Start()
    {
        dropdown = GetComponent<TMP_Dropdown>();

        dropdown.ClearOptions();
        dropdown.AddOptions(TimeframeArray.TIMEFRAME_ARRAY);
    }
}