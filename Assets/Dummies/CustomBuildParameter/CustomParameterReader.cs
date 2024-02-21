using TMPro;
using UnityEngine;

public class CustomParameterReader : MonoBehaviour
{
    public TMP_Text text;

    void Start()
    {
        var args = System.Environment.GetCommandLineArgs();
        foreach (string arg in args)
        {
            text.text += "\n" + arg;
        }
    }
}
