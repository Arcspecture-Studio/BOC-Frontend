using UnityEngine;
using UnityEngine.UI;

public class OnClick_OpenQuickTabSystem : MonoBehaviour
{
    QuickTabComponent quickTabComponent;
    HideAllPanelComponent hideAllPanelComponent;
    Button button;

    void Start()
    {
        quickTabComponent = GlobalComponent.instance.quickTabComponent;
        hideAllPanelComponent = GlobalComponent.instance.hideAllPanelComponent;

        button = GetComponent<Button>();

        button.onClick.AddListener(() =>
        {
            hideAllPanelComponent.hideNow = "quickTabComponent";

            quickTabComponent.active = !quickTabComponent.active;
        });
    }
}
