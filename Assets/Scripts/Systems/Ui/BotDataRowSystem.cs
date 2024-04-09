using UnityEngine;

public class BotDataRowSystem : MonoBehaviour
{
    BotDataRowComponent botDataRowComponent;
    BotTabComponent botTabComponent;

    void Start()
    {
        botDataRowComponent = GetComponent<BotDataRowComponent>();
        botTabComponent = GlobalComponent.instance.botTabComponent;

        botDataRowComponent.infoButton.onClick.AddListener(OnClick_InfoButton);
        botDataRowComponent.closeButton.onClick.AddListener(OnClick_CloseButton);
    }

    void OnClick_InfoButton()
    {
        // TODO
    }
    void OnClick_CloseButton()
    {
        botTabComponent.deleteFromServer = botDataRowComponent.botId;
        botDataRowComponent.closeButton.interactable = false;
    }
}
