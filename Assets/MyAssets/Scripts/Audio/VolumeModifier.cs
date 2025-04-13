using UnityEngine;
using UnityEngine.Audio;

public class VolumeModifier : MonoBehaviour
{
    private const float MinimumLevel = -80;
    private const float MaximumLevel = 20;

    private const string Music = nameof(Music);
    private const string Game = nameof(Game);

    [SerializeField] private AudioMixer _mixer;
    [SerializeField] private SliderVolumeMusic _sliderVolumeMusic;
    [SerializeField] private SliderVolumeGame _sliderVolumeGame;

    private float _minimumValueSlider;
    private float _maximumValueSlider;

    private void Start()
    {
        _minimumValueSlider = _sliderVolumeMusic.MinimumValue;
        _maximumValueSlider = _sliderVolumeMusic.MaximumValue;

        OnChangedMusicVolume(_sliderVolumeMusic.Value);
        OnChangedGameVolume(_sliderVolumeGame.Value);
    }

    private void OnEnable()
    {
        _sliderVolumeMusic.ValueChanged += OnChangedMusicVolume;
        _sliderVolumeGame.ValueChanged += OnChangedGameVolume;
    }

    private void OnDisable()
    {
        _sliderVolumeMusic.ValueChanged -= OnChangedMusicVolume;
        _sliderVolumeGame.ValueChanged -= OnChangedGameVolume;
    }

    private void OnChangedMusicVolume(float value) =>
        SetLevel(Music, value);

    private void OnChangedGameVolume(float value) =>
        SetLevel(Game, value);

    public void SetLevel(string group, float value)
    {
        float level = ConvertVolumeToLevel(NormalizeValue(value));
        _mixer.SetFloat(group, level);
    }

    private float NormalizeValue(float value) =>
        Mathf.InverseLerp(_minimumValueSlider, _maximumValueSlider, value);

    private float ConvertVolumeToLevel(float value) =>
        value == 0 ? MinimumLevel : Mathf.Log10(value) * MaximumLevel;
}