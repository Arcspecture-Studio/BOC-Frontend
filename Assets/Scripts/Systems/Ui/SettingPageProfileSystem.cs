using System.Collections.Generic;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using WebSocketSharp;

public class SettingPageProfileSystem : MonoBehaviour
{
    SettingPageComponent settingPageComponent;
    ProfileComponent profileComponent;
    LoginComponent loginComponent;
    PlatformComponent platformComponent;
    WebsocketComponent websocketComponent;
    PromptComponent promptComponent;
    GetInitialDataComponent getInitialDataComponent;
    LoadingComponent loadingComponent;

    List<string> profileIndexToIds;
    void Start()
    {
        settingPageComponent = GlobalComponent.instance.settingPageComponent;
        profileComponent = GlobalComponent.instance.profileComponent;
        loginComponent = GlobalComponent.instance.loginComponent;
        platformComponent = GlobalComponent.instance.platformComponent;
        websocketComponent = GlobalComponent.instance.websocketComponent;
        promptComponent = GlobalComponent.instance.promptComponent;
        getInitialDataComponent = GlobalComponent.instance.getInitialDataComponent;
        loadingComponent = GlobalComponent.instance.loadingComponent;

        DefineListeners();
        OnShowAddNewProfileButton();
        OnShowRenameProfileButton();
    }
    void Update()
    {
        OnProfileDropdownValueChanged();

        OnAddProfileResponse();
        OnRemoveProfileResponse();
        OnUpdateProfileResponse();
    }

