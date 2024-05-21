using System.Collections.Generic;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using WebSocketSharp;

public class PlatformSystem : MonoBehaviour
{
    PlatformComponent platformComponent;
    PromptComponent promptComponent;
    WebsocketComponent websocketComponent;
    LoginComponent loginComponent;
    LoadingComponent loadingComponent;
    ProfileComponent profileComponent;
    GetInitialDataComponent getInitialDataComponent;
    MiniPromptComponent miniPromptComponent;

    List<string> platformIdsMapper;

    void Start()
    {
        platformComponent = GlobalComponent.instance.platformComponent;
        promptComponent = GlobalComponent.instance.promptComponent;
        websocketComponent = GlobalComponent.instance.websocketComponent;
        loginComponent = GlobalComponent.instance.loginComponent;
        loadingComponent = GlobalComponent.instance.loadingComponent;
        profileComponent = GlobalComponent.instance.profileComponent;
        getInitialDataComponent = GlobalComponent.instance.getInitialDataComponent;
        miniPromptComponent = GlobalComponent.instance.miniPromptComponent;

        InitializeEventListeners();
        platformComponent.gameObject.SetActive(false);
    }
    void Update()
    {
        AddPlatformResponse();
        RemovePlatformResponse();
    }
    void OnComponentEnable()
    {
        if (GlobalComponent.instance.platformComponent.platforms.Count > 0)
        {
            GlobalComponent.instance.platformComponent.platformPage = PlatformPageEnum.SWITCH_OR_REMOVE;
        }
        else
        {
            GlobalComponent.instance.platformComponent.platformPage = PlatformPageEnum.ADD;
        }
    }

