using System;
using UnityEngine;

public class AimColorPicker : MonoBehaviour
{
    [SerializeField] private AimMarker _aim;
    [SerializeField] private AimMarker _aimPreview;
    [SerializeField] private SliderAimSizer _sliderScalingAim;
    [SerializeField] private SliderAimColorRed _redSlider;
    [SerializeField] private SliderAimColorGreen _greenSlider;
    [SerializeField] private SliderAimColorBlue _blueSlider;

    public float Scale => _sliderScalingAim.Value;

    private void Start()
    {
        _sliderScalingAim.ValueChanged += OnChanged;
        _redSlider.ValueChanged += OnChanged;
        _greenSlider.ValueChanged += OnChanged;
        _blueSlider.ValueChanged += OnChanged;

        OnChanged(0);
    }

    private void OnDestroy()
    {
        _sliderScalingAim.ValueChanged -= OnChanged;
        _redSlider.ValueChanged -= OnChanged;
        _greenSlider.ValueChanged -= OnChanged;
        _blueSlider.ValueChanged -= OnChanged;
    }

    private void OnChanged(float _)
    {
        Color color = new(_redSlider.Value, _greenSlider.Value, _blueSlider.Value, 1);

        _aimPreview.SetColor(color);
        _aimPreview.SetLocalScale(_sliderScalingAim.Value);

        if (_aim == null)
            return;

        _aim.SetColor(color);
        _aim.SetLocalScale(_sliderScalingAim.Value);
    }
}