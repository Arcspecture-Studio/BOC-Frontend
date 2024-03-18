public class BinanceComponent : PlatformTemplateComponent
{
    public bool getExchangeInfo;

    void Start()
    {
        loggedIn = false;
        marginAssets = new();
        quantityPrecisions = new();
        pricePrecisions = new();
        fees = new();
    }
}