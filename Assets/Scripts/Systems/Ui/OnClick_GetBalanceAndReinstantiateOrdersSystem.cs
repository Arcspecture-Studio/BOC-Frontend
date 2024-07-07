using UnityEngine;
using UnityEngine.UI;

public class OnClick_GetBalanceAndReinstantiateOrdersSystem : MonoBehaviour
{
    GetRuntimeDataComponent getRuntimeDataComponent;

    Button button;

    void Start()
    {
        getRuntimeDataComponent = GlobalComponent.instance.getRuntimeDataComponent;

        button = GetComponent<Button>();

        button.onClick.AddListener(() =>
        {
            getRuntimeDataComponent.getRuntimeData = true;
        });
    }
}
