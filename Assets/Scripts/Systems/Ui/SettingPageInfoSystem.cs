using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SettingPageInfoSystem : MonoBehaviour
{
    SettingPageComponent settingPageComponent;
    PlatformComponent platformComponent;
    OrderPagesComponent orderPagesComponent;

    Dictionary<string, TMP_Text> walletBalances = new();
    int totalOrders = -1;

    void Start()
    {
        settingPageComponent = GlobalComponent.instance.settingPageComponent;
        platformComponent = GlobalComponent.instance.platformComponent;
        orderPagesComponent = GlobalComponent.instance.orderPagesComponent;

        settingPageComponent.onChange_updateInfoUI.AddListener(UpdateInfoUI);

        EventManager.AddListener(EventManagerConfig.UPDATE_ORDER_STATUS, OnUpdateOrderStatus);
    }
    void Update()
    {
        UpdateTotalOrders();
    }

    void UpdateInfoUI()
    {
        settingPageComponent.activePlatformText.text = platformComponent.activePlatform.ToString();
        foreach (KeyValuePair<string, float> walletBalance in platformComponent.walletBalances)
        {
            if (walletBalance.Value == 0)
            {
                if (walletBalances.ContainsKey(walletBalance.Key))
                {
                    walletBalances[walletBalance.Key].transform.parent.gameObject.SetActive(false);
                }
                continue;
            }
            if (!walletBalances.ContainsKey(walletBalance.Key))
            {
                GameObject balanceLabelObj = Instantiate(settingPageComponent.balanceLabelPrefab, settingPageComponent.InfoBodyPanel);
                balanceLabelObj.transform.GetChild(0).GetComponent<TMP_Text>().text = walletBalance.Key + " Balance: ";
                walletBalances.Add(walletBalance.Key, balanceLabelObj.transform.GetChild(1).GetComponent<TMP_Text>());
            }
            walletBalances[walletBalance.Key].transform.parent.gameObject.SetActive(true);
            walletBalances[walletBalance.Key].text = Utils.TruncTwoDecimal(walletBalance.Value).ToString();
        }
    }
    void UpdateTotalOrders()
    {
        if (totalOrders == orderPagesComponent.childOrderPageComponents.Count) return;
        totalOrders = orderPagesComponent.childOrderPageComponents.Count;
        settingPageComponent.totalOrdersText.text = orderPagesComponent.childOrderPageComponents.Count.ToString();
        StartCoroutine(UpdateTotalOrdersInPosition());
    }
    void OnUpdateOrderStatus(object parameter)
    {
        StartCoroutine(UpdateTotalOrdersInPosition());
    }
    IEnumerator UpdateTotalOrdersInPosition()
    {
        yield return new WaitUntil(() => totalOrders == orderPagesComponent.childOrderPageComponents.Count);

        int totalOrdersInPosition = 0;
        float totalAmountInPosition = 0;

        foreach (OrderPageComponent orderPageComponent in orderPagesComponent.childOrderPageComponents)
        {
            if (orderPageComponent.orderStatus == OrderStatusEnum.FILLED)
            {
                totalOrdersInPosition++;
                totalAmountInPosition += orderPageComponent.marginCalculator.totalLossAmount;
            }
        }
        settingPageComponent.totalOrdersInPositionText.text = totalOrdersInPosition.ToString();
        settingPageComponent.totalAmountInPositionText.text = Utils.RoundTwoDecimal(totalAmountInPosition).ToString();
    }
}
