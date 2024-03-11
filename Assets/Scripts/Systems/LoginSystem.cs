using UnityEngine;

public class LoginSystem : MonoBehaviour
{
    LoginComponent loginComponent;

    void Start()
    {
        loginComponent = GlobalComponent.instance.loginComponent;

        SwitchBetweenLoginAndRegister();
        loginComponent.onChange_isLoginPage.AddListener(SwitchBetweenLoginAndRegister);
    }

    void SwitchBetweenLoginAndRegister()
    {
        loginComponent.proceedButton.onClick.RemoveAllListeners();
        loginComponent.switchButton.onClick.RemoveAllListeners();
        loginComponent.confirmPasswordObj.SetActive(!loginComponent.isLoginPage);
        if (loginComponent.isLoginPage)
        {
            loginComponent.proceedButtonText.text = loginComponent.loginKeyword;
            loginComponent.proceedButton.onClick.AddListener(Login);
            loginComponent.switchButtonText.text = loginComponent.switchToRegisterKeyword;
            loginComponent.switchButton.onClick.AddListener(SwitchToRegister);
        }
        else
        {
            loginComponent.proceedButtonText.text = loginComponent.registerKeyword;
            loginComponent.proceedButton.onClick.AddListener(Register);
            loginComponent.switchButtonText.text = loginComponent.switchToLoginKeyword;
            loginComponent.switchButton.onClick.AddListener(SwitchToLogin);
        }
    }
    void Login()
    {

    }
    void SwitchToRegister()
    {
        loginComponent.isLoginPage = false;
    }
    void Register()
    {

    }
    void SwitchToLogin()
    {
        loginComponent.isLoginPage = true;
    }
}
