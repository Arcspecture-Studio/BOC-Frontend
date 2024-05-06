using Newtonsoft.Json;
using UnityEngine;
using WebSocketSharp;

public class GetProfileDataSystem : MonoBehaviour
{
    GetProfileDataComponent getProfileDataComponent;
    LoginComponent loginComponent;
    WebsocketComponent websocketComponent;
    PromptComponent promptComponent;
    PlatformComponent platformComponent;
    ProfileComponent profileComponent;
    SettingPageComponent settingPageComponent;
    GetRuntimeDataComponent getRuntimeDataComponent;

    void Start()
    {
        getProfileDataComponent = GlobalComponent.instance.getProfileDataComponent;
        loginComponent = GlobalComponent.instance.loginComponent;
        websocketComponent = GlobalComponent.instance.websocketComponent;
        promptComponent = GlobalComponent.instance.promptComponent;
        platformComponent = GlobalComponent.instance.platformComponent;
        profileComponent = GlobalComponent.instance.profileComponent;
        settingPageComponent = GlobalComponent.instance.settingPageComponent;
        getRuntimeDataComponent = GlobalComponent.instance.getRuntimeDataComponent;

        getProfileDataComponent.onChange_getProfileData.AddListener(GetProfileData);
        getProfileDataComponent.onChange_processProfileData.AddListener(ProcessProfileData);
    }
    void Update()
    {
        GetProfileDataResponse();
    }

    void GetProfileData()
    {
        General.WebsocketGeneralRequest request = new(WebsocketEventTypeEnum.GET_PROFILE_DATA, loginComponent.token);
        websocketComponent.generalRequests.Add(request);
    }
    void GetProfileDataResponse()
    {
        string jsonString = websocketComponent.RetrieveGeneralResponses(WebsocketEventTypeEnum.GET_PROFILE_DATA);
        websocketComponent.RemovesGeneralResponses(WebsocketEventTypeEnum.GET_PROFILE_DATA);
        if (jsonString.IsNullOrEmpty()) return;

        General.WebsocketGetProfileDataResponse response = JsonConvert.DeserializeObject<General.WebsocketGetProfileDataResponse>(jsonString, JsonSerializerConfig.settings);

        if (!response.success)
        {
            promptComponent.ShowPrompt(PromptConstant.ERROR, response.message, () =>
            {
                promptComponent.active = false;
            });
            return;
        }

        ProcessProfileData(response);
    }
    void ProcessProfileData(General.WebsocketGetProfileDataResponse profileData)
    {
        if (profileData.accountData.platforms.Count == 0)
        {
            platformComponent.gameObject.SetActive(true);
            return;
        }

        profileComponent.profiles = profileData.accountData.profiles;
        profileComponent.activeProfileId = profileData.defaultProfileId;
        settingPageComponent.updateProfileUI = true;

        // TODO
        foreach (PlatformEnum platform in profileData.accountData.platforms)
        {
            switch (platform)
            {
                case PlatformEnum.BINANCE:
                    GlobalComponent.instance.binanceComponent.loggedIn = true;
                    break;
                case PlatformEnum.BINANCE_TESTNET:
                    GlobalComponent.instance.binanceTestnetComponent.loggedIn = true;
                    break;
            }
        }
        getRuntimeDataComponent.processRuntimeData = profileData;
    }
}