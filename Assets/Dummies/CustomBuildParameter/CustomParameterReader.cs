using TMPro;
using UnityEngine;

public class CustomParameterReader : MonoBehaviour
{
    public TMP_Text text;

    void Start()
    {
        // var args = System.Environment.GetEnvironmentVariables();
        // // text.text = args;
        // foreach (DictionaryEntry entry in args)
        // {
        //     text.text += $"{entry.Key} ---> {entry.Value}\n";
        // }

        text.text = SecretConfig.ENCRYPTION_ACCESS_TOKEN_32;
    }
}
