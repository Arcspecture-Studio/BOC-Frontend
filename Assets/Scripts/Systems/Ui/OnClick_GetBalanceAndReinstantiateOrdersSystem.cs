using UnityEngine;
using UnityEngine.UI;

public class OnClick_GetBalanceAndReinstantiateOrdersSystem : MonoBehaviour
{
    PlatformComponent platformComponent;
    HideAllPanelComponent hideAllPanelComponent;
    GetRuntimeDataComponent getRuntimeDataComponent;

    Button button;

    void Start()
    {
        platformComponent = GlobalComponent.instance.platformComponent;
        hideAllPanelComponent = GlobalComponent.instance.hideAllPanelComponent;
        getRuntimeDataComponent = GlobalComponent.instance.getRuntimeDataComponent;

        button = GetComponent<Button>();

        button.onClick.AddListener(() =>
        {
            hideAllPanelComponent.hideNow = "true";

            getRuntimeDataComponent.getRuntimeData = true;
        });
    }
}
