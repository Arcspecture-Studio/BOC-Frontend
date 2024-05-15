using UnityEngine;
using UnityEngine.UI;

public class OnClick_GetBalanceAndReinstantiateOrdersSystem : MonoBehaviour
{
    GetRuntimeDataComponent getRuntimeDataComponent;
    LoadingComponent loadingComponent;

    Button button;

    void Start()
    {
        getRuntimeDataComponent = GlobalComponent.instance.getRuntimeDataComponent;
        loadingComponent = GlobalComponent.instance.loadingComponent;

        button = GetComponent<Button>();

        button.onClick.AddListener(() =>
        {
            getRuntimeDataComponent.getRuntimeData = true;
            loadingComponent.active = true;
        });
    }
}
