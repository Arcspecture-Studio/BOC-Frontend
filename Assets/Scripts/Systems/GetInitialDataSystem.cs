using Newtonsoft.Json;
using UnityEngine;
using WebSocketSharp;

public class GetInitialDataSystem : MonoBehaviour
{
    GetInitialDataComponent getInitialDataComponent;
    WebsocketComponent websocketComponent;
    LoginComponent loginComponent;
    PlatformComponent platformComponent;
    PromptComponent promptComponent;
    ProfileComponent profileComponent;
    SettingPageComponent settingPageComponent;
    QuickTabComponent quickTabComponent;
    GetRuntimeDataComponent getRuntimeDataComponent;

    void Start()
    {
        getInitialDataComponent = GlobalComponent.instance.getInitialDataComponent;
        websocketComponent = GlobalComponent.instance.websocketComponent;
        loginComponent = GlobalComponent.instance.loginComponent;
        platformComponent = GlobalComponent.instance.platformComponent;
        promptComponent = GlobalComponent.instance.promptComponent;
        profileComponent = GlobalComponent.instance.profileComponent;
        settingPageComponent = GlobalComponent.instance.settingPageComponent;
        quickTabComponent = GlobalComponent.instance.quickTabComponent;
        getRuntimeDataComponent = GlobalComponent.instance.getRuntimeDataComponent;

        getInitialDataComponent.onChange_getInitialData.AddListener(GetInitialData);
    }
    void Update()
    {
        GetInitialDataResponse();
    }
    void GetInitialData()
    {
        General.WebsocketGeneralRequest request = new(WebsocketEventTypeEnum.GET_INITIAL_DATA, loginComponent.token);
        websocketComponent.generalRequests.Add(request);
    }
    void GetInitialDataResponse()
    {
        string jsonString = websocketComponent.RetrieveGeneralResponses(WebsocketEventTypeEnum.GET_INITIAL_DATA);
        if (jsonString.IsNullOrEmpty()) return;
        websocketComponent.RemovesGeneralResponses(WebsocketEventTypeEnum.GET_INITIAL_DATA);

        General.WebsocketGetInitialDataResponse response = JsonConvert.DeserializeObject
        <General.WebsocketGetInitialDataResponse>(jsonString, JsonSerializerConfig.settings);

        if (!response.success)
        {
            promptComponent.ShowPrompt(PromptConstant.ERROR, response.message, () =>
            {
                promptComponent.active = false;
            });
            return;
        }

        loginComponent.loginStatus = LoginPageStatusEnum.LOGGED_IN;
        if (response.accountData.platformList.Count == 0)
        {
            platformComponent.gameObject.SetActive(true);
            return;
        }

        #region Profile data
        profileComponent.profiles = response.accountData.profiles;
        profileComponent.activeProfileId = response.defaultProfileId;
        settingPageComponent.updateProfile = true;
        settingPageComponent.updatePreferenceUI = true;
        quickTabComponent.updatePreferenceUI = true;
        #endregion

        #region Platform data
        foreach (PlatformEnum platform in response.accountData.platformList)
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
        platformComponent.activePlatform = profileComponent.activeProfile.activePlatform.Value;
        platformComponent.processInitialData = response;
        settingPageComponent.updateInfo = true;
        #endregion

        #region Runtime data
        getRuntimeDataComponent.processRuntimeData = response;
        #endregion
    }
}