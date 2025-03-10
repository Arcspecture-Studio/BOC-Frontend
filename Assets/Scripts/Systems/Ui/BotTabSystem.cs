using DG.Tweening;
using Newtonsoft.Json;
using UnityEngine;
using WebSocketSharp;

public class BotTabSystem : MonoBehaviour
{
    BotTabComponent botTabComponent;
    WebsocketComponent websocketComponent;
    ProfileComponent profileComponent;
    QuickTabComponent quickTabComponent;
    LoginComponent loginComponent;
    PlatformComponent platformComponent;
    SpawnBotComponent spawnTradingBotComponent;

    bool? active = null;
    Tween tween = null;
    int spawnedTradingBotObjectCount = -1;

    void Start()
    {
        botTabComponent = GlobalComponent.instance.botTabComponent;
        websocketComponent = GlobalComponent.instance.websocketComponent;
        profileComponent = GlobalComponent.instance.profileComponent;
        quickTabComponent = GlobalComponent.instance.quickTabComponent;
        loginComponent = GlobalComponent.instance.loginComponent;
        platformComponent = GlobalComponent.instance.platformComponent;
        spawnTradingBotComponent = GlobalComponent.instance.spawnTradingBotComponent;

        InitializeListener();
        ShowStretegySettingBasedOnBotType(0);
    }
    void Update()
    {
        MovePage();
        ShowAndHideQuickOrdersObject();
        SpawnTradingBotObject();
        DestroyTradingBotObject();
    }

    void InitializeListener()
    {
        botTabComponent.botTypeDropdown.onValueChanged.AddListener(ShowStretegySettingBasedOnBotType);
        botTabComponent.onChange_addToServer.AddListener(AddToServer);
        botTabComponent.onChange_deleteFromServer.AddListener(DeleteFromServer);
        botTabComponent.addBotButton.onClick.AddListener(() =>
        {
            botTabComponent.addToServer = true;
            botTabComponent.addBotButton.interactable = false;
        });
    }
    void AddToServer()
    {
        websocketComponent.generalRequests.Add(new General.WebsocketAddTradingBotRequest(
            loginComponent.token
        ));
    }
    void DeleteFromServer(string botId)
    {
        websocketComponent.generalRequests.Add(new General.WebsocketDeleteTradingBotRequest(loginComponent.token, botId));
    }
    void SpawnTradingBotObject()
    {
        string jsonString = websocketComponent.RetrieveGeneralResponses(WebsocketEventTypeEnum.ADD_TRADING_BOT);
        websocketComponent.RemovesGeneralResponses(WebsocketEventTypeEnum.ADD_TRADING_BOT);
        if (jsonString.IsNullOrEmpty()) return;

        General.WebsocketAddTradingBotResponse response = JsonConvert.DeserializeObject<General.WebsocketAddTradingBotResponse>(jsonString, JsonSerializerConfig.settings);

        botTabComponent.addBotButton.interactable = true;
        spawnTradingBotComponent.botToSpawn = response.tradingBot;
    }
    void DestroyTradingBotObject()
    {
        string jsonString = websocketComponent.RetrieveGeneralResponses(WebsocketEventTypeEnum.DELETE_TRADING_BOT);
        websocketComponent.RemovesGeneralResponses(WebsocketEventTypeEnum.DELETE_TRADING_BOT);
        if (jsonString.IsNullOrEmpty()) return;

        General.WebsocketDeleteTradingBotResponse response = JsonConvert.DeserializeObject<General.WebsocketDeleteTradingBotResponse>(jsonString, JsonSerializerConfig.settings);

        if (botTabComponent.spawnedBotDataObjects.TryGetValue(response.botId, out GameObject botDataObject))
        {
            Destroy(botDataObject);
            botTabComponent.spawnedBotDataObjects.Remove(response.botId);
        }
    }
    void MovePage()
    {
        if (active == botTabComponent.active) return;
        if (tween != null && tween.IsActive()) return;

        active = botTabComponent.active;
        float initialValue = active.Value ? botTabComponent.inactiveYPosition : botTabComponent.activeYPosition;
        float moveValue = active.Value ? botTabComponent.inactiveToActiveYMovement : botTabComponent.activeToInactiveYMovement;
        botTabComponent.rectTransform.anchoredPosition = new Vector2(botTabComponent.rectTransform.anchoredPosition.x,
            initialValue);
        tween = botTabComponent.rectTransform.DOBlendableLocalMoveBy(new Vector3(0, moveValue, 0), botTabComponent.pageMoveDuration).SetEase(botTabComponent.pageMoveEase);
    }
    void ShowAndHideQuickOrdersObject()
    {
        if (spawnedTradingBotObjectCount == botTabComponent.spawnedBotDataObjects.Count) return;
        spawnedTradingBotObjectCount = botTabComponent.spawnedBotDataObjects.Count;
        botTabComponent.botDataObjectList.SetActive(spawnedTradingBotObjectCount > 0);
    }
    void ShowStretegySettingBasedOnBotType(int value)
    {
        botTabComponent.premiumIndexSettingObj.SetActive(false);
        botTabComponent.mcdxSettingObj.SetActive(false);

        switch (botTabComponent.botType)
        {
            case BotTypeEnum.PREMIUM_INDEX:
                botTabComponent.premiumIndexSettingObj.SetActive(true);
                break;
            case BotTypeEnum.MCDX:
                botTabComponent.mcdxSettingObj.SetActive(true);
                break;
        }
    }
}