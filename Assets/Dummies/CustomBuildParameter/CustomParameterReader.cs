using TMPro;
using UnityEngine;

public class CustomParameterReader : MonoBehaviour
{
    public TMP_Text text;

    void Start()
    {
        var arg = System.Environment.GetEnvironmentVariable("KIMI_NO_NAWA");
        text.text = arg;
    }
}
