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
    ProfileComponent profileComponent;
    GetInitialDataComponent getInitialDataComponent;
    LoadingComponent loadingComponent;
    MiniPromptComponent miniPromptComponent;

    void Start()
    {
        platformComponent = GlobalComponent.instance.platformComponent;
        websocketComponent = GlobalComponent.instance.websocketComponent;
        loginComponent = GlobalComponent.instance.loginComponent;
        promptComponent = GlobalComponent.instance.promptComponent;
        profileComponent = GlobalComponent.instance.profileComponent;
        getInitialDataComponent = GlobalComponent.instance.getInitialDataComponent;
        loadingComponent = GlobalComponent.instance.loadingComponent;
        miniPromptComponent = GlobalComponent.instance.miniPromptComponent;

        // Set initial state
        platformComponent.gameObject.SetActive(false);
        platformComponent.onEnable.AddListener(OnComponentEnable);
        platformComponent.platformsDropdown.onValueChanged.AddListener(value => UpdateObjectState());
        // platformComponent.proceedButton.onClick.AddListener(AddOrRemovePlatform);
        platformComponent.backButton.onClick.AddListener(PromptConfirmToUpdateProfileActivePlatformOnServer);
        platformComponent.logoutButton.onClick.AddListener(Logout);
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
        platformComponent.activePlatformOnUi = platformComponent.activePlatform;
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
                    // platformComponent.backButtonObj.SetActive(true);
                    // platformComponent.apiKeyObj.SetActive(false);
                    // platformComponent.apiSecretObj.SetActive(false);
                    // platformComponent.proceedButtonText.text = PromptConstant.DISCONNECT;
                }
            }
            else
            {
                platformComponent.platformsDropdown.options[i].text = platformEnum.ToString();
                if (platformComponent.platformsDropdown.value == i)
                {
                    ClearInput();
                    // platformComponent.backButtonObj.SetActive(false);
                    // platformComponent.apiKeyObj.SetActive(true);
                    // platformComponent.apiSecretObj.SetActive(true);
                    // platformComponent.proceedButtonText.text = PromptConstant.CONNECT;
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
        // platformComponent.proceedButton.interactable = yes;
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
        switch (platformComponent.activePlatformOnUi)
        {
            case PlatformEnum.BINANCE:
            case PlatformEnum.BINANCE_TESTNET:
                BinanceComponent binanceComponent = platformComponent.activePlatformOnUi == PlatformEnum.BINANCE ?
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
                platformComponent.activePlatformOnUi
            );
            websocketComponent.generalRequests.Add(request);
            AllowForInteraction(false);
        }
        else
        {
            promptComponent.ShowSelection(PromptConstant.DISCONNECT_PLATFORM, PromptConstant.DISCONNECT_PLATFORM_CONFIRM, PromptConstant.YES_PROCEED, PromptConstant.NO, () =>
            {
                // request = new General.WebsocketRemovePlatformRequest(loginComponent.token, platformComponent.activePlatformOnUi);
                // websocketComponent.generalRequests.Add(request);
                AllowForInteraction(false);

                promptComponent.active = false;
            }, () =>
            {
                promptComponent.active = false;
            });
        }

    }
    void AddPlatformResponse()
    {
        string jsonString = websocketComponent.RetrieveGeneralResponses(WebsocketEventTypeEnum.ADD_PLATFORM);
        websocketComponent.RemovesGeneralResponses(WebsocketEventTypeEnum.ADD_PLATFORM);
        if (jsonString.IsNullOrEmpty()) return;

        // HandleResponse(jsonString, true);
    }
    void RemovePlatformResponse()
    {
        string jsonString = websocketComponent.RetrieveGeneralResponses(WebsocketEventTypeEnum.REMOVE_PLATFORM);
        websocketComponent.RemovesGeneralResponses(WebsocketEventTypeEnum.REMOVE_PLATFORM);
        if (jsonString.IsNullOrEmpty()) return;

        // HandleResponse(jsonString, false);
    }
    // void HandleResponse(string jsonString, bool loggedIn)
    // {
    //     // General.WebsocketAddOrRemovePlatformResponse response = JsonConvert.DeserializeObject
    //     // <General.WebsocketAddOrRemovePlatformResponse>(jsonString, JsonSerializerConfig.settings);

    //     AllowForInteraction(true);
    //     if (!response.success)
    //     {
    //         ClearInput();
    //         promptComponent.ShowPrompt(PromptConstant.ERROR, response.message, () =>
    //         {
    //             promptComponent.active = false;
    //         });
    //         return;
    //     }

    //     PlatformTemplateComponent platformTemplateComponent = GlobalComponent.instance.binanceComponent;
    //     switch (response.platform)
    //     {
    //         case PlatformEnum.BINANCE_TESTNET:
    //             platformTemplateComponent = GlobalComponent.instance.binanceTestnetComponent;
    //             break;
    //     }
    //     platformTemplateComponent.loggedIn = loggedIn;
    //     UpdateObjectState();
    //     // platformComponent.activePlatform = response.newActivePlatform;
    //     getInitialDataComponent.getInitialData = true;
    // }
    void PromptConfirmToUpdateProfileActivePlatformOnServer()
    {
        // if (!platformComponent.loggedIn) return;
        if (profileComponent.activeProfile == null) return;
        if (platformComponent.activePlatformOnUi == platformComponent.activePlatform)
        {
            platformComponent.gameObject.SetActive(false);
            return;
        }

        promptComponent.ShowSelection(PromptConstant.SWITCH_PLATFORM, PromptConstant.SWITCH_PLATFORM_CONFIRM, PromptConstant.YES_PROCEED, PromptConstant.NO, () =>
        {
            UpdateProfileActivePlatformOnServer();
            promptComponent.active = false;
        }, () =>
        {
            platformComponent.activePlatformOnUi = platformComponent.activePlatform;
            platformComponent.gameObject.SetActive(false);
            miniPromptComponent.message = PromptConstant.SWITCH_PLATFORM_CANCELLED;
            promptComponent.active = false;
        });
    }
    void UpdateProfileActivePlatformOnServer()
    {
        // platformComponent.activePlatform = platformComponent.activePlatformOnUi;

        General.WebsocketUpdateProfileRequest request = new(loginComponent.token, profileComponent.activeProfileId, "", UpdateProfilePropertyEnum.platformId);
        websocketComponent.generalRequests.Add(request);

        loadingComponent.active = true;
    }
    void Logout()
    {
        promptComponent.ShowSelection(PromptConstant.LOGOUT, PromptConstant.LOGOUT_CONFIRM, PromptConstant.YES_PROCEED, PromptConstant.NO, () =>
        {
            loginComponent.logoutNow = true;
            platformComponent.logoutButton.interactable = false;
            platformComponent.gameObject.SetActive(false);

            promptComponent.active = false;
        }, () =>
        {
            promptComponent.active = false;
        });
    }
}