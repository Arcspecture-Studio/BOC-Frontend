using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class AddPlatformComponent : MonoBehaviour
{
    [Header("Reference")]
    public TMP_Dropdown platformsDropdown;
    public TMP_InputField apiKeyInput;
    public GameObject apiKeyObj;
    public TMP_InputField apiSecretInput;
    public GameObject apiSecretObj;
    public Button proceedButton;
    public TMP_Text proceedButtonText;
    public Button backButton;
    public GameObject backButtonObj;

    [Header("Runtime")]
    [HideInInspector] public UnityEvent onEnable = new();

    public void OnEnable()
    {
        onEnable.Invoke();
    }
}