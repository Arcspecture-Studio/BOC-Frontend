using System;
using TMPro;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public TMP_Text text;

    void Start()
    {
        string[] args = Environment.GetCommandLineArgs();
        string arg = "";
        for (int i = 0; i < args.Length; i++)
        {
            arg += args[i] + " || ";
        }
        text.text = arg;
    }
}
