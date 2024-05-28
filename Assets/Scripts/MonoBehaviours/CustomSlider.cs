using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.Events;

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
    [HideInInspector] public UnityEvent<string> onInputSubmit = new();
    int precision;

    void Start()
    {
        slider.onValueChanged.AddListener(OnSliderMove);
        input.onSubmit.AddListener(OnInputSubmit);
        EventTrigger.Entry pointerUpEvent = new() { eventID = EventTriggerType.PointerUp };
        pointerUpEvent.callback.AddListener(eventData => onSliderUp.Invoke());
        sliderTrigger.triggers.Add(pointerUpEvent);
    }

    public void SetRangeAndPrecision(float min, float max, int precision = 2)
    {
        slider.minValue = min;
        slider.maxValue = max;
        minText.text = min.ToString() + postfix;
        maxText.text = max.ToString() + postfix;
        this.precision = precision;
    }
    void SetValue(double value)
    {
        double parsedValue = Math.Min(Math.Max(value, slider.minValue), slider.maxValue);
        double roundedValue = Utils.RoundNDecimal(parsedValue, precision);
        slider.value = (float)roundedValue;
        input.text = roundedValue.ToString();
    }
    void OnSliderMove(float value)
    {
        SetValue(value);
        onSliderMove.Invoke(value);
    }
    void OnInputSubmit(string value)
    {
        SetValue(double.Parse(value));
        onInputSubmit.Invoke(value);
    }
}
