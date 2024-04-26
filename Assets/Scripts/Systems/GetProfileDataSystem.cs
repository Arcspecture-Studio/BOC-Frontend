using Newtonsoft.Json;
using UnityEngine;
using WebSocketSharp;

public class GetProfileDataSystem : MonoBehaviour
{
    GetProfileDataComponent getProfileDataComponent;
    LoginComponent loginComponent;
    WebsocketComponent websocketComponent;
    PromptComponent promptComponent;

    void Start()
    {
        getProfileDataComponent = GlobalComponent.instance.getProfileDataComponent;
        loginComponent = GlobalComponent.instance.loginComponent;
        websocketComponent = GlobalComponent.instance.websocketComponent;
        promptComponent = GlobalComponent.instance.promptComponent;

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
        // TODO
    }
}