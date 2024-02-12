﻿public enum WebsocketEventTypeEnum
{
    #region General
    // Both Way
    VERSION_CHECKING,
    CALL_API,
    SAVE_ORDER,
    SAVE_THROTTLE_ORDER,
    SAVE_QUICK_ORDER,
    SAVE_TRADING_BOT,
    RETRIEVE_ORDERS,
    RETRIEVE_TRADING_BOTS,

    // To Server
    SIGNUP,
    LOGIN,
    SYNC_API_KEY,
    LOGOUT,

    // From Server
    CONNECTION_ID,
    ACCOUNT_OVERWRITE,
    INVALID_LOGIN_PHRASE,
    RETRIEVE_POSITION_INFO,
    SPAWN_QUICK_ORDER,
    SPAWN_ORDER
    #endregion
}