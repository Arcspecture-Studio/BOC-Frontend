using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GetBalanceComponent : MonoBehaviour
{
    public bool getBalance
    {
        set { onChange_getBalance.Invoke(); }
    }
    [HideInInspector] public UnityEvent onChange_getBalance = new();
    public Dictionary<string, double> processBalance
    {
        set { onChange_processBalance.Invoke(value); }
    }
    [HideInInspector] public UnityEvent<Dictionary<string, double>> onChange_processBalance = new();
}