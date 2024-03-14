using Newtonsoft.Json;
using UnityEngine;
using WebSocketSharp;

public class GetInitialDataSystem : MonoBehaviour
{
    GetInitialDataComponent getInitialDataComponent;
    WebsocketComponent websocketComponent;
    LoginComponent loginComponent;
    AddPlatformComponent addPlatformComponent;
    PlatformComponentOld platformComponentOld;
    PromptComponent promptComponent;

    void Start()
    {
        getInitialDataComponent = GlobalComponent.instance.getInitialDataComponent;
        websocketComponent = GlobalComponent.instance.websocketComponent;
        loginComponent = GlobalComponent.instance.loginComponent;
        addPlatformComponent = GlobalComponent.instance.addPlatformComponent;
        platformComponentOld = GlobalComponent.instance.platformComponentOld;
        promptComponent = GlobalComponent.instance.promptComponent;

        getInitialDataComponent.onChange_getInitialData.AddListener(GetInitialData);
    }
    void Update()
    {
        GetInitialDataResponse();
    }
    void GetInitialData()
    {
        UnityMainThread.AddJob(() =>
        {
            General.WebsocketGeneralRequest request = new(WebsocketEventTypeEnum.GET_INITIAL_DATA, loginComponent.token);
            websocketComponent.generalRequests.Add(request);
        });
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

        if (response.accountData.platformList.Count == 0)
        {
            loginComponent.gameObject.SetActive(false);
            addPlatformComponent.gameObject.SetActive(true);
            return;
        }
        platformComponentOld.activePlatform = response.accountData.profiles[response.defaultProfileId].activePlatform.Value;
        switch (platformComponentOld.activePlatform)
        {
            case PlatformEnum.BINANCE:
            case PlatformEnum.BINANCE_TESTNET:
                Binance.WebsocketPlatformDataResponse platformData = JsonConvert.DeserializeObject
                <Binance.WebsocketPlatformDataResponse>(response.platformData.ToString(), JsonSerializerConfig.settings);
                break;
        }
    }
}