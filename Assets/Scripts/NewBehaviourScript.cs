using System;
using TMPro;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public bool a;
    public TMP_Text text;

    void Start()
    {
        if (a)
        {
            text.text = Environment.GetEnvironmentVariable("WEBSOCKET_SERVER_HOST") ?? "WEBSOCKET_SERVER_HOST";
        }
        else
        {
            text.text = Environment.GetEnvironmentVariable("ENCRYPTION_ACCESS_TOKEN_32") ?? "ENCRYPTION_ACCESS_TOKEN_32";
        }
    }
}
