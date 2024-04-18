using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GetBalanceComponent : MonoBehaviour
{
    // TODO: get balance
    public Dictionary<string, double> processBalance
    {
        set { onChange_processBalance.Invoke(value); }
    }
    [HideInInspector] public UnityEvent<Dictionary<string, double>> onChange_processBalance = new();
}