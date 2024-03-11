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
    LoginComponentOld loginComponent;
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

        ioComponent.readPreferences = true;
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
    void WriteIntoApiKeyFile()
    {
        if (!ioComponent.writeApiKey) return;
        ioComponent.writeApiKey = false;
        if (websocketComponent.generalSocketIv == null) return;
        List<ApiKeyFile> data = new List<ApiKeyFile>();
        if (binanceComponent.loggedIn) data.Add(new ApiKeyFile(PlatformEnum.BINANCE, binanceComponent.apiKey, binanceComponent.apiSecret, binanceComponent.loginPhrase));
        if (binanceTestnetComponent.loggedIn) data.Add(new ApiKeyFile(PlatformEnum.BINANCE_TESTNET, binanceTestnetComponent.apiKey, binanceTestnetComponent.apiSecret, binanceTestnetComponent.loginPhrase));
        if (data.Count == 0)
        {
            if (File.Exists(ioComponent.path + ioComponent.apiKeyFileName))
            {
                File.Delete(ioComponent.path + ioComponent.apiKeyFileName);
            }
        }
        else
        {
            string jsonString = JsonConvert.SerializeObject(data, JsonSerializerConfig.settings);
            Write(ioComponent.apiKeyFileName, jsonString);
        }
    }
    void ReadFromApiKeyFile()
    {
        if (!ioComponent.readApiKey) return;
        ioComponent.readApiKey = false;
        bool fileNotExist = !File.Exists(ioComponent.path + ioComponent.apiKeyFileName);
        loginComponent.allowInput = fileNotExist;
        if (fileNotExist) return;
        StreamReader reader = new StreamReader(ioComponent.path + ioComponent.apiKeyFileName);
        string jsonString = reader.ReadToEnd();
        reader.Close();
        try
        {
            List<ApiKeyFile> datas = JsonConvert.DeserializeObject<List<ApiKeyFile>>(jsonString, JsonSerializerConfig.settings);
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
            Debug.LogError(logPrefix + ex);
            loginComponent.allowInput = true;
            string message = "Unable to read data from file named " + ioComponent.apiKeyFileName + " as the data has been manually modified.";
            promptComponent.ShowPrompt("ERROR", message, () =>
            {
                promptComponent.active = false;
            });
        }
    }
    void ReadFromPreferencesFile()
    {
        if (!ioComponent.readPreferences) return;
        ioComponent.readPreferences = false;
        bool fileNotExist = !File.Exists(ioComponent.path + ioComponent.preferencesFileName);
        if (fileNotExist)
        {
            if (!readApiKeyAdy)
            {
                readApiKeyAdy = true;
                ioComponent.readApiKey = readApiKeyAdy;
            }
            return;
        }
        StreamReader reader = new StreamReader(ioComponent.path + ioComponent.preferencesFileName);
        string jsonString = reader.ReadToEnd();
        reader.Close();
        try
        {
            PreferenceFile preferenceFile = JsonConvert.DeserializeObject<PreferenceFile>(jsonString, JsonSerializerConfig.settings);
            preferenceComponent.UpdateValue(preferenceFile);
            settingPageComponent.syncSetting = true;
            quickTabComponent.syncDataFromPreference = true;
            if (!readApiKeyAdy)
            {
                readApiKeyAdy = true;
                ioComponent.readApiKey = readApiKeyAdy;
            }
        }
        catch (Exception ex)
        {
            Debug.LogError(logPrefix + ex);
            string message = "Unable to read data from file named " + ioComponent.preferencesFileName + " as the data has been manually modified.";
            promptComponent.ShowPrompt("ERROR", message, () =>
            {
                promptComponent.active = false;
            });
        }
    }
    void WriteIntoPreferencesFile()
    {
        if (!ioComponent.writePreferences) return;
        ioComponent.writePreferences = false;
        Write(ioComponent.preferencesFileName, preferenceComponent.GetJsonString());
    }
}