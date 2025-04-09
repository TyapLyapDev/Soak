using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class SliderChangeInformer : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private TextMeshProUGUI _text;

    public event Action<float> ValueChanged;

    public float Value => _slider.value;

    public float MinimumValue => _slider.minValue;

    public float MaximumValue => _slider.maxValue;

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
        _text.text = Value.ToString("F1");
}