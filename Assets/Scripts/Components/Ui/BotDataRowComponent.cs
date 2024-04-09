using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BotDataRowComponent : MonoBehaviour
{
    [Header("Reference")]
    public TMP_Text symbolText;
    public TMP_Text botTypeText;
    public TMP_Text autoDestroyOrderText;
    public Button infoButton;
    public Button closeButton;

    [Header("Config")]
    public string botId;
}
