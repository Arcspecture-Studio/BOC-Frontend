using UnityEngine;
using UnityEngine.UI;

public class OnClick_OpenTradingBotTabSystem : MonoBehaviour
{
    BotTabComponent botTabComponent;
    HideAllPanelComponent hideAllPanelComponent;
    Button button;

    void Start()
    {
        botTabComponent = GlobalComponent.instance.botTabComponent;
        hideAllPanelComponent = GlobalComponent.instance.hideAllPanelComponent;

        button = GetComponent<Button>();

        button.onClick.AddListener(() =>
        {
            hideAllPanelComponent.hideNow = "botTabComponent";

            botTabComponent.active = !botTabComponent.active;
        });
    }
}
