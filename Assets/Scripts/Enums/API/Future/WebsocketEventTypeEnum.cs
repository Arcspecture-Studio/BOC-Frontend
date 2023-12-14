public enum WebsocketEventTypeEnum
{
    #region General
    // Both Way
    CALL_API,
    SAVE_ORDER,
    SAVE_THROTTLE_ORDER,

    // From Server
    CONNECTION_ID,
    RETRIEVE_ORDERS,
    ACCOUNT_OVERWRITE,
    INVALID_LOGIN_PHRASE,
    RETRIEVE_POSITION_INFO,
    VERSION_MISMATCH,

    // To Server
    SYNC_API_KEY,
    LOGOUT,
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