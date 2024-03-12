using UnityEngine;
using UnityEngine.Events;

public class IoComponent : MonoBehaviour
{
    [Header("Config")]
    public string tokenFileName;

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
    public bool readToken
    {
        set { onChange_readToken.Invoke(); }
    }
    [HideInInspector] public UnityEvent onChange_readToken = new();
    public bool writeToken;
}