    void InitializeEventListeners()
    {
        platformComponent.onEnable.AddListener(OnComponentEnable);
        platformComponent.onChange_platformPage.AddListener(OnSwitchPlatformPage);

        platformComponent.addPlatformButton.onClick.AddListener(() => platformComponent.platformPage = PlatformPageEnum.ADD);
        platformComponent.cancelRegisterButton.onClick.AddListener(() => platformComponent.platformPage = PlatformPageEnum.SWITCH_OR_REMOVE);
        platformComponent.connectButton.onClick.AddListener(AddPlatformRequest);
        platformComponent.disconnectButton.onClick.AddListener(RemovePlatformRequest);
        platformComponent.backButton.onClick.AddListener(PromptConfirmToUpdateProfilePlatformIdOnServer);
        platformComponent.logoutButton.onClick.AddListener(Logout);
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
    void ClearInput()
    {
        platformComponent.apiKeyInput.text = "";
        platformComponent.apiSecretInput.text = "";
    }
    void OnSwitchPlatformPage(PlatformPageEnum platformPageEnum)
    {
        platformComponent.addPlatformObj.SetActive(platformPageEnum == PlatformPageEnum.ADD);
        platformComponent.switchOrRemovePlatformObj.SetActive(platformPageEnum == PlatformPageEnum.SWITCH_OR_REMOVE);

        if (platformPageEnum == PlatformPageEnum.SWITCH_OR_REMOVE)
        {
            UpdatePlatformIdsDropdownUi();
        }
    }
    void UpdatePlatformIdsDropdownUi()
    {
        platformComponent.platformIdsDropdown.ClearOptions();
        platformIdsMapper = new();

        foreach (KeyValuePair<string, Platform> platform in platformComponent.platforms)
        {
            string str = platform.Key + "(" + platform.Value.platform + ")";
            platformIdsMapper.Add(platform.Key);
            platformComponent.platformIdsDropdown.options.Add(new TMP_Dropdown.OptionData(str));
        }

        if (platformComponent.platforms.Count > 0)
        {
            platformComponent.platformIdsDropdown.value = platformIdsMapper.IndexOf(profileComponent.activeProfile.platformId);
            platformComponent.platformIdsDropdown.captionText.text = platformComponent.platformIdsDropdown.options[platformComponent.platformIdsDropdown.value].text;
        }
        else
        {
            platformComponent.platformPage = PlatformPageEnum.ADD;
        }
    }
    void AddPlatformRequest()
    {
        if (InvalidateInput()) return;
        websocketComponent.generalRequests.Add(new General.WebsocketAddPlatformRequest(
            loginComponent.token,
            platformComponent.apiKeyInput.text,
            platformComponent.apiSecretInput.text,
            (PlatformEnum)platformComponent.platformsDropdown.value
        ));
        loadingComponent.active = true;
    }
    void AddPlatformResponse()
    {
        string jsonString = websocketComponent.RetrieveGeneralResponses(WebsocketEventTypeEnum.ADD_PLATFORM);
        websocketComponent.RemovesGeneralResponses(WebsocketEventTypeEnum.ADD_PLATFORM);
        if (jsonString.IsNullOrEmpty()) return;

        General.WebsocketAddPlatformResponse response = JsonConvert.DeserializeObject
        <General.WebsocketAddPlatformResponse>(jsonString, JsonSerializerConfig.settings);

        ClearInput();

        if (!response.success)
        {
            promptComponent.ShowPrompt(PromptConstant.ERROR, response.message, () =>
            {
                loadingComponent.active = false;
                promptComponent.active = false;
            });
            return;
        }
        loadingComponent.active = false;

        platformComponent.platforms.Add(response.platformId,
        new Platform(response.platform));
        // profileComponent.activeProfile.platformId = response.platformId;
        platformComponent.platformPage = PlatformPageEnum.SWITCH_OR_REMOVE;
        miniPromptComponent.message = PromptConstant.PLATFORM_ADDED;
        // getInitialDataComponent.getInitialData = true;
    }
    void RemovePlatformRequest()
    {
        promptComponent.ShowSelection(PromptConstant.DISCONNECT_PLATFORM, PromptConstant.DISCONNECT_PLATFORM_CONFIRM, PromptConstant.YES_PROCEED, PromptConstant.NO, () =>
        {
            websocketComponent.generalRequests.Add(new General.WebsocketRemovePlatformRequest(
                loginComponent.token,
                platformIdsMapper[platformComponent.platformIdsDropdown.value]
            ));
            loadingComponent.active = true;

            promptComponent.active = false;
        }, () =>
        {
            promptComponent.active = false;
        });
    }
    void RemovePlatformResponse()
    {
        string jsonString = websocketComponent.RetrieveGeneralResponses(WebsocketEventTypeEnum.REMOVE_PLATFORM);
        websocketComponent.RemovesGeneralResponses(WebsocketEventTypeEnum.REMOVE_PLATFORM);
        if (jsonString.IsNullOrEmpty()) return;

        General.WebsocketRemovePlatformResponse response = JsonConvert.DeserializeObject
        <General.WebsocketRemovePlatformResponse>(jsonString, JsonSerializerConfig.settings);

        if (!response.success)
        {
            promptComponent.ShowPrompt(PromptConstant.ERROR, response.message, () =>
            {
                loadingComponent.active = false;
                promptComponent.active = false;
            });
            return;
        }
        loadingComponent.active = false;

        platformComponent.platforms.Remove(response.platformId);
        profileComponent.activeProfile.platformId = response.newActivePlatformId;
        platformComponent.platformPage = PlatformPageEnum.SWITCH_OR_REMOVE;
        getInitialDataComponent.getInitialData = true;
    }
    void PromptConfirmToUpdateProfilePlatformIdOnServer()
    {
        if (profileComponent.activeProfile.platformId ==
        platformIdsMapper[platformComponent.platformIdsDropdown.value])
        {
            platformComponent.gameObject.SetActive(false);
            return;
        }

        if (platformComponent.platforms[profileComponent.activeProfile.platformId].platform == platformComponent.platforms[platformIdsMapper[platformComponent.platformIdsDropdown.value]].platform)
        {
            UpdateProfilePlatformIdOnServer();
        }
        else // cross platform
        {
            promptComponent.ShowSelection(PromptConstant.SWITCH_PLATFORM, PromptConstant.SWITCH_PLATFORM_CONFIRM, PromptConstant.YES_PROCEED, PromptConstant.NO, () =>
            {
                UpdateProfilePlatformIdOnServer();
                promptComponent.active = false;
            }, () =>
            {
                platformComponent.platformIdsDropdown.value = platformIdsMapper.IndexOf(profileComponent.activeProfile.platformId);
                platformComponent.gameObject.SetActive(false);
                miniPromptComponent.message = PromptConstant.SWITCH_PLATFORM_CANCELLED;
                promptComponent.active = false;
            });
        }
    }
    void UpdateProfilePlatformIdOnServer()
    {
        profileComponent.activeProfile.platformId = platformIdsMapper[platformComponent.platformIdsDropdown.value];

        General.WebsocketUpdateProfileRequest request = new(loginComponent.token, profileComponent.activeProfileId, profileComponent.activeProfile.platformId, UpdateProfilePropertyEnum.platformId);
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