using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoginComponent : MonoBehaviour
{
    [Header("References")]
    public GameObject gameObj;
    public GameObject loginPhraseObj;
    public TMP_InputField loginPhraseInput;
    public TMP_Text loginPhraseWordCountText;
    public Button loginPhraseVisibleButton;
    public GameObject loginPhraseVisibleIconOn;
    public GameObject loginPhraseVisibleIconOff;
    public GameObject apiKeyObj;
    public TMP_InputField apiKeyInput;
    public GameObject secretKeyObj;
    public TMP_InputField secretKeyInput;
    public TMP_Dropdown platformsDropdown;
    public Button loginButton;
    public TMP_Text loginButtonText;
    public GameObject logoutButtonObject;
    public Button logoutButton;

    [Header("Runtime")]
    public bool changePlatform;
    public string loginPhrase
    {
        get
        {
            string data = null;
            switch (GlobalComponent.instance.platformComponent.tradingPlatform)
            {
                case PlatformEnum.BINANCE:
                    data = GlobalComponent.instance.binanceComponent.loginPhrase;
                    break;
                case PlatformEnum.BINANCE_TESTNET:
                    data = GlobalComponent.instance.binanceTestnetComponent.loginPhrase;
                    break;
                case PlatformEnum.MEXC:
                    break;
            }
            return data;
        }
        set
        {
            switch (GlobalComponent.instance.platformComponent.tradingPlatform)
            {
                case PlatformEnum.BINANCE:
                    GlobalComponent.instance.binanceComponent.loginPhrase = value;
                    break;
                case PlatformEnum.BINANCE_TESTNET:
                    GlobalComponent.instance.binanceTestnetComponent.loginPhrase = value;
                    break;
                case PlatformEnum.MEXC:
                    break;
            }
        }
    }
    public bool loggedIn
    {
        get
        {
            bool data = false;
            switch (GlobalComponent.instance.platformComponent.tradingPlatform)
            {
                case PlatformEnum.BINANCE:
                    data = GlobalComponent.instance.binanceComponent.loggedIn;
                    break;
                case PlatformEnum.BINANCE_TESTNET:
                    data = GlobalComponent.instance.binanceTestnetComponent.loggedIn;
                    break;
                case PlatformEnum.MEXC:
                    break;
            }
            return data;
        }
        set
        {
            switch (GlobalComponent.instance.platformComponent.tradingPlatform)
            {
                case PlatformEnum.BINANCE:
                    GlobalComponent.instance.binanceComponent.loggedIn = value;
                    break;
                case PlatformEnum.BINANCE_TESTNET:
                    GlobalComponent.instance.binanceTestnetComponent.loggedIn = value;
                    break;
                case PlatformEnum.MEXC:
                    break;
            }
        }
    }
    public bool allowInput
    {
        get
        {
            return loginButton.interactable && platformsDropdown.interactable && loginPhraseInput.interactable &&
                loginPhraseVisibleButton.interactable && apiKeyInput.interactable && secretKeyInput.interactable;
        }
        set
        {
            loginButton.interactable = value;
            platformsDropdown.interactable = value;
            loginPhraseInput.interactable = value;
            loginPhraseVisibleButton.interactable = value;
            apiKeyInput.interactable = value;
            secretKeyInput.interactable = value;
        }
    }
}