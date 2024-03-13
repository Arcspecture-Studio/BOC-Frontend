using UnityEngine;
using WebSocketSharp;

public class LoginSystem : MonoBehaviour
{
    LoginComponent loginComponent;
    PromptComponent promptComponent;
    WebsocketComponent websocketComponent;

    void Start()
    {
        loginComponent = GlobalComponent.instance.loginComponent;
        promptComponent = GlobalComponent.instance.promptComponent;
        websocketComponent = GlobalComponent.instance.websocketComponent;

        loginComponent.onChange_loginStatus.AddListener(SwitchPageBasedOnLoginStatus);

        // Set initial state
        loginComponent.pageObj.SetActive(true);
        loginComponent.loginStatus = LoginPageStatusEnum.LOGGED_IN;
        AllowForInteraction(false);
    }

    void AllowForInteraction(bool yes)
    {
        loginComponent.emailInput.interactable = yes;
        loginComponent.passwordInput.interactable = yes;
        loginComponent.confirmPasswordInput.interactable = yes;
        loginComponent.proceedButton.interactable = yes;
        loginComponent.switchButton.interactable = yes;
    }
    bool InvalidateInput()
    {
        if (loginComponent.emailInput.text.IsNullOrEmpty() ||
            !loginComponent.emailInput.text.Contains("@") ||
            !loginComponent.emailInput.text.Contains(".") ||
            loginComponent.passwordInput.text.IsNullOrEmpty() ||
            (loginComponent.loginStatus == LoginPageStatusEnum.REGISTER && loginComponent.confirmPasswordInput.text.IsNullOrEmpty())
        )
        {
            string message = "Password can't be empty.";
            if (loginComponent.emailInput.text.IsNullOrEmpty())
            {
                message = "Email can't be empty.";
            }
            else if (!loginComponent.emailInput.text.Contains("@") ||
                    !loginComponent.emailInput.text.Contains("."))
            {
                message = "Email format invalid";
            }
            promptComponent.ShowPrompt(PromptConstant.ERROR, message, () =>
            {
                promptComponent.active = false;
            });

            return true;
        }
        return false;
    }
    void SwitchPageBasedOnLoginStatus()
    {
        AllowForInteraction(true);
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
        if (InvalidateInput()) return;
        if (loginComponent.passwordInput.text == loginComponent.confirmPasswordInput.text)
        {
            General.WebsocketAccountRequest request = new(WebsocketEventTypeEnum.CREATE_ACCOUNT, loginComponent.emailInput.text, loginComponent.passwordInput.text);
            websocketComponent.generalRequests.Add(request);

            AllowForInteraction(false);
        }
        else
        {
            promptComponent.ShowPrompt(PromptConstant.ERROR, PromptConstant.PASSWORD_MISMATCH, () =>
            {
                promptComponent.active = false;

                loginComponent.passwordInput.text = "";
                loginComponent.confirmPasswordInput.text = "";
            });
        }
    }
    void Login()
    {
        if (InvalidateInput()) return;

        General.WebsocketAccountRequest request = new(WebsocketEventTypeEnum.GET_JWT, loginComponent.emailInput.text, loginComponent.passwordInput.text);
        websocketComponent.generalRequests.Add(request);

        AllowForInteraction(false);
    }
    void Logout()
    {

    }
}
