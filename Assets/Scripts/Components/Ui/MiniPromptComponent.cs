using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class MiniPromptComponent : MonoBehaviour
{
    [Header("Reference")]
    public Animation anim;
    public TMP_Text messageText;

    public string message
    {
        set
        {
            onChange_message.Invoke(value);
        }
    }
    [HideInInspector] public UnityEvent<string> onChange_message = new();
}