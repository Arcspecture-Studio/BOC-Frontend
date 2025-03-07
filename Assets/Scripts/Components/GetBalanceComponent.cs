using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using UnityEngine;
using UnityEngine.Events;

public class GetBalanceComponent : MonoBehaviour
{
    public bool getBalance
    {
        set { onChange_getBalance.Invoke(); }
    }
    [HideInInspector] public UnityEvent onChange_getBalance = new();
    public SerializedDictionary<string, float> processBalance
    {
        set { onChange_processBalance.Invoke(value); }
    }
    [HideInInspector] public UnityEvent<SerializedDictionary<string, float>> onChange_processBalance = new();
}