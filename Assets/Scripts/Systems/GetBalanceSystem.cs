using System.Collections.Generic;
using UnityEngine;

public class GetBalanceSystem : MonoBehaviour
{
    GetBalanceComponent getBalanceComponent;
    PlatformComponent platformComponent;

    void Start()
    {
        getBalanceComponent = GlobalComponent.instance.getBalanceComponent;
        platformComponent = GlobalComponent.instance.platformComponent;

        getBalanceComponent.onChange_processBalance.AddListener(ProcessBalance);
    }

    void ProcessBalance(Dictionary<string, double> balances)
    {
        platformComponent.walletBalances = balances;
    }
}