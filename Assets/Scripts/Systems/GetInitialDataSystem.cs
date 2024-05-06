using Newtonsoft.Json;
using UnityEngine;
using WebSocketSharp;

public class GetInitialDataSystem : MonoBehaviour
{
    GetInitialDataComponent getInitialDataComponent;
    WebsocketComponent websocketComponent;
    LoginComponent loginComponent;
    PromptComponent promptComponent;
    GetExchangeInfoComponent getExchangeInfoComponent;
    GetProfileDataComponent getProfileDataComponent;
    ProfileComponent profileComponent;
    PlatformComponent platformComponent;

    void Start()
    {
        getInitialDataComponent = GlobalComponent.instance.getInitialDataComponent;
        websocketComponent = GlobalComponent.instance.websocketComponent;
        loginComponent = GlobalComponent.instance.loginComponent;
        promptComponent = GlobalComponent.instance.promptComponent;
        getExchangeInfoComponent = GlobalComponent.instance.getExchangeInfoComponent;
        getProfileDataComponent = GlobalComponent.instance.getProfileDataComponent;
        profileComponent = GlobalComponent.instance.profileComponent;
        platformComponent = GlobalComponent.instance.platformComponent;

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
        websocketComponent.RemovesGeneralResponses(WebsocketEventTypeEnum.GET_INITIAL_DATA);
        if (jsonString.IsNullOrEmpty()) return;

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

        profileComponent.profiles = response.accountData.profiles;
        platformComponent.platforms = response.accountData.platforms; // TODO
        profileComponent.activeProfileId = response.defaultProfileId;

        getExchangeInfoComponent.processExchangeInfo = response.exchangeInfos;
        getProfileDataComponent.processProfileData = response;
    }
}