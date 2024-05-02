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

        botDataRowComponent.infoPanel.SetActive(false);
    }

    void OnClick_InfoButton()
    {
        bool isActive = !botDataRowComponent.infoPanel.activeSelf;
        botDataRowComponent.infoPanel.SetActive(isActive);

        if (isActive && !botDataRowComponent.infoPanelInstantiated)
        {
            botDataRowComponent.infoPanelInstantiated = true;
            InstantiateInfoPanelData();
        }
    }
    void InstantiateInfoPanelData()
    {

    }
    void OnClick_CloseButton()
    {
        botTabComponent.deleteFromServer = botDataRowComponent.botId;
        botDataRowComponent.closeButton.interactable = false;
    }
}
