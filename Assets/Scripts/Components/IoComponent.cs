using UnityEngine;

public class IoComponent : MonoBehaviour
{
    [Header("Config")]
    public string websocketConnectionFileName;
    public string apiKeyFileName;
    public string ordersFileName;
    public string preferencesFileName;

    [Header("Runtime")]
    public string editorPath;
    public string persistentPath;
    public string path
    {
        get
        {
        #if UNITY_EDITOR
            return editorPath;
        #else
            return persistentPath;
        #endif
        }
    }
    public bool writeWebsocketConnection = false;
    public bool writeApiKey = false;
    public bool writePreferences = false;
    public bool readApiKey = false;
    public bool readPreferences = false;
}