public enum WebsocketEventTypeEnum
{
    #region General
    // Both Way
    VERSION_CHECKING,
    CALL_API,
    SAVE_ORDER,
    SAVE_THROTTLE_ORDER,
    RETRIEVE_ORDERS,

    // To Server
    SYNC_API_KEY,
    LOGOUT,

    // From Server
    CONNECTION_ID,
    ACCOUNT_OVERWRITE,
    INVALID_LOGIN_PHRASE,
    RETRIEVE_POSITION_INFO,
    #endregion

    #region Binance
    MARGIN_CALL,
    ACCOUNT_UPDATE, // balance and position update
    ORDER_TRADE_UPDATE, // order update
    ACCOUNT_CONFIG_UPDATE,
    STRATEGY_UPDATE,
    GRID_UPDATE
    #endregion
}