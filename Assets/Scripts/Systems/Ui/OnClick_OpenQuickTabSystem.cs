using UnityEngine;
using UnityEngine.UI;

public class OnClick_OpenQuickTabSystem : MonoBehaviour
{
    QuickTabComponent quickTabComponent;
    Button button;

    void Start()
    {
        quickTabComponent = GlobalComponent.instance.quickTabComponent;
        button = GetComponent<Button>();

        button.onClick.AddListener(() =>
        {
            quickTabComponent.active = !quickTabComponent.active;
        });
    }
}
