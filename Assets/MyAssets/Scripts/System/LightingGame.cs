using System;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class LightingGame : MonoBehaviour
{
    private const float MaximumIntensity = 5f;

    [SerializeField] private SliderLighting _sliderLighting;

    private Light _directionalLight;
    private float _minimumValueSlider;
    private float _maximumValueSlider;

    private void Awake() =>
        _directionalLight = GetComponent<Light>();

    private void Start()
    {
        _minimumValueSlider = _sliderLighting.MinimumValue;
        _maximumValueSlider = _sliderLighting.MaximumValue;
        OnChanged(_sliderLighting.Value);
    }

    private void OnEnable() =>
        _sliderLighting.ValueChanged += OnChanged;

    private void OnDisable() =>
        _sliderLighting.ValueChanged -= OnChanged;

    private void OnChanged(float value)
    {
        float normalizedValue = Mathf.InverseLerp(_minimumValueSlider, _maximumValueSlider, value) * MaximumIntensity;
        RenderSettings.ambientIntensity = normalizedValue;
        _directionalLight.intensity = normalizedValue * 0.5f;
    }
}