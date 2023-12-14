using UnityEngine;
using UnityEngine.UI;

public class OnClick_OpenSettingPageSystem : MonoBehaviour
{
    SettingPageComponent settingPageComponent;
    Button button;

    void Start()
    {
        settingPageComponent = GlobalComponent.instance.settingPageComponent;
        button = GetComponent<Button>();

        button.onClick.AddListener(() =>
        {
            settingPageComponent.active = !settingPageComponent.active;
        });
    }
}
