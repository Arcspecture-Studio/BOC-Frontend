using TMPro;
using UnityEngine;

public class CustomParameterReader : MonoBehaviour
{
    public TMP_Text text;

    void Start()
    {
        string[] args = System.Environment.GetCommandLineArgs();
        for (int i = 0; i < args.Length; i++)
        {
            text.text += "\n" + i + ": " + args[i];
        }
    }
}
