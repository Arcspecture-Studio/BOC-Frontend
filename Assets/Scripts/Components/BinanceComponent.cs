public class BinanceComponent : PlatformTemplateComponent
{
    public string loginPhrase = null; // TODO: remove
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