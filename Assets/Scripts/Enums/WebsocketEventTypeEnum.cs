﻿public enum WebsocketEventTypeEnum
{
    CONNECTION_ESTABLISH, // From Server

    #region Two Way
    VERSION_CHECKING,
    CREATE_ACCOUNT,
    GET_JWT,
    REVOKE_JWT,
    ADD_PLATFORM,
    REMOVE_PLATFORM,
    GET_PLATFORM_LIST,
    ADD_PROFILE,
    REMOVE_PROFILE,
    UPDATE_PROFILE,
    GET_PROFILES,
    GET_INITIAL_DATA,
    CALL_API,
    ADD_ORDER,
    UPDATE_ORDER,
    DELETE_ORDER,
    SUBMIT_ORDER,
    ADD_THROTTLE_ORDER,
    UPDATE_THROTTLE_ORDER,
    DELETE_THROTTLE_ORDER,
    SUBMIT_THROTTLE_ORDER,
    GET_RUNTIME_DATA,
    ADD_QUICK_ORDER,
    DELETE_QUICK_ORDER,
    ADD_TRADING_BOT,
    DELETE_TRADING_BOT,
    #endregion

    #region From Server
    POSITION_INFO_UPDATE,
    SPAWN_ORDER,
    #endregion

    #region Old Events
    // Both Way
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
    SPAWN_QUICK_ORDER,
    #endregion
}