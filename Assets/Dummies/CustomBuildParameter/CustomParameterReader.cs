using TMPro;
using UnityEditor;
using UnityEngine;

public class CustomParameterReader : MonoBehaviour
{
    public TMP_Text text;

    void Start()
    {
        var args = PlayerSettings.GetAdditionalIl2CppArgs();
        text.text = args;
        // foreach (string arg in args)
        // {
        //     text.text += "\n" + arg;
        // }
    }
}
