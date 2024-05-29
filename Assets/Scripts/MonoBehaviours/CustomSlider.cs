using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.Events;
using WebSocketSharp;

public class CustomSlider : MonoBehaviour
{
    [Header("Reference")]
    public Slider slider;
    public EventTrigger sliderTrigger;
    public TMP_Text minText;
    public TMP_Text maxText;
    public TMP_InputField input;

    [Header("Config")]
    public string postfix;

    [HideInInspector] public UnityEvent<float> onSliderMove = new();
    [HideInInspector] public UnityEvent onSliderUp = new();
    [HideInInspector] public UnityEvent<float> onInputSubmit = new();
    int precision = 2;

    void Start()
    {
        slider.onValueChanged.AddListener(OnSliderMove);
        input.onSubmit.AddListener(OnInputSubmit);
        EventTrigger.Entry pointerUpEvent = new() { eventID = EventTriggerType.PointerUp };
        pointerUpEvent.callback.AddListener(OnSliderUp);
        sliderTrigger.triggers.Add(pointerUpEvent);
    }

    public void SetRangeAndPrecision(float min, float max, float defaultValue = float.NaN, int precision = 2)
    {
        slider.minValue = min;
        slider.maxValue = max;
        minText.text = min.ToString() + postfix;
        maxText.text = max.ToString() + postfix;
        this.precision = precision;

        SetValue(float.IsNaN(defaultValue) ? slider.value : defaultValue);
    }
    public void SetValue(float value)
    {
        float parsedValue = Mathf.Min(Mathf.Max(value, slider.minValue), slider.maxValue);
        float roundedValue = Utils.RoundNDecimal(parsedValue, precision);
        slider.value = roundedValue;
        input.text = roundedValue.ToString();
    }
    void OnSliderMove(float value)
    {
        SetValue(value);
        onSliderMove.Invoke(slider.value);
    }
    void OnSliderUp(BaseEventData eventData)
    {
        onSliderUp.Invoke();
    }
    void OnInputSubmit(string value)
    {
        if (value.IsNullOrEmpty()) SetValue(slider.value);
        else SetValue(float.Parse(value));
        onInputSubmit.Invoke(slider.value);
    }
}
