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

    [Header("Config")]
    public string loginKeyword;
    public string registerKeyword;
    public string switchToRegisterKeyword;
    public string switchToLoginKeyword;

    [Header("Runtime")]
    [SerializeField] private bool _isLoginPage;
    public bool isLoginPage
    {
        set
        {
            _isLoginPage = value;
            onChange_isLoginPage.Invoke();
        }
        get { return _isLoginPage; }
    }
    [HideInInspector] public UnityEvent onChange_isLoginPage = new();
}