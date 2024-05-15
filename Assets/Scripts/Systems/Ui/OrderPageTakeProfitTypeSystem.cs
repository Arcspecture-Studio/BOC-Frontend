﻿using UnityEngine;

public class OrderPageTakeProfitTypeSystem : MonoBehaviour
{
    [SerializeField] OrderPageComponent orderPageComponent;
    [SerializeField] SettingPageComponent settingPageComponent;

    const int takeOnReturnTrailingEnum = (int)TakeProfitTypeEnum.TRAILING;
    void Start()
    {
        ForOrderPageComponent();
        ForSettingPageComponent();
    }
    void ForOrderPageComponent()
    {
        if (orderPageComponent == null) return;
        orderPageComponent.riskRewardRatioObject.SetActive(orderPageComponent.lockForEdit && orderPageComponent.takeProfitTypeDropdown.value > (int)TakeProfitTypeEnum.NONE);
        orderPageComponent.takeProfitTrailingCallbackPercentageObject.SetActive(orderPageComponent.lockForEdit && orderPageComponent.takeProfitTypeDropdown.value == takeOnReturnTrailingEnum);
        orderPageComponent.takeProfitTypeDropdown.onValueChanged.AddListener(value =>
        {
            orderPageComponent.riskRewardRatioObject.SetActive(orderPageComponent.lockForEdit && value > (int)TakeProfitTypeEnum.NONE);
            orderPageComponent.takeProfitTrailingCallbackPercentageObject.SetActive(orderPageComponent.lockForEdit && value == takeOnReturnTrailingEnum);
        });
    }
    void ForSettingPageComponent()
    {
        if (settingPageComponent == null) return;
        settingPageComponent.riskRewardRatioObject.SetActive(settingPageComponent.takeProfitTypeDropdown.value != (int)TakeProfitTypeEnum.NONE);
        settingPageComponent.takeProfitTrailingCallbackPercentageObject.SetActive(settingPageComponent.takeProfitTypeDropdown.value == takeOnReturnTrailingEnum);
        settingPageComponent.takeProfitTypeDropdown.onValueChanged.AddListener(value =>
        {
            settingPageComponent.riskRewardRatioObject.SetActive(value != (int)TakeProfitTypeEnum.NONE);
            settingPageComponent.takeProfitTrailingCallbackPercentageObject.SetActive(value == takeOnReturnTrailingEnum);
        });
    }
}