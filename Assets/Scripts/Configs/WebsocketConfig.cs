using System.Security.Authentication;

public static class WebsocketConfig
{
    public static readonly string GENERAL_HOST_LOCAL = "ws://localhost";
    public static readonly string GENERAL_HOST = "ws://13.214.62.217";
    public static readonly string GENERAL_PORT = "3000";
    public static readonly string GENERAL_PORT_PRODUCTION = "3001";
    public static readonly string BINANCE_HOST_TEST = "wss://stream.binancefuture.com";
    public static readonly string BINANCE_HOST = "wss://fstream.binance.com";
    public static readonly string BINANCE_MARKET_PATH = "/stream";
    public static readonly string BINANCE_USER_DATA_PATH = "/ws/";
    public static readonly SslProtocols SSL_PROTOCOLS = SslProtocols.Tls12;
}
