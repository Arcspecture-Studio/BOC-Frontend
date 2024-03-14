using UnityEngine;

public class GlobalComponent : MonoBehaviour
{
    public static GlobalComponent instance;

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    public PlatformComponentOld platformComponentOld;
    public BinanceComponent binanceComponent;
    public BinanceComponent binanceTestnetComponent;
    public WebsocketComponent websocketComponent;
    public WebrequestComponent webrequestComponent;
    public InputComponent inputComponent;
    public OrderPagesComponent orderPagesComponent;
    public PreferenceComponent preferenceComponent;
    public PromptComponent promptComponent;
    public LoginComponentOld loginComponentOld;
    public LoginComponent loginComponent;
    public IoComponent ioComponent;
    public RetrieveOrdersComponent retrieveOrdersComponent;
    public SettingPageComponent settingPageComponent;
    public QuickTabComponent quickTabComponent;
    public HideAllPanelComponent hideAllPanelComponent;
    public TradingBotComponent tradingBotComponent;
    public MiniPromptComponent miniPromptComponent;
    public GetInitialDataComponent getInitialDataComponent;
    public PlatformComponent platformComponent;
}