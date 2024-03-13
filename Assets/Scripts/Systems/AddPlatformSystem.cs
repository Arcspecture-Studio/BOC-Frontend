using System.Collections.Generic;
using TMPro;
using UnityEngine;
using WebSocketSharp;

public class AddPlatformSystem : MonoBehaviour
{
    AddPlatformComponent addPlatformComponent;
    PlatformComponent platformComponent;
    WebsocketComponent websocketComponent;
    LoginComponent loginComponent;
    PromptComponent promptComponent;

    void Start()
    {
        addPlatformComponent = GlobalComponent.instance.addPlatformComponent;
        platformComponent = GlobalComponent.instance.platformComponent;
        websocketComponent = GlobalComponent.instance.websocketComponent;
        loginComponent = GlobalComponent.instance.loginComponent;
        promptComponent = GlobalComponent.instance.promptComponent;

        // Set initial state
        addPlatformComponent.gameObject.SetActive(false);
        addPlatformComponent.onEnable.AddListener(() => OnComponentEnable());
        addPlatformComponent.platformsDropdown.onValueChanged.AddListener(value => UpdatePageStatus());
        addPlatformComponent.proceedButton.onClick.AddListener(() => AddOrRemovePlatform());
        InitializePlatformDropdownOptions();
    }
    void OnComponentEnable()
    {
        if (addPlatformComponent == null) return;
        UpdatePageStatus();
    }
    void InitializePlatformDropdownOptions()
    {
        if (addPlatformComponent.platformsDropdown.options.Count > 0) return;
        List<TMP_Dropdown.OptionData> optionDataList = new()
        {
            new TMP_Dropdown.OptionData(PlatformEnum.BINANCE.ToString()),
            new TMP_Dropdown.OptionData(PlatformEnum.BINANCE_TESTNET.ToString())
        };
        addPlatformComponent.platformsDropdown.options = optionDataList;
    }
    void UpdatePageStatus()
    {
        addPlatformComponent.backButtonObj.SetActive(platformComponent.activePlatform > PlatformEnum.NONE);
        for (int i = 0; i < addPlatformComponent.platformsDropdown.options.Count; i++)
        {
            PlatformEnum platformEnum = (PlatformEnum)i;
            BinanceComponent binanceComponent = GlobalComponent.instance.binanceComponent;
            switch (platformEnum)
            {
                case PlatformEnum.BINANCE_TESTNET:
                    binanceComponent = GlobalComponent.instance.binanceTestnetComponent;
                    break;
            }

            if (binanceComponent.loggedIn)
            {
                addPlatformComponent.platformsDropdown.options[i].text = platformEnum.ToString() + " (" + PromptConstant.CONNECTED + ")";
                if (addPlatformComponent.platformsDropdown.value == i)
                {
                    addPlatformComponent.apiKeyObj.SetActive(false);
                    addPlatformComponent.apiSecretObj.SetActive(false);
                    addPlatformComponent.proceedButtonText.text = PromptConstant.DISCONNECT;
                }
            }
            else
            {
                addPlatformComponent.platformsDropdown.options[i].text = platformEnum.ToString();
                if (addPlatformComponent.platformsDropdown.value == i)
                {
                    addPlatformComponent.apiKeyInput.text = "";
                    addPlatformComponent.apiKeyObj.SetActive(true);
                    addPlatformComponent.apiSecretInput.text = "";
                    addPlatformComponent.apiSecretObj.SetActive(true);
                    addPlatformComponent.proceedButtonText.text = PromptConstant.CONNECT;
                }
            }
        }
        addPlatformComponent.platformsDropdown.captionText.text = addPlatformComponent.platformsDropdown.options[addPlatformComponent.platformsDropdown.value].text;
    }
    bool InvalidateInput()
    {
        if (addPlatformComponent.apiKeyInput.text.IsNullOrEmpty() ||
            addPlatformComponent.apiSecretInput.text.IsNullOrEmpty()
        )
        {
            promptComponent.ShowPrompt(PromptConstant.ERROR, PromptConstant.API_KEY_EMPTY, () =>
            {
                promptComponent.active = false;
            });

            return true;
        }
        return false;
    }
    void AddOrRemovePlatform()
    {
        if (InvalidateInput()) return;

        PlatformEnum selectedPlatform = (PlatformEnum)addPlatformComponent.platformsDropdown.value;

        bool add = true;
        switch (selectedPlatform)
        {
            case PlatformEnum.BINANCE:
            case PlatformEnum.BINANCE_TESTNET:
                BinanceComponent binanceComponent = selectedPlatform == PlatformEnum.BINANCE ?
                GlobalComponent.instance.binanceComponent : GlobalComponent.instance.binanceTestnetComponent;
                add = !binanceComponent.loggedIn;
                break;
        }

        General.WebsocketGeneralRequest request;
        if (add)
        {
            request = new General.WebsocketAddPlatformRequest(
                loginComponent.token,
                addPlatformComponent.apiKeyInput.text,
                addPlatformComponent.apiSecretInput.text,
                selectedPlatform
            );
        }
        else
        {
            request = new General.WebsocketRemovePlatformRequest(loginComponent.token, selectedPlatform);
        }
        websocketComponent.generalRequests.Add(request);
    }
}