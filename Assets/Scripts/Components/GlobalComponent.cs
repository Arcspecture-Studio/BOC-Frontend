using UnityEngine;

public class GlobalComponent : MonoBehaviour
{
    public static GlobalComponent instance;

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    public BinanceComponent binanceComponent;
    public BinanceComponent binanceTestnetComponent;
    public WebsocketComponent websocketComponent;
    public WebrequestComponent webrequestComponent;
    public InputComponent inputComponent;
    public OrderPagesComponent orderPagesComponent;
    public PromptComponent promptComponent;
    public LoginComponent loginComponent;
    public IoComponent ioComponent;
    public SettingPageComponent settingPageComponent;
    public QuickTabComponent quickTabComponent;
    public HideAllPanelComponent hideAllPanelComponent;
    public TradingBotComponent tradingBotComponent;
    public MiniPromptComponent miniPromptComponent;
    public GetInitialDataComponent getInitialDataComponent;
    public GetRuntimeDataComponent getRuntimeDataComponent;
    public PlatformComponent platformComponent;
    public ProfileComponent profileComponent;
    public SpawnOrderComponent spawnOrderComponent;
    public SpawnQuickOrderComponent spawnQuickOrderComponent;
    public SpawnTradingBotComponent spawnTradingBotComponent;
    public TradingBotTabComponent tradingBotTabComponent;
}