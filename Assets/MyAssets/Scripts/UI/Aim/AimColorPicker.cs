using UnityEngine;

public class AimColorPicker : MonoBehaviour
{
    [SerializeField] private AimMarker _aim;
    [SerializeField] private AimMarker _aimPreview;
    [SerializeField] private SliderChangeInformer _redSlider;
    [SerializeField] private SliderChangeInformer _greenSlider;
    [SerializeField] private SliderChangeInformer _blueSlider;
    [SerializeField] private SliderChangeInformer _sliderScalingAim;

    public Color Color => new(_redSlider.Value, _greenSlider.Value, _blueSlider.Value, 1);

    public float Scale => _sliderScalingAim.Value;

    private void Start() =>
        UpdateInfo();

    private void OnEnable()
    {
        _redSlider.ValueChanged += (float _) => UpdateInfo();
        _greenSlider.ValueChanged += (float _) => UpdateInfo();
        _blueSlider.ValueChanged += (float _) => UpdateInfo();
        _sliderScalingAim.ValueChanged += (float _) => UpdateInfo();
    }

    private void OnDisable()
    {
        _redSlider.ValueChanged -= (float _) => UpdateInfo();
        _greenSlider.ValueChanged -= (float _) => UpdateInfo();
        _blueSlider.ValueChanged -= (float _) => UpdateInfo();
        _sliderScalingAim.ValueChanged -= (float _) => UpdateInfo();
    }

    public void SetColor(Color color)
    {
        _redSlider.SetValue(color.r);
        _greenSlider.SetValue(color.g);
        _blueSlider.SetValue(color.b);

        UpdateInfo();
    }

    public void SetScale(float value)
    {
        _sliderScalingAim.SetValue(value);

        UpdateInfo();
    }

    private void UpdateInfo()
    {
        Color color = Color;

        _aimPreview.SetColor(color);
        _aimPreview.SetLocalScale(_sliderScalingAim.Value);

        if (_aim == null)
            return;

        _aim.SetColor(color);
        _aim.SetLocalScale(_sliderScalingAim.Value);
    }
}