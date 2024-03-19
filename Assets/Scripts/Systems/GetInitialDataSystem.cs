using System.Collections.Generic;
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

    void Start()
    {
        getInitialDataComponent = GlobalComponent.instance.getInitialDataComponent;
        websocketComponent = GlobalComponent.instance.websocketComponent;
        loginComponent = GlobalComponent.instance.loginComponent;
        platformComponent = GlobalComponent.instance.platformComponent;
        promptComponent = GlobalComponent.instance.promptComponent;
        profileComponent = GlobalComponent.instance.profileComponent;
        settingPageComponent = GlobalComponent.instance.settingPageComponent;

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

        loginComponent.loginStatus = LoginPageStatusEnum.LOGGED_IN;
        if (response.accountData.platformList.Count == 0)
        {
            platformComponent.gameObject.SetActive(true);
            return;
        }

        profileComponent.profiles = response.accountData.profiles;
        profileComponent.activeProfileId = response.defaultProfileId;
        settingPageComponent.updateProfile = true;
        settingPageComponent.updatePreferenceUI = true;

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
        switch (platformComponent.activePlatform)
        {
            case PlatformEnum.BINANCE:
            case PlatformEnum.BINANCE_TESTNET:
                UpdateBinancePlatformData(response);
                break;
        }
        settingPageComponent.updateInfo = true;
    }
    void UpdateBinancePlatformData(General.WebsocketGetInitialDataResponse response)
    {
        Binance.WebsocketPlatformDataResponse platformData = JsonConvert.DeserializeObject
        <Binance.WebsocketPlatformDataResponse>(response.platformData.ToString(), JsonSerializerConfig.settings);

        platformComponent.walletBalances = new();
        foreach (KeyValuePair<string, Binance.WebrequestGetBalanceResponseData> balance in platformData.balances)
        {
            platformComponent.walletBalances.Add(balance.Key, double.Parse(balance.Value.balance));
        }

        // TODO: exchange info
    }
}