using TMPro;
using UnityEditor;
using UnityEditor.Build;
using UnityEngine;

public class CustomParameterReader : MonoBehaviour
{
    public TMP_Text text;

    void Start()
    {
        var args = PlayerSettings.GetAdditionalCompilerArguments(NamedBuildTarget.Android);
        foreach (string arg in args)
        {
            text.text += "\n" + arg;
        }
    }
}
