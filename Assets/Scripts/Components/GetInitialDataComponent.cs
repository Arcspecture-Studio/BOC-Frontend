using UnityEngine;
using UnityEngine.Events;

public class GetInitialDataComponent : MonoBehaviour
{
    /* Current usages
     - on login and get token
     - on add new platform
     - on remove a platform
     - on remove a profile
     - on change platform in a profile
     - on change default profile
    */
    public bool getInitialData
    {
        set { onChange_getInitialData.Invoke(); }
    }
    [HideInInspector] public UnityEvent onChange_getInitialData = new();
}