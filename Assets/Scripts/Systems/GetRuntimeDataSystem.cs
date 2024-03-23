using UnityEngine;

public class GetRuntimeDataSystem : MonoBehaviour
{
    GetRuntimeDataComponent getRuntimeDataComponent;

    void Start()
    {
        getRuntimeDataComponent = GlobalComponent.instance.getRuntimeDataComponent;

        getRuntimeDataComponent.onChange_getRuntimeData.AddListener(GetRuntimeData);
        getRuntimeDataComponent.onChange_processRuntimeData.AddListener(ProcessRuntimeData);
    }
    void Update()
    {
        GetRuntimeDataResponse();
    }

    void GetRuntimeData()
    {
        // TODO: call websocket event
    }
    void GetRuntimeDataResponse()
    {
        // TODO: call process runtime
    }
    void ProcessRuntimeData(General.WebsocketGetRuntimeDataResponse runtimeData)
    {
        // TODO: instantiate order (destroy existing orders)
        // TODO: instantiate quick orders
        // TODO: instantiate  trading bots
    }

}