    void DefineListeners()
    {
        settingPageComponent.onChange_updateProfile.AddListener(UpdateProfileUI);
        settingPageComponent.onChange_showAddNewProfileButton.AddListener(OnShowAddNewProfileButton);
        settingPageComponent.onChange_showRenameProfileButton.AddListener(OnShowRenameProfileButton);

        settingPageComponent.addProfileButton.onClick.AddListener(() => settingPageComponent.showAddNewProfileButton = true);
        settingPageComponent.cancelAddProfileButton.onClick.AddListener(() => settingPageComponent.showAddNewProfileButton = false);
        settingPageComponent.confirmAddProfileButton.onClick.AddListener(OnAddProfile);

        settingPageComponent.renameProfileButton.onClick.AddListener(() => settingPageComponent.showRenameProfileButton = true);
        settingPageComponent.cancelRenameProfileButton.onClick.AddListener(() => settingPageComponent.showRenameProfileButton = false);
        settingPageComponent.confirmRenameProfileButton.onClick.AddListener(OnRenameProfile);

        settingPageComponent.removeProfileButton.onClick.AddListener(OnRemoveProfile);
    }
    void UpdateProfileUI()
    {
        UpdateProfileDropdownUI();
    }
    void UpdateProfileDropdownUI()
    {
        profileIndexToIds = new();
        List<TMP_Dropdown.OptionData> optionDataList = new();
        foreach (KeyValuePair<string, Profile> profile in profileComponent.profiles)
        {
            profileIndexToIds.Add(profile.Key);
            optionDataList.Add(new TMP_Dropdown.OptionData(profile.Value.name));
        }
        settingPageComponent.profileDropdown.options = optionDataList;
        settingPageComponent.profileDropdown.value = profileIndexToIds.IndexOf(profileComponent.activeProfileId);

        settingPageComponent.removeProfileButtonObj.SetActive(profileComponent.profiles.Count > 1);
    }
    void OnShowAddNewProfileButton()
    {
        settingPageComponent.addProfileButtonObj.SetActive(!settingPageComponent.showAddNewProfileButton);
        settingPageComponent.addProfileNameInputObj.SetActive(settingPageComponent.showAddNewProfileButton);
        settingPageComponent.confirmAddProfileButtonObj.SetActive(settingPageComponent.showAddNewProfileButton);
        if (settingPageComponent.showAddNewProfileButton)
        {
            settingPageComponent.addProfileNameInput.text = "";
        }
    }
    void OnShowRenameProfileButton()
    {
        settingPageComponent.renameProfileButtonObj.SetActive(!settingPageComponent.showRenameProfileButton);
        settingPageComponent.renameProfileNameInputObj.SetActive(settingPageComponent.showRenameProfileButton);
        settingPageComponent.confirmRenameProfileButtonObj.SetActive(settingPageComponent.showRenameProfileButton);
        if (settingPageComponent.showRenameProfileButton)
        {
            settingPageComponent.renameProfileNameInput.text = profileComponent.activeProfile.name;
        }
    }
    void OnProfileDropdownValueChanged()
    {
        if (profileIndexToIds == null || profileIndexToIds.Count == 0) return;
        if (profileIndexToIds.IndexOf(profileComponent.activeProfileId) == settingPageComponent.profileDropdown.value) return;
        profileComponent.activeProfileId = profileIndexToIds[settingPageComponent.profileDropdown.value];

        General.WebsocketUpdateProfileRequest request = new(loginComponent.token, profileComponent.activeProfileId, true);
        websocketComponent.generalRequests.Add(request);

        loadingComponent.active = true;
    }
    void OnAddProfile()
    {
        if (settingPageComponent.addProfileNameInput.text.IsNullOrEmpty())
        {
            promptComponent.ShowPrompt(PromptConstant.ERROR, PromptConstant.PROFILE_NAME_EMPTY,
            () => promptComponent.active = false);
            return;
        }
        General.WebsocketAddProfileRequest request = new(loginComponent.token, settingPageComponent.addProfileNameInput.text, platformComponent.activePlatform);
        websocketComponent.generalRequests.Add(request);

        settingPageComponent.confirmAddProfileButton.interactable = false;
    }
    void OnAddProfileResponse()
    {
        string jsonString = websocketComponent.RetrieveGeneralResponses(WebsocketEventTypeEnum.ADD_PROFILE);
        websocketComponent.RemovesGeneralResponses(WebsocketEventTypeEnum.ADD_PROFILE);
        if (jsonString.IsNullOrEmpty()) return;

        General.WebsocketAddProfileResponse response = JsonConvert.DeserializeObject
        <General.WebsocketAddProfileResponse>(jsonString, JsonSerializerConfig.settings);

        settingPageComponent.confirmAddProfileButton.interactable = true;
        if (!response.success)
        {
            promptComponent.ShowPrompt(PromptConstant.ERROR, response.message,
            () => promptComponent.active = false);
            return;
        }

        settingPageComponent.showAddNewProfileButton = false;

        profileComponent.profiles.Add(response.profile._id, response.profile);
        profileComponent.activeProfileId = response.profile._id;
        settingPageComponent.updateProfile = true;
        settingPageComponent.profileDropdown.value = profileIndexToIds.IndexOf(profileComponent.activeProfileId);
    }
    void OnRemoveProfile()
    {
        string message = PromptConstant.REMOVE_PROFILE_CONFIRM + "\"" + profileComponent.activeProfile.name + "\"?";
        promptComponent.ShowSelection(PromptConstant.REMOVE_PROFILE, message, PromptConstant.YES_PROCEED, PromptConstant.NO, () =>
        {
            General.WebsocketRemoveProfileRequest request = new(loginComponent.token, profileComponent.activeProfileId);
            websocketComponent.generalRequests.Add(request);

            settingPageComponent.removeProfileButton.interactable = false;
            promptComponent.active = false;
        }, () =>
        {
            promptComponent.active = false;
        });
    }
    void OnRemoveProfileResponse()
    {
        string jsonString = websocketComponent.RetrieveGeneralResponses(WebsocketEventTypeEnum.REMOVE_PROFILE);
        websocketComponent.RemovesGeneralResponses(WebsocketEventTypeEnum.REMOVE_PROFILE);
        if (jsonString.IsNullOrEmpty()) return;

        General.WebsocketRemoveProfileResponse response = JsonConvert.DeserializeObject
        <General.WebsocketRemoveProfileResponse>(jsonString, JsonSerializerConfig.settings);

        settingPageComponent.removeProfileButton.interactable = true;
        if (!response.success)
        {
            promptComponent.ShowPrompt(PromptConstant.ERROR, response.message,
            () => promptComponent.active = false);
            return;
        }

        profileComponent.profiles.Remove(response.profileId);
        profileComponent.activeProfileId = response.newDefaultProfileId;
        settingPageComponent.updateProfile = true;
        settingPageComponent.profileDropdown.value = profileIndexToIds.IndexOf(profileComponent.activeProfileId);
    }
    void OnRenameProfile()
    {
        if (settingPageComponent.renameProfileNameInput.text.IsNullOrEmpty())
        {
            promptComponent.ShowPrompt(PromptConstant.ERROR, PromptConstant.PROFILE_NAME_EMPTY,
            () => promptComponent.active = false);
            return;
        }
        General.WebsocketUpdateProfileRequest request = new(loginComponent.token, profileComponent.activeProfile._id, settingPageComponent.renameProfileNameInput.text);
        websocketComponent.generalRequests.Add(request);

        settingPageComponent.confirmRenameProfileButton.interactable = false;
    }
    void OnUpdateProfileResponse()
    {
        string jsonString = websocketComponent.RetrieveGeneralResponses(WebsocketEventTypeEnum.UPDATE_PROFILE);
        websocketComponent.RemovesGeneralResponses(WebsocketEventTypeEnum.UPDATE_PROFILE);
        if (jsonString.IsNullOrEmpty()) return;

        General.WebsocketUpdateProfileResponse response = JsonConvert.DeserializeObject
        <General.WebsocketUpdateProfileResponse>(jsonString, JsonSerializerConfig.settings);

        if (!response.success)
        {
            promptComponent.ShowPrompt(PromptConstant.ERROR, response.message,
            () => promptComponent.active = false);
            return;
        }

        switch (response.property)
        {
            case UpdateProfilePropertyEnum.name:
                OnRenameProfileResponse(response);
                break;
            case UpdateProfilePropertyEnum.activePlatform:
            case UpdateProfilePropertyEnum.makeItDefault:
                getInitialDataComponent.getInitialData = true;
                break;
        }
    }
    void OnRenameProfileResponse(General.WebsocketUpdateProfileResponse response)
    {
        settingPageComponent.confirmRenameProfileButton.interactable = true;

        profileComponent.profiles[response.profileId].name = settingPageComponent.renameProfileNameInput.text;
        settingPageComponent.showRenameProfileButton = false;
        settingPageComponent.updateProfile = true;
    }
}
