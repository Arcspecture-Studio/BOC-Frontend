using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using WebSocketSharp;

public class PlatformSystem : MonoBehaviour
{
    PlatformComponent platformComponent;
    WebsocketComponent websocketComponent;
    LoginComponent loginComponent;
    PromptComponent promptComponent;

    void Start()
    {
        platformComponent = GlobalComponent.instance.platformComponent;
        websocketComponent = GlobalComponent.instance.websocketComponent;
        loginComponent = GlobalComponent.instance.loginComponent;
        promptComponent = GlobalComponent.instance.promptComponent;

        // Set initial state
        platformComponent.gameObject.SetActive(false);
        platformComponent.onEnable.AddListener(() => OnComponentEnable());
        platformComponent.platformsDropdown.onValueChanged.AddListener(value =>
        {
            UpdateObjectState();
            UpdateActivePlatformOnServer();
        });
        platformComponent.proceedButton.onClick.AddListener(() => AddOrRemovePlatform());
        platformComponent.backButton.onClick.AddListener(() => platformComponent.gameObject.SetActive(false));
        InitializePlatformDropdownOptions();
    }
    void Update()
    {
        AddPlatformResponse();
        RemovePlatformResponse();
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
        for (int i = 0; i < platformComponent.platformsDropdown.options.Count; i++)
        {
            PlatformEnum platformEnum = (PlatformEnum)i;
            PlatformTemplateComponent platformTemplateComponent = GlobalComponent.instance.binanceComponent;
            switch (platformEnum)
            {
                case PlatformEnum.BINANCE_TESTNET:
                    platformTemplateComponent = GlobalComponent.instance.binanceTestnetComponent;
                    break;
            }

            if (platformTemplateComponent.loggedIn)
            {
                platformComponent.platformsDropdown.options[i].text = platformEnum.ToString() + " (" + PromptConstant.CONNECTED + ")";
                if (platformComponent.platformsDropdown.value == i)
                {
                    platformComponent.backButtonObj.SetActive(true);
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
                    platformComponent.backButtonObj.SetActive(false);
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
        bool add = true;
        switch (platformComponent.activePlatform)
        {
            case PlatformEnum.BINANCE:
            case PlatformEnum.BINANCE_TESTNET:
                BinanceComponent binanceComponent = platformComponent.activePlatform == PlatformEnum.BINANCE ?
                GlobalComponent.instance.binanceComponent : GlobalComponent.instance.binanceTestnetComponent;
                add = !binanceComponent.loggedIn;
                break;
        }

        General.WebsocketGeneralRequest request;
        if (add)
        {
            if (InvalidateInput()) return;
            request = new General.WebsocketAddPlatformRequest(
                loginComponent.token,
                platformComponent.apiKeyInput.text,
                platformComponent.apiSecretInput.text,
                platformComponent.activePlatform
            );
        }
        else
        {
            request = new General.WebsocketRemovePlatformRequest(loginComponent.token, platformComponent.activePlatform);
        }
        websocketComponent.generalRequests.Add(request);

        AllowForInteraction(false);
    }
    void AddPlatformResponse()
    {
        string jsonString = websocketComponent.RetrieveGeneralResponses(WebsocketEventTypeEnum.ADD_PLATFORM);
        if (jsonString.IsNullOrEmpty()) return;
        websocketComponent.RemovesGeneralResponses(WebsocketEventTypeEnum.ADD_PLATFORM);

        HandleResponse(jsonString, true);
    }
    void RemovePlatformResponse()
    {
        string jsonString = websocketComponent.RetrieveGeneralResponses(WebsocketEventTypeEnum.REMOVE_PLATFORM);
        if (jsonString.IsNullOrEmpty()) return;
        websocketComponent.RemovesGeneralResponses(WebsocketEventTypeEnum.REMOVE_PLATFORM);

        HandleResponse(jsonString, false);
    }
    void HandleResponse(string jsonString, bool loggedIn)
    {
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

        PlatformTemplateComponent platformTemplateComponent = GlobalComponent.instance.binanceComponent;
        switch (response.platform)
        {
            case PlatformEnum.BINANCE_TESTNET:
                platformTemplateComponent = GlobalComponent.instance.binanceTestnetComponent;
                break;
        }
        platformTemplateComponent.loggedIn = loggedIn;
        UpdateObjectState();
        // TODO: consider not using apiKey in frontend, when calling webrequest api, server assign the apikey onto request
        platformTemplateComponent.apiKey = platformComponent.apiKeyInput.text;
        platformTemplateComponent.apiSecret = platformComponent.apiSecretInput.text;
    }
    void UpdateActivePlatformOnServer()
    {
        if (platformComponent.loggedIn)
        {
            // TODO: update profile's active platform
            // General.WebsocketGeneralRequest request = new General.WebsocketAddPlatformRequest(
            //     loginComponent.token,
            //     platformComponent.apiKeyInput.text,
            //     platformComponent.apiSecretInput.text,
            //     platformComponent.activePlatform
            // );
            // websocketComponent.generalRequests.Add(request);
        }
    }
}