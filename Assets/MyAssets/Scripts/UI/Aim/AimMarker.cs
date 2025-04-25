using UnityEngine;
using UnityEngine.UI;

public class AimMarker : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private SliderAimSizer _sliderScalingAim;
    [SerializeField] private SliderAimColorRed _redSlider;
    [SerializeField] private SliderAimColorGreen _greenSlider;
    [SerializeField] private SliderAimColorBlue _blueSlider;

    private void OnEnable()
    {
        OnScaleChanged();
        OnColorChanged();

        _sliderScalingAim.ValueChanged += OnScaleChanged;
        _redSlider.ValueChanged += OnColorChanged;
        _greenSlider.ValueChanged += OnColorChanged;
        _blueSlider.ValueChanged += OnColorChanged;
    }

    private void OnDisable()
    {
        _sliderScalingAim.ValueChanged -= OnScaleChanged;
        _redSlider.ValueChanged -= OnColorChanged;
        _greenSlider.ValueChanged -= OnColorChanged;
        _blueSlider.ValueChanged -= OnColorChanged;
    }

    private void OnScaleChanged() =>
        _image.color = new(_redSlider.Value, _greenSlider.Value, _blueSlider.Value, 1);

    private void OnColorChanged() =>
        transform.localScale = Vector3.one * _sliderScalingAim.Value;
}