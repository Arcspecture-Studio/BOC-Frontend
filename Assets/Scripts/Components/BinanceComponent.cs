public class BinanceComponent : PlatformTemplateComponent
{
    public bool getExchangeInfo;

    void Start()
    {
        loggedIn = false;
        apiKey = null;
        apiSecret = null;
        marginAssets = new();
        quantityPrecisions = new();
        pricePrecisions = new();
        fees = new();
    }
}