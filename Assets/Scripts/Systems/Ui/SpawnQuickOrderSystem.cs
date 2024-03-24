using UnityEngine;

public class SpawnQuickOrderSystem : MonoBehaviour
{
    SpawnQuickOrderComponent spawnQuickOrderComponent;

    void Start()
    {
        spawnQuickOrderComponent = GlobalComponent.instance.spawnQuickOrderComponent;

        spawnQuickOrderComponent.onChange_quickOrderToSpawn.AddListener(SpawnQuickOrder);
    }

    void SpawnQuickOrder(General.WebsocketGetQuickOrderResponse response)
    {

    }
}