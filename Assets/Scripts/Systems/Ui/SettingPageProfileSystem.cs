using System;
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

    List<string> profileIndexToIds;
    void Start()
    {
        settingPageComponent = GlobalComponent.instance.settingPageComponent;
        profileComponent = GlobalComponent.instance.profileComponent;
        loginComponent = GlobalComponent.instance.loginComponent;
        platformComponent = GlobalComponent.instance.platformComponent;
        websocketComponent = GlobalComponent.instance.websocketComponent;
        promptComponent = GlobalComponent.instance.promptComponent;

        settingPageComponent.onChange_updateProfile.AddListener(UpdateProfileUI);
        settingPageComponent.onChange_showAddNewProfileButton.AddListener(OnShowAddNewProfileButton);
        settingPageComponent.profileDropdown.onValueChanged.AddListener(OnProfileDropdownValueChanged);
        settingPageComponent.newProfileButton.onClick.AddListener(() =>
        settingPageComponent.showAddNewProfileButton = true);
        settingPageComponent.cancelAddProfileButton.onClick.AddListener(() =>
        settingPageComponent.showAddNewProfileButton = false);
        settingPageComponent.addProfileButton.onClick.AddListener(OnAddProfile);
        settingPageComponent.removeProfileButton.onClick.AddListener(OnRemoveProfile);
    }
    void Update()
    {
        OnAddProfileResposne();
        OnRemoveProfileResponse();
    }

    void UpdateProfileUI()
    {
        OnShowAddNewProfileButton();
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

        settingPageComponent.removeProfileButtonObj.SetActive(profileComponent.profiles.Count > 1);
    }
    void OnShowAddNewProfileButton()
    {
        settingPageComponent.newProfileButtonObj.SetActive(!settingPageComponent.showAddNewProfileButton);
        settingPageComponent.newProfileNameInputObj.SetActive(settingPageComponent.showAddNewProfileButton);
        settingPageComponent.addProfileButtonObj.SetActive(settingPageComponent.showAddNewProfileButton);
    }
    void OnProfileDropdownValueChanged(int value)
    {
        profileComponent.activeProfileId = profileIndexToIds[value];

        // TODO: sync activeProfileId to server's defaultProfileId
    }
    void OnAddProfile()
    {
        if (settingPageComponent.newProfileNameInput.text.IsNullOrEmpty())
        {
            promptComponent.ShowPrompt(PromptConstant.ERROR, PromptConstant.PROFILE_NAME_EMPTY,
            () => promptComponent.active = false);
            return;
        }
        General.WebsocketAddProfileRequest request = new(loginComponent.token, settingPageComponent.newProfileNameInput.text, platformComponent.activePlatform);
        websocketComponent.generalRequests.Add(request);

        settingPageComponent.addProfileButton.interactable = false;
    }
    void OnAddProfileResposne()
    {
        string jsonString = websocketComponent.RetrieveGeneralResponses(WebsocketEventTypeEnum.ADD_PROFILE);
        if (jsonString.IsNullOrEmpty()) return;
        websocketComponent.RemovesGeneralResponses(WebsocketEventTypeEnum.ADD_PROFILE);

        General.WebsocketAddProfileResponse response = JsonConvert.DeserializeObject
        <General.WebsocketAddProfileResponse>(jsonString, JsonSerializerConfig.settings);

        settingPageComponent.addProfileButton.interactable = true;
        if (!response.success)
        {
            promptComponent.ShowPrompt(PromptConstant.ERROR, response.message,
            () => promptComponent.active = false);
            return;
        }

        settingPageComponent.showAddNewProfileButton = false;

        profileComponent.profiles.Add(response.profile._id, response.profile);
        settingPageComponent.updateProfile = true;
        settingPageComponent.profileDropdown.value = profileComponent.profiles.Count - 1;
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
        if (jsonString.IsNullOrEmpty()) return;
        websocketComponent.RemovesGeneralResponses(WebsocketEventTypeEnum.REMOVE_PROFILE);

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
        settingPageComponent.updateProfile = true;
        settingPageComponent.profileDropdown.value = profileComponent.profiles.Count - 1;
    }
}
