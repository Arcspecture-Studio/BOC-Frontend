using System.Collections.Generic;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using WebSocketSharp;

public class PlatformSystem : MonoBehaviour
{
    PlatformComponent platformComponent;
    PlatformComponentOld platformComponentOld;
    WebsocketComponent websocketComponent;
    LoginComponent loginComponent;
    PromptComponent promptComponent;

    void Start()
    {
        platformComponent = GlobalComponent.instance.platformComponent;
        platformComponentOld = GlobalComponent.instance.platformComponentOld;
        websocketComponent = GlobalComponent.instance.websocketComponent;
        loginComponent = GlobalComponent.instance.loginComponent;
        promptComponent = GlobalComponent.instance.promptComponent;

        // Set initial state
        platformComponent.gameObject.SetActive(false);
        platformComponent.onEnable.AddListener(() => OnComponentEnable());
        platformComponent.platformsDropdown.onValueChanged.AddListener(value => UpdateObjectState());
        platformComponent.proceedButton.onClick.AddListener(() => AddOrRemovePlatform());
        InitializePlatformDropdownOptions();
    }
    void Update()
    {
        AddPlatformResponse();
    }
    void OnComponentEnable()
    {
        if (platformComponent == null) return;
        UpdateObjectState();
    }
    void InitializePlatformDropdownOptions()
    {
        if (platformComponent.platformsDropdown.options.Count > 0) return;
        List<TMP_Dropdown.OptionData> optionDataList = new()
        {
            new TMP_Dropdown.OptionData(PlatformEnum.BINANCE.ToString()),
            new TMP_Dropdown.OptionData(PlatformEnum.BINANCE_TESTNET.ToString())
        };
        platformComponent.platformsDropdown.options = optionDataList;
    }
    void UpdateObjectState()
    {
        platformComponent.backButtonObj.SetActive(platformComponentOld.activePlatform > PlatformEnum.NONE);
        for (int i = 0; i < platformComponent.platformsDropdown.options.Count; i++)
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
                platformComponent.platformsDropdown.options[i].text = platformEnum.ToString() + " (" + PromptConstant.CONNECTED + ")";
                if (platformComponent.platformsDropdown.value == i)
                {
                    platformComponent.apiKeyObj.SetActive(false);
                    platformComponent.apiSecretObj.SetActive(false);
                    platformComponent.proceedButtonText.text = PromptConstant.DISCONNECT;
                }
            }
            else
            {
                platformComponent.platformsDropdown.options[i].text = platformEnum.ToString();
                if (platformComponent.platformsDropdown.value == i)
                {
                    ClearInput();
                    platformComponent.apiKeyObj.SetActive(true);
                    platformComponent.apiSecretObj.SetActive(true);
                    platformComponent.proceedButtonText.text = PromptConstant.CONNECT;
                }
            }
        }
        platformComponent.platformsDropdown.captionText.text = platformComponent.platformsDropdown.options[platformComponent.platformsDropdown.value].text;
    }
    bool InvalidateInput()
    {
        if (platformComponent.apiKeyInput.text.IsNullOrEmpty() ||
            platformComponent.apiSecretInput.text.IsNullOrEmpty()
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
    void AllowForInteraction(bool yes)
    {
        platformComponent.platformsDropdown.interactable = yes;
        platformComponent.apiKeyInput.interactable = yes;
        platformComponent.apiSecretInput.interactable = yes;
        platformComponent.proceedButton.interactable = yes;
        platformComponent.backButton.interactable = yes;
    }
    void ClearInput()
    {
        platformComponent.apiKeyInput.text = "";
        platformComponent.apiSecretInput.text = "";
    }
    void AddOrRemovePlatform()
    {
        if (InvalidateInput()) return;

        PlatformEnum selectedPlatform = (PlatformEnum)platformComponent.platformsDropdown.value;

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
                platformComponent.apiKeyInput.text,
                platformComponent.apiSecretInput.text,
                selectedPlatform
            );
        }
        else
        {
            request = new General.WebsocketRemovePlatformRequest(loginComponent.token, selectedPlatform);
        }
        websocketComponent.generalRequests.Add(request);

        AllowForInteraction(false);
    }
    void AddPlatformResponse()
    {
        string jsonString = websocketComponent.RetrieveGeneralResponses(WebsocketEventTypeEnum.ADD_PLATFORM);
        if (jsonString.IsNullOrEmpty()) return;
        websocketComponent.RemovesGeneralResponses(WebsocketEventTypeEnum.ADD_PLATFORM);
        General.WebsocketAddOrRemovePlatformResponse response = JsonConvert.DeserializeObject
        <General.WebsocketAddOrRemovePlatformResponse>(jsonString, JsonSerializerConfig.settings);

        AllowForInteraction(true);
        if (!response.success)
        {
            ClearInput();
            promptComponent.ShowPrompt(PromptConstant.ERROR, response.message, () =>
            {
                promptComponent.active = false;
            });
            return;
        }

        switch (response.platform)
        {
            case PlatformEnum.BINANCE:
                GlobalComponent.instance.binanceComponent.loggedIn = true;
                break;
            case PlatformEnum.BINANCE_TESTNET:
                GlobalComponent.instance.binanceTestnetComponent.loggedIn = true;
                break;
        }
        UpdateObjectState();
    }
}