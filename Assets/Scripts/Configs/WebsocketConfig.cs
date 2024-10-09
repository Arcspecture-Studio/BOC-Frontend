public static class WebsocketConfig
{
    public static readonly WebsocketConfigEnvData develop = new(
        "ws://localhost",
        "3000",
        true,
        false
    );
    public static readonly WebsocketConfigEnvData test = new(
        "ws://52.198.114.90",
        "3001",
        true,
        true
    );
    public static readonly WebsocketConfigEnvData production = new(
        "ws://52.198.114.90",
        "3002",
        false,
        true
    );
}