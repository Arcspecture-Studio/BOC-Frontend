using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using WebSocketSharp;

public class IoSystem : MonoBehaviour
{
    IoComponent ioComponent;
    WebsocketComponent websocketComponent;
    BinanceComponent binanceComponent;
    BinanceComponent binanceTestnetComponent;
    LoginComponent loginComponent;
    PromptComponent promptComponent;
    PlatformComponent platformComponent;
    PreferenceComponent preferenceComponent;
    SettingPageComponent settingPageComponent;
    QuickTabComponent quickTabComponent;
    string logPrefix = "[IoSystem] ";
    bool readApiKeyAdy = false;

    void Start()
    {
        ioComponent = GlobalComponent.instance.ioComponent;
        websocketComponent = GlobalComponent.instance.websocketComponent;
        binanceComponent = GlobalComponent.instance.binanceComponent;
        binanceTestnetComponent = GlobalComponent.instance.binanceTestnetComponent;
        loginComponent = GlobalComponent.instance.loginComponent;
        promptComponent = GlobalComponent.instance.promptComponent;
        platformComponent = GlobalComponent.instance.platformComponent;
        preferenceComponent = GlobalComponent.instance.preferenceComponent;
        settingPageComponent = GlobalComponent.instance.settingPageComponent;
        quickTabComponent = GlobalComponent.instance.quickTabComponent;

        ioComponent.editorPath = Application.dataPath + Path.AltDirectorySeparatorChar + "Saved Data" + Path.AltDirectorySeparatorChar;
        ioComponent.persistentPath = Application.persistentDataPath + Path.AltDirectorySeparatorChar;

        ioComponent.onChange_readToken.AddListener(ReadFromTokenFile);
        ioComponent.readToken = true;
    }
    void Update()
    {
        ReadFromPreferencesFile();
        ReadFromApiKeyFile();
        WriteIntoApiKeyFile();
        WriteIntoPreferencesFile();
    }

    void Write(string fileName, string jsonString)
    {
        if (!Directory.Exists(ioComponent.path))
        {
            Directory.CreateDirectory(ioComponent.path);
        }
        StreamWriter writer = new StreamWriter(ioComponent.path + fileName);
        writer.Write(jsonString);
        writer.Close();
    }
    void ReadFromTokenFile()
    {
        bool fileNotExist = !File.Exists(ioComponent.path + ioComponent.tokenFileName);
        if (fileNotExist) return;

        StreamReader reader = new StreamReader(ioComponent.path + ioComponent.tokenFileName);
        string jsonString = reader.ReadToEnd();
        reader.Close();

        try // to serialize the json string
        {
            TokenFile data = JsonConvert.DeserializeObject<TokenFile>(jsonString, JsonSerializerConfig.settings);
            loginComponent.loginStatus = LoginPageStatusEnum.LOGGED_IN;
            loginComponent.token = Encryption.Decrypt(data.token, SecretConfig.ENCRYPTION_ACCESS_TOKEN_32, data.cache);
        }
        catch (Exception ex)
        {
            Debug.LogError(logPrefix + ex);
            string message = "Unable to read data from file named " + ioComponent.tokenFileName + " as the data has been manually modified.";
            promptComponent.ShowPrompt("ERROR", message, () => { promptComponent.active = false; });
        }
    }
    void WriteIntoApiKeyFile()
    {
        if (websocketComponent.generalSocketIv == null) return;
        List<ApiKeyFile> data = new List<ApiKeyFile>();
        if (binanceComponent.loggedIn) data.Add(new ApiKeyFile(PlatformEnum.BINANCE, binanceComponent.apiKey, binanceComponent.apiSecret, binanceComponent.loginPhrase));
        if (binanceTestnetComponent.loggedIn) data.Add(new ApiKeyFile(PlatformEnum.BINANCE_TESTNET, binanceTestnetComponent.apiKey, binanceTestnetComponent.apiSecret, binanceTestnetComponent.loginPhrase));
        if (data.Count == 0)
        {
            // if (File.Exists(ioComponent.path + ioComponent.apiKeyFileName))
            // {
            //     File.Delete(ioComponent.path + ioComponent.apiKeyFileName);
            // }
        }
        else
        {
            string jsonString = JsonConvert.SerializeObject(data, JsonSerializerConfig.settings);
            // Write(ioComponent.apiKeyFileName, jsonString);
        }
    }
    void ReadFromApiKeyFile()
    {
        try
        {
            List<ApiKeyFile> datas = JsonConvert.DeserializeObject<List<ApiKeyFile>>("jsonString", JsonSerializerConfig.settings);
            Dictionary<PlatformEnum, bool> loggedIn = new();
            datas.ForEach(data =>
            {
                websocketComponent.syncApiKeyToServer = true;
                switch (data.platform)
                {
                    case PlatformEnum.BINANCE:
                        if (data.apiKey.IsNullOrEmpty() || data.apiSecret.IsNullOrEmpty()) break;
                        loggedIn.TryAdd(PlatformEnum.BINANCE, true);
                        binanceComponent.apiKey = data.apiKey;
                        binanceComponent.apiSecret = data.apiSecret;
                        binanceComponent.loginPhrase = data.loginPhrase;
                        binanceComponent.getBalance = true;
                        break;
                    case PlatformEnum.BINANCE_TESTNET:
                        if (data.apiKey.IsNullOrEmpty() || data.apiSecret.IsNullOrEmpty()) break;
                        loggedIn.TryAdd(PlatformEnum.BINANCE_TESTNET, true);
                        binanceTestnetComponent.apiKey = data.apiKey;
                        binanceTestnetComponent.apiSecret = data.apiSecret;
                        binanceTestnetComponent.loginPhrase = data.loginPhrase;
                        binanceTestnetComponent.getBalance = true;
                        break;
                }
            });
            if (loggedIn.ContainsKey(preferenceComponent.tradingPlatform))
            {
                platformComponent.tradingPlatform = preferenceComponent.tradingPlatform;
            }
            else
            {
                foreach (KeyValuePair<PlatformEnum, bool> logged in loggedIn)
                {
                    if (logged.Value)
                    {
                        platformComponent.tradingPlatform = logged.Key;
                        break;
                    }
                }
            }
        }
        catch (Exception ex)
        {
        }
    }
    void ReadFromPreferencesFile()
    {
        try
        {
            PreferenceFile preferenceFile = JsonConvert.DeserializeObject<PreferenceFile>("jsonString", JsonSerializerConfig.settings);
            preferenceComponent.UpdateValue(preferenceFile);
            settingPageComponent.syncSetting = true;
            quickTabComponent.syncDataFromPreference = true;
            if (!readApiKeyAdy)
            {
                readApiKeyAdy = true;
                // ioComponent.readApiKey = readApiKeyAdy;
            }
        }
        catch (Exception ex)
        {
        }
    }
    void WriteIntoPreferencesFile()
    {
        // if (!ioComponent.writePreferences) return;
        // ioComponent.writePreferences = false;
        // Write(ioComponent.preferencesFileName, preferenceComponent.GetJsonString());
    }
}