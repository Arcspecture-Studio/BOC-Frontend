using Unity.Plastic.Newtonsoft.Json;
using UnityEngine;
using WebSocketSharp;

public class OrderPagesSpawnOrderBasedOnServerSignalSystem : MonoBehaviour
{
    WebsocketComponent websocketComponent;
    HideAllPanelComponent hideAllPanelComponent;
    SpawnOrderComponent spawnOrderComponent;

    void Start()
    {
        websocketComponent = GlobalComponent.instance.websocketComponent;
        hideAllPanelComponent = GlobalComponent.instance.hideAllPanelComponent;
        spawnOrderComponent = GlobalComponent.instance.spawnOrderComponent;
    }
    void Update()
    {
        string spawnOrderString = websocketComponent.RetrieveGeneralResponses(WebsocketEventTypeEnum.SPAWN_ORDER);
        websocketComponent.RemovesGeneralResponses(WebsocketEventTypeEnum.SPAWN_ORDER);
        if (spawnOrderString.IsNullOrEmpty()) return;

        General.WebsocketSpawnOrderResponse response = JsonConvert.DeserializeObject<General.WebsocketSpawnOrderResponse>(spawnOrderString, JsonSerializerConfig.settings);

        spawnOrderComponent.orderToSpawn = response.order;
        hideAllPanelComponent.hideNow = "true";
    }
}