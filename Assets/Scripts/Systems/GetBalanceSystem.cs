using AYellowpaper.SerializedCollections;
using Newtonsoft.Json;
using UnityEngine;
using WebSocketSharp;

public class GetBalanceSystem : MonoBehaviour
{
    GetBalanceComponent getBalanceComponent;
    PlatformComponent platformComponent;
    WebsocketComponent websocketComponent;
    LoginComponent loginComponent;
    SettingPageComponent settingPageComponent;
    ProfileComponent profileComponent;

    void Start()
    {
        getBalanceComponent = GlobalComponent.instance.getBalanceComponent;
        platformComponent = GlobalComponent.instance.platformComponent;
        websocketComponent = GlobalComponent.instance.websocketComponent;
        loginComponent = GlobalComponent.instance.loginComponent;
        settingPageComponent = GlobalComponent.instance.settingPageComponent;
        profileComponent = GlobalComponent.instance.profileComponent;

        getBalanceComponent.onChange_processBalance.AddListener(ProcessBalance);
        getBalanceComponent.onChange_getBalance.AddListener(GetBalance);
    }
    void Update()
    {
        GetBalanceResponse();
    }

    void GetBalance()
    {
        General.WebsocketGetBalanceRequest request = new(loginComponent.token, profileComponent.activeProfile.platformId);
        websocketComponent.generalRequests.Add(request);
    }
    void GetBalanceResponse()
    {
        string jsonString = websocketComponent.RetrieveGeneralResponses(WebsocketEventTypeEnum.GET_BALANCE);
        websocketComponent.RemovesGeneralResponses(WebsocketEventTypeEnum.GET_BALANCE);
        if (jsonString.IsNullOrEmpty()) return;

        General.WebsocketGetBalanceResponse response = JsonConvert.DeserializeObject
        <General.WebsocketGetBalanceResponse>(jsonString, JsonSerializerConfig.settings);

        ProcessBalance(response.balances);
    }
    void ProcessBalance(SerializedDictionary<string, float> balances)
    {
        platformComponent.walletBalances = balances;
        settingPageComponent.updateInfoUI = true;
    }
}