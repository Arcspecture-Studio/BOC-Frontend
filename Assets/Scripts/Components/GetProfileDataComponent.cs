using UnityEngine;
using UnityEngine.Events;

public class GetProfileDataComponent : MonoBehaviour
{
    public bool getProfileData
    {
        set { onChange_getProfileData.Invoke(); }
    }
    [HideInInspector] public UnityEvent onChange_getProfileData = new();
    public General.WebsocketGetProfileDataResponse processProfileData
    {
        set { onChange_processProfileData.Invoke(value); }
    }
    [HideInInspector] public UnityEvent<General.WebsocketGetProfileDataResponse> onChange_processProfileData = new();
}