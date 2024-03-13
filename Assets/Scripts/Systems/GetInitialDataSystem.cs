using Newtonsoft.Json;
using UnityEngine;
using WebSocketSharp;

public class GetInitialDataSystem : MonoBehaviour
{
    GetInitialDataComponent getInitialDataComponent;
    WebsocketComponent websocketComponent;
    LoginComponent loginComponent;

    void Start()
    {
        getInitialDataComponent = GlobalComponent.instance.getInitialDataComponent;
        websocketComponent = GlobalComponent.instance.websocketComponent;
        loginComponent = GlobalComponent.instance.loginComponent;

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

        General.WebsocketGetInitialDataResponse<object> response = JsonConvert.DeserializeObject
        <General.WebsocketGetInitialDataResponse<object>>(jsonString, JsonSerializerConfig.settings);
        Debug.Log(response.platformData == null);
    }
}