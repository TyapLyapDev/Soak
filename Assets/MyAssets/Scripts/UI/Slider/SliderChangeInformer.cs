using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public abstract class SliderChangeInformer : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Slider _slider;
    private SliderTextValue _textValue;

    public event Action<float> ValueChanged;
    public event Action<Type> DownPressed;
    public event Action<Type> UpPressed;

    public float Value => _slider.value;

    public float MinimumValue => _slider.minValue;

    public float MaximumValue => _slider.maxValue;

    public void Init()
    {
        _slider = GetComponent<Slider>();
        _textValue = GetComponentInChildren<SliderTextValue>(true);
        _textValue.Init(MaximumValue);
    }

    private void Start() =>
        SetText(Value);

    private void OnEnable() =>
        _slider.onValueChanged.AddListener(OnValueChanged);

    private void OnDisable() =>
        _slider.onValueChanged.RemoveListener(OnValueChanged);

    public void SetValue(float value)
    {
        _slider.value = value;
        OnValueChanged(value);
    }

    private void OnValueChanged(float value)
    {
        SetText(value);
        ValueChanged?.Invoke(value);
    }

    private void SetText(float value) =>
        _textValue.SetValue(value);

    public void OnPointerDown(PointerEventData eventData) =>
        DownPressed?.Invoke(GetType());

    public void OnPointerUp(PointerEventData eventData) =>
        UpPressed?.Invoke(GetType());
}