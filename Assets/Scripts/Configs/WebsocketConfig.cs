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
        EnvironmentParameter.WEBSOCKET_SERVER_HOST ?? "ws://localhost",
        "3001",
        true,
        true
    );
    public static readonly WebsocketConfigEnvData production = new(
        EnvironmentParameter.WEBSOCKET_SERVER_HOST ?? "ws://localhost",
        "3002",
        false,
        true
    );
}