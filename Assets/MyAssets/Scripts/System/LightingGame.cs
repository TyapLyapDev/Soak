using UnityEngine;

[RequireComponent(typeof(Light))]
public class LightingGame : MonoBehaviour
{
    private const float MaximumIntensity = 5f;

    [SerializeField] private SliderChangeInformer _lightSlider;

    private Light _directionalLight;
    private float _minimumValueSlider;
    private float _maximumValueSlider;

    private void Awake() =>
        _directionalLight = GetComponent<Light>();

    private void Start()
    {
        _minimumValueSlider = _lightSlider.MinimumValue;
        _maximumValueSlider = _lightSlider.MaximumValue;

        OnChanged(_lightSlider.Value);
    }

    private void OnEnable() =>
        _lightSlider.ValueChanged += OnChanged;

    private void OnDisable() =>
        _lightSlider.ValueChanged -= OnChanged;

    private void OnChanged(float value)
    {
        float normalizedValue = Mathf.InverseLerp(_minimumValueSlider, _maximumValueSlider, value) * MaximumIntensity;
        RenderSettings.ambientIntensity = normalizedValue;
        _directionalLight.intensity = normalizedValue * 0.5f;
    }
}