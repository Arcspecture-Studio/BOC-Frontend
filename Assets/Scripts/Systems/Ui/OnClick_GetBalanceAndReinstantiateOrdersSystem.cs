using UnityEngine;
using UnityEngine.UI;

public class OnClick_GetBalanceAndReinstantiateOrdersSystem : MonoBehaviour
{
    GetRuntimeDataComponent getRuntimeDataComponent;
    QuickTabComponent quickTabComponent;
    ProfileComponent profileComponent;

    Button button;

    void Start()
    {
        getRuntimeDataComponent = GlobalComponent.instance.getRuntimeDataComponent;
        quickTabComponent = GlobalComponent.instance.quickTabComponent;
        profileComponent = GlobalComponent.instance.profileComponent;

        button = GetComponent<Button>();

        button.onClick.AddListener(() =>
        {
            getRuntimeDataComponent.getRuntimeData = true;

            PreferenceQuickOrder preferenceQuickOrder = profileComponent.activeProfile.preference.quickOrder;
            quickTabComponent.slPriceInput.text = preferenceQuickOrder.slPrice.ToString();
        });
    }
}
