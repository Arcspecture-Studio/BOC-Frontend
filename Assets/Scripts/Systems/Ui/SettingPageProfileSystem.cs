using UnityEngine;

public class SettingPageProfileSystem : MonoBehaviour
{
    ProfileComponent profileComponent;
    SettingPageComponent settingPageComponent;
    void Start()
    {
        profileComponent = GlobalComponent.instance.profileComponent;
        settingPageComponent = GlobalComponent.instance.settingPageComponent;
    }
    void Update()
    {

    }
}
