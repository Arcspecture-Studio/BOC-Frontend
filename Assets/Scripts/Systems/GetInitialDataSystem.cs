using UnityEngine;

public class GetInitialDataSystem : MonoBehaviour
{
    GetInitialDataComponent getInitialDataComponent;
    WebsocketComponent websocketComponent;

    void Start()
    {

        getInitialDataComponent = GlobalComponent.instance.getInitialDataComponent;
        websocketComponent = GlobalComponent.instance.websocketComponent;

        getInitialDataComponent.onChange_getNow.AddListener(GetInitialData);
    }

    void GetInitialData()
    {
        General.WebsocketGeneralRequest request = new(WebsocketEventTypeEnum.GET_INITIAL_DATA);
        websocketComponent.generalRequests.Add(request);
    }
}