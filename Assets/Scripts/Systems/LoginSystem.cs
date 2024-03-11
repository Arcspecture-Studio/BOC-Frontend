using UnityEngine;

public class LoginSystem : MonoBehaviour
{
    LoginComponent loginComponent;

    void Start()
    {
        loginComponent = GlobalComponent.instance.loginComponent;

        loginComponent.onChange_loginStatus.AddListener(SwitchPageBasedOnLoginStatus);

        // Set initial state
        loginComponent.pageObj.SetActive(true);
        loginComponent.loginStatus = LoginPageStatusEnum.LOGGED_IN;
        loginComponent.proceedButton.interactable = false;
    }

    void SwitchPageBasedOnLoginStatus()
    {
        loginComponent.proceedButton.interactable = true;
        loginComponent.proceedButton.onClick.RemoveAllListeners();
        loginComponent.switchButton.onClick.RemoveAllListeners();
        loginComponent.confirmPasswordObj.SetActive(loginComponent.loginStatus == LoginPageStatusEnum.REGISTER);
        switch (loginComponent.loginStatus)
        {
            case LoginPageStatusEnum.REGISTER:
                loginComponent.proceedButtonText.text = loginComponent.registerKeyword;
                loginComponent.proceedButton.onClick.AddListener(Register);
                loginComponent.switchButtonText.text = loginComponent.switchToLoginKeyword;
                loginComponent.switchButton.onClick.AddListener(SwitchToLogin);
                loginComponent.switchButtonObj.SetActive(true);
                loginComponent.emailInput.interactable = true;
                loginComponent.passwordInput.interactable = true;
                loginComponent.confirmPasswordInput.interactable = true;
                break;
            case LoginPageStatusEnum.LOGIN:
                loginComponent.proceedButtonText.text = loginComponent.loginKeyword;
                loginComponent.proceedButton.onClick.AddListener(Login);
                loginComponent.switchButtonText.text = loginComponent.switchToRegisterKeyword;
                loginComponent.switchButton.onClick.AddListener(SwitchToRegister);
                loginComponent.switchButtonObj.SetActive(true);
                loginComponent.emailInput.interactable = true;
                loginComponent.passwordInput.interactable = true;
                loginComponent.confirmPasswordInput.interactable = true;
                break;
            case LoginPageStatusEnum.LOGGED_IN:
                loginComponent.proceedButtonText.text = loginComponent.logoutKeyword;
                loginComponent.proceedButton.onClick.AddListener(Logout);
                loginComponent.switchButtonObj.SetActive(false);
                loginComponent.emailInput.interactable = false;
                loginComponent.passwordInput.interactable = false;
                loginComponent.confirmPasswordInput.interactable = false;
                loginComponent.emailInput.text = "";
                loginComponent.passwordInput.text = "";
                loginComponent.confirmPasswordInput.text = "";
                break;
        }
    }
    void SwitchToRegister()
    {
        loginComponent.loginStatus = LoginPageStatusEnum.REGISTER;
    }
    void SwitchToLogin()
    {
        loginComponent.loginStatus = LoginPageStatusEnum.LOGIN;
    }
    void Register()
    {

    }
    void Login()
    {

    }
    void Logout()
    {

    }
}
