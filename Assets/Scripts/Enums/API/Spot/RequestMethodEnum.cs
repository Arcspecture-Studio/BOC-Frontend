public static class RequestMethodEnum
{
    // General
    public const string PING = "ping"; // Test connectivity to the WebSocket API.
    public const string TIME = "time"; // Test connectivity to the WebSocket API and get the current server time.
    public const string EXCHANGE_INFO = "exchangeInfo"; // Query current exchange trading rules, rate limits, and symbol information.

    // Market data
    public const string DEPTH = "depth"; // Get current order book.
    public const string TRADES_RECENT = "trades.recent"; // Get recent trades.
    public const string TRADES_HISTORICAL = "trades.historical"; // Get historical trades.
    public const string TRADES_AGGREGATE = "trades.aggregate"; // Get aggregate trades.
    public const string KLINES = "klines"; // Get klines (candlestick bars).
    public const string UI_KLINES = "uiKlines"; // Get klines (candlestick bars) optimized for presentation.
    public const string AVG_PRICE = "avgPrice"; // Get current average price for a symbol.
    public const string TICKER_24HR = "ticker.24hr"; // Get 24-hour rolling window price change statistics.
    public const string TICKER = "ticker"; // Get rolling window price change statistics with a custom window.
    public const string TICKER_PRICE = "ticker.price"; // Get the latest market price for a symbol.
    public const string TICKER_BOOK = "ticker.book"; // Get the current best price and quantity on the order book.

    // Trading
    public const string ORDER_PLACE = "order.place"; // Send in a new order.
}