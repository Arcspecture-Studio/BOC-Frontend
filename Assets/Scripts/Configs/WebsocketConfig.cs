using System;

public static class WebsocketConfig
{
    public static readonly WebsocketConfigEnvData develop = new(
        "ws://localhost",
        "3000",
        true,
        false
    );
    public static readonly WebsocketConfigEnvData test = new(
        Environment.GetEnvironmentVariable("WEBSOCKET_SERVER_HOST"),
        "3001",
        true,
        true
    );
    public static readonly WebsocketConfigEnvData production = new(
        Environment.GetEnvironmentVariable("WEBSOCKET_SERVER_HOST"),
        "3002",
        false,
        true
    );
}