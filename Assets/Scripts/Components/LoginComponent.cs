using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LoginComponent : MonoBehaviour
{
    [Header("Reference")]
    public GameObject pageObj;
    public TMP_InputField emailInput;
    public TMP_InputField passwordInput;
    public TMP_InputField confirmPasswordInput;
    public GameObject confirmPasswordObj;
    public Button proceedButton;
    public TMP_Text proceedButtonText;
    public Button switchButton;
    public TMP_Text switchButtonText;
    public GameObject switchButtonObj;

    [Header("Config")]
    public string loginKeyword;
    public string registerKeyword;
    public string switchToRegisterKeyword;
    public string switchToLoginKeyword;
    public string logoutKeyword;

    [Header("Runtime")]
    [SerializeField] private LoginPageStatusEnum _loginStatus;
    public LoginPageStatusEnum loginStatus
    {
        set
        {
            _loginStatus = value;
            onChange_loginStatus.Invoke();
        }
        get { return _loginStatus; }
    }
    [HideInInspector] public UnityEvent onChange_loginStatus = new();
    public string token;
}