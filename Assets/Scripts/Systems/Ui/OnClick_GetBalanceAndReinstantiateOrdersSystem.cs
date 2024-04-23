using UnityEngine;
using UnityEngine.UI;

public class OnClick_GetBalanceAndReinstantiateOrdersSystem : MonoBehaviour
{
    HideAllPanelComponent hideAllPanelComponent;
    GetRuntimeDataComponent getRuntimeDataComponent;
    LoadingComponent loadingComponent;

    Button button;

    void Start()
    {
        hideAllPanelComponent = GlobalComponent.instance.hideAllPanelComponent;
        getRuntimeDataComponent = GlobalComponent.instance.getRuntimeDataComponent;
        loadingComponent = GlobalComponent.instance.loadingComponent;

        button = GetComponent<Button>();

        button.onClick.AddListener(() =>
        {
            hideAllPanelComponent.hideNow = "true";

            getRuntimeDataComponent.getRuntimeData = true;
            loadingComponent.active = true;
        });
    }
}
