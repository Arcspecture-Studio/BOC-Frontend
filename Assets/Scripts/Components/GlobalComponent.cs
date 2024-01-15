using UnityEngine;

public class GlobalComponent : MonoBehaviour
{
    public static GlobalComponent instance;

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    public PlatformComponent platformComponent;
    public BinanceComponent binanceComponent;
    public BinanceComponent binanceTestnetComponent;
    public WebsocketComponent websocketComponent;
    public WebrequestComponent webrequestComponent;
    public InputComponent inputComponent;
    public OrderPagesComponent orderPagesComponent;
    public PreferenceComponent preferenceComponent;
    public PromptComponent promptComponent;
    public LoginComponent loginComponent;
    public IoComponent ioComponent;
    public RetrieveOrdersComponent retrieveOrdersComponent;
    public SettingPageComponent settingPageComponent;
    public QuickTabComponent quickTabComponent;
    public HideAllPanelComponent hideAllPanelComponent;
}