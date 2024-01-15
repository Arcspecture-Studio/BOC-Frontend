using UnityEngine;
using UnityEngine.UI;

public class OnClick_OpenSettingPageSystem : MonoBehaviour
{
    SettingPageComponent settingPageComponent;
    HideAllPanelComponent hideAllPanelComponent;
    Button button;

    void Start()
    {
        settingPageComponent = GlobalComponent.instance.settingPageComponent;
        hideAllPanelComponent = GlobalComponent.instance.hideAllPanelComponent;
        button = GetComponent<Button>();

        button.onClick.AddListener(() =>
        {
            hideAllPanelComponent.hideNow = "settingPageComponent";

            settingPageComponent.active = !settingPageComponent.active;
        });
    }
}
