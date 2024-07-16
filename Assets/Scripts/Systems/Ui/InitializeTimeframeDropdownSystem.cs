using TMPro;
using UnityEngine;

public class InitializeTimeframeDropdownSystem : MonoBehaviour
{
    TMP_Dropdown dropdown;
    void Start()
    {
        dropdown = GetComponent<TMP_Dropdown>();

        dropdown.AddOptions(TimeframeArray.TIMEFRAME_ARRAY);
    }
}