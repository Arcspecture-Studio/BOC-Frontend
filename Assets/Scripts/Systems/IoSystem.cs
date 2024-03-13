using Newtonsoft.Json;
using System;
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
    GetInitialDataComponent getInitialDataComponent;
    string logPrefix = "[IoSystem] ";
    bool readTokenAdy = false;

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
        getInitialDataComponent = GlobalComponent.instance.getInitialDataComponent;

        ioComponent.editorPath = Application.dataPath + Path.AltDirectorySeparatorChar + "Saved Data" + Path.AltDirectorySeparatorChar;
        ioComponent.persistentPath = Application.persistentDataPath + Path.AltDirectorySeparatorChar;

        ioComponent.onChange_readToken.AddListener(ReadFromTokenFile);
    }
    void Update()
    {
        ReadTokenFromUpdate();
        WriteIntoTokenFileFromUpdate();
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
    void CheckVersion()
    {
        General.WebsocketGeneralRequest request = new(WebsocketEventTypeEnum.VERSION_CHECKING);
        websocketComponent.generalRequests.Add(request);
    }
    void ReadTokenFromUpdate()
    {
        if (readTokenAdy) return;
        readTokenAdy = true;
        ioComponent.readToken = true;
    }
    void ReadFromTokenFile()
    {
        bool fileNotExist = !File.Exists(ioComponent.path + ioComponent.tokenFileName);
        if (fileNotExist)
        {
            loginComponent.loginStatus = LoginPageStatusEnum.REGISTER;
            CheckVersion();
            return;
        }

        StreamReader reader = new StreamReader(ioComponent.path + ioComponent.tokenFileName);
        string jsonString = reader.ReadToEnd();
        reader.Close();

        try // to serialize the json string
        {
            TokenFile data = JsonConvert.DeserializeObject<TokenFile>(jsonString, JsonSerializerConfig.settings);
            loginComponent.token = Encryption.Decrypt(data.token, SecretConfig.ENCRYPTION_ACCESS_TOKEN_32, data.cache);
            ioComponent.writeToken = true;
            getInitialDataComponent.getInitialData = true;
        }
        catch (Exception ex)
        {
            Debug.LogError(logPrefix + ex);
            promptComponent.ShowPrompt(PromptConstant.NOTICE, PromptConstant.UNABLE_TO_CONTINUE_LOGIN, () => { promptComponent.active = false; });

            loginComponent.loginStatus = LoginPageStatusEnum.LOGIN;
            CheckVersion();
        }
    }
    void WriteIntoTokenFileFromUpdate()
    {
        if (!ioComponent.writeToken ||
            websocketComponent.generalSocketIv.Length != EncryptionConfig.IV_LENGTH ||
            loginComponent.token.IsNullOrEmpty()
        ) return;
        ioComponent.writeToken = false;

        string token = Encryption.Encrypt(loginComponent.token, SecretConfig.ENCRYPTION_ACCESS_TOKEN_32, websocketComponent.generalSocketIv);
        TokenFile file = new(token, websocketComponent.generalSocketIv);
        string jsonString = JsonConvert.SerializeObject(file, JsonSerializerConfig.settings);
        Write(ioComponent.tokenFileName, jsonString);
    }
}