using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;

public class UpdateFeeSystem : MonoBehaviour
{
    OrderPageSymbolDropdownComponent orderPageSymbolDropdownComponent;
    PlatformComponent platformComponent;
    WebsocketComponent websocketComponent;
    LoginComponent loginComponent;
    PromptComponent promptComponent;
    ProfileComponent profileComponent;

    string selectedSymbol;
    bool resend = false;

    void Start()
    {
        orderPageSymbolDropdownComponent = GetComponent<OrderPageSymbolDropdownComponent>();
        platformComponent = GlobalComponent.instance.platformComponent;
        websocketComponent = GlobalComponent.instance.websocketComponent;
        loginComponent = GlobalComponent.instance.loginComponent;
        promptComponent = GlobalComponent.instance.promptComponent;
        profileComponent = GlobalComponent.instance.profileComponent;
    }
    void Update()
    {
        UpdateFees();
        ReceiveFees();
        ResendGetFee();
    }

    void UpdateFees()
    {
        if (selectedSymbol == orderPageSymbolDropdownComponent.selectedSymbol) return;
        selectedSymbol = orderPageSymbolDropdownComponent.selectedSymbol;
        if (selectedSymbol.Length <= 0) return;
        if (!platformComponent.fees.ContainsKey(selectedSymbol))
        {
            platformComponent.fees.Add(selectedSymbol, null);
            GetFeeData();
        }
    }
    void ResendGetFee()
    {
        if (!resend) return;
        resend = false;
        GetFeeData();
    }
    void GetFeeData()
    {
        General.WebsocketGetFeeDataRequest request = new(loginComponent.token, profileComponent.activeProfile.platformId, selectedSymbol);
        websocketComponent.generalRequests.Add(request);
    }
    void ReceiveFees()
    {
        string jsonString = websocketComponent.RetrieveGeneralResponses(WebsocketEventTypeEnum.GET_FEE);
        websocketComponent.RemovesGeneralResponses(WebsocketEventTypeEnum.GET_FEE);
        if (jsonString.IsNullOrEmpty()) return;

        General.WebsocketGetFeeResponse response = JsonConvert.DeserializeObject
        <General.WebsocketGetFeeResponse>(jsonString, JsonSerializerConfig.settings);

        if (!response.success)
        {
            promptComponent.ShowPrompt(PromptConstant.ERROR, response.message, () =>
            {
                promptComponent.active = false;
            });
            return;
        }

        platformComponent.fees[response.symbol] = response.fee;
    }
}
