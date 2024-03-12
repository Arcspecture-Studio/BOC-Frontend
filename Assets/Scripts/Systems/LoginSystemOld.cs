using System.Collections.Generic;
using TMPro;
using UnityEngine;
using WebSocketSharp;

public class LoginSystemOld : MonoBehaviour
{
    LoginComponentOld loginComponent;
    BinanceComponent binanceComponent;
    BinanceComponent binanceTestnetComponent;
    PlatformComponent platformComponent;
    WebsocketComponent websocketComponent;
    RetrieveOrdersComponent retrieveOrdersComponent;
    PromptComponent promptComponent;
    IoComponent ioComponent;

    bool hideLoginPhrase;

    void Start()
    {
        loginComponent = GlobalComponent.instance.loginComponentOld;
        binanceComponent = GlobalComponent.instance.binanceComponent;
        binanceTestnetComponent = GlobalComponent.instance.binanceTestnetComponent;
        platformComponent = GlobalComponent.instance.platformComponent;
        websocketComponent = GlobalComponent.instance.websocketComponent;
        retrieveOrdersComponent = GlobalComponent.instance.retrieveOrdersComponent;
        promptComponent = GlobalComponent.instance.promptComponent;
        ioComponent = GlobalComponent.instance.ioComponent;

        loginComponent.gameObj.SetActive(true);
        loginComponent.platformsDropdown.onValueChanged.AddListener(value => OnPlatformValueChanged(value));
        UpdateDropdownOptions();

        loginComponent.logoutButtonObject.SetActive(false);
        loginComponent.logoutButton.onClick.AddListener(() =>
        {
            loginComponent.loggedIn = false;
            // ioComponent.writeApiKey = true;
            UpdateDropdownOptions();

            General.WebsocketLogoutRequest request = new General.WebsocketLogoutRequest(platformComponent.tradingPlatform);
            websocketComponent.generalRequests.Add(request);
        });

        hideLoginPhrase = loginComponent.loginPhraseInput.contentType == TMP_InputField.ContentType.Password;
        loginComponent.loginPhraseInput.onValueChanged.AddListener(value =>
        {
            loginComponent.loginPhraseWordCountText.text = value.Length.ToString();
        });
        loginComponent.loginPhraseVisibleButton.onClick.AddListener(() =>
        {
            hideLoginPhrase = !hideLoginPhrase;
            if (hideLoginPhrase)
            {
                loginComponent.loginPhraseInput.contentType = TMP_InputField.ContentType.Password;
                loginComponent.loginPhraseVisibleIconOn.SetActive(true);
                loginComponent.loginPhraseVisibleIconOff.SetActive(false);
            }
            else
            {
                loginComponent.loginPhraseInput.contentType = TMP_InputField.ContentType.Standard;
                loginComponent.loginPhraseVisibleIconOn.SetActive(false);
                loginComponent.loginPhraseVisibleIconOff.SetActive(true);
            }
            loginComponent.loginPhraseInput.ActivateInputField();
        });
    }
    void Update()
    {
        ChangePlatform();
    }
    void ChangePlatform()
    {
        if (!loginComponent.changePlatform) return;
        loginComponent.changePlatform = false;
        loginComponent.gameObj.SetActive(true);
        loginComponent.allowInput = true;
        UpdateDropdownOptions();
        retrieveOrdersComponent.destroyOrders = true;
    }
    void UpdateDropdownOptions()
    {
        loginComponent.platformsDropdown.ClearOptions();
        List<TMP_Dropdown.OptionData> optionDataList = new List<TMP_Dropdown.OptionData>();

        string binance = PlatformEnum.BINANCE.ToString();
        if (binanceComponent.loggedIn) binance += " (Logged In)";
        optionDataList.Add(new TMP_Dropdown.OptionData(binance));

        string binanceTestnet = PlatformEnum.BINANCE_TESTNET.ToString();
        binanceTestnet = binanceTestnet.Replace("_", " ");
        if (binanceTestnetComponent.loggedIn) binanceTestnet += " (Logged In)";
        optionDataList.Add(new TMP_Dropdown.OptionData(binanceTestnet));

        string mexc = PlatformEnum.MEXC.ToString();
        if (true) mexc += " (Coming Soon)"; // PENDING: replace with mexcComponent.loggedIn
        optionDataList.Add(new TMP_Dropdown.OptionData(mexc));

        loginComponent.platformsDropdown.AddOptions(optionDataList);
        int tradingPlatform = (int)platformComponent.tradingPlatform;
        if (loginComponent.platformsDropdown.value == tradingPlatform)
        {
            OnPlatformValueChanged(tradingPlatform);
        }
        else
        {
            loginComponent.platformsDropdown.value = tradingPlatform;
        }
    }
    void OnPlatformValueChanged(int value)
    {
        switch (value)
        {
            case ((int)PlatformEnum.BINANCE):
                platformComponent.tradingPlatform = PlatformEnum.BINANCE;
                UpdateUiBehaviour(binanceComponent.loggedIn);
                loginComponent.loginButton.interactable = true;
                loginComponent.logoutButton.interactable = true;
                break;
            case ((int)PlatformEnum.BINANCE_TESTNET):
                platformComponent.tradingPlatform = PlatformEnum.BINANCE_TESTNET;
                UpdateUiBehaviour(binanceTestnetComponent.loggedIn);
                loginComponent.loginButton.interactable = true;
                loginComponent.logoutButton.interactable = true;
                break;
            case ((int)PlatformEnum.MEXC):
                platformComponent.tradingPlatform = PlatformEnum.MEXC;
                UpdateUiBehaviour(true); // PENDING: replace with mexcComponent.loggedIn
                loginComponent.loginButton.interactable = false;
                loginComponent.logoutButton.interactable = false;
                break;
        }
        RemoveInputText();
    }
    void UpdateUiBehaviour(bool loggedIn)
    {
        loginComponent.loginPhraseObj.SetActive(!loggedIn);
        loginComponent.apiKeyObj.SetActive(!loggedIn);
        loginComponent.secretKeyObj.SetActive(!loggedIn);
        loginComponent.loginButton.onClick.RemoveAllListeners();
        loginComponent.logoutButtonObject.SetActive(loggedIn);
        if (loggedIn)
        {
            loginComponent.loginButtonText.text = "Back";
            loginComponent.loginButton.onClick.AddListener(() =>
            {
                loginComponent.gameObj.SetActive(false);

                retrieveOrdersComponent.instantiateOrders = true;
                GlobalComponent.instance.tradingBotComponent.getTradingBots = true;
            });
        }
        else
        {
            loginComponent.loginButtonText.text = "Login";
            loginComponent.loginButton.onClick.AddListener(() =>
            {
                if (loginComponent.loginPhraseInput.text.IsNullOrEmpty())
                {
                    promptComponent.ShowPrompt(PromptConstant.ERROR, "Personal Secret Login Phrase cannot be blank.", () =>
                    {
                        promptComponent.active = false;
                    });
                }
                else if (loginComponent.loginPhraseInput.text.Length < 20)
                {
                    promptComponent.ShowPrompt(PromptConstant.ERROR, "Personal Secret Login Phrase must have minimum of 20 characters.", () =>
                    {
                        promptComponent.active = false;
                    });
                }
                else if (loginComponent.apiKeyInput.text.IsNullOrEmpty() || loginComponent.secretKeyInput.text.IsNullOrEmpty())
                {
                    promptComponent.ShowPrompt(PromptConstant.ERROR, "Key(s) cannot be blank.", () =>
                    {
                        promptComponent.active = false;
                    });
                }
                else
                {
                    loginComponent.allowInput = false;

                    loginComponent.loginPhrase = loginComponent.loginPhraseInput.text;
                    platformComponent.apiKey = loginComponent.apiKeyInput.text;
                    platformComponent.apiSecret = loginComponent.secretKeyInput.text;
                    RemoveInputText();
                    websocketComponent.syncApiKeyToServer = true;
                    platformComponent.getBalance = true;
                }
            });
        }
    }
    void RemoveInputText()
    {
        loginComponent.loginPhraseInput.text = "";
        loginComponent.apiKeyInput.text = "";
        loginComponent.secretKeyInput.text = "";
    }
}
