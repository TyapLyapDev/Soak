using UnityEngine;

public class Saver : MonoBehaviour
{
    private const string HorizontalRotation = nameof(HorizontalRotation);
    private const string VerticalRotation = nameof(VerticalRotation);
    private const string VolumeGame = nameof(VolumeGame);
    private const string VolumeMusic = nameof(VolumeMusic);
    private const string Lighting = nameof(Lighting);
    private const string AimColorR = nameof(AimColorR);
    private const string AimColorG = nameof(AimColorG);
    private const string AimColorB = nameof(AimColorB);
    private const string AimScale = nameof(AimScale);

    private const float ValueHorizontalRotation = 5f;
    private const float ValueVerticalRotation = 5f;
    private const float ValueVolumeGame = 0.9f;
    private const float ValueVolumeMusic = 0.8f;
    private const float ValueLighting = 40f;
    private const float ValueAimColorR = 0f;
    private const float ValueAimColorG = 1f;
    private const float ValueAimColorB = 0f;
    private const float ValueAimScale = 1f;

    [SerializeField] private WindowSwitcher _windowSwitcher;
    [SerializeField] private SliderChangeInformer _sliderHorizontalRotation;
    [SerializeField] private SliderChangeInformer _sliderVerticalRotation;
    [SerializeField] private SliderChangeInformer _sliderVolumeGame;
    [SerializeField] private SliderChangeInformer _sliderVolumeMusic;
    [SerializeField] private SliderChangeInformer _sliderLighting;
    [SerializeField] private AimColorPicker _aimColorPicker;

    private void Start() =>
        Load();

    private void OnEnable()
    {
        _windowSwitcher.ChangesApplied += Save;
        _windowSwitcher.ChangesCanceled += Load;
    }

    private void OnDisable()
    {
        _windowSwitcher.ChangesApplied -= Save;
        _windowSwitcher.ChangesApplied -= Load;
    }

    private void Save()
    {
        PlayerPrefs.SetFloat(HorizontalRotation, _sliderHorizontalRotation.Value);
        PlayerPrefs.SetFloat(VerticalRotation, _sliderVerticalRotation.Value);
        PlayerPrefs.SetFloat(VolumeGame, _sliderVolumeGame.Value);
        PlayerPrefs.SetFloat(VolumeMusic, _sliderVolumeMusic.Value);
        PlayerPrefs.SetFloat(Lighting, _sliderLighting.Value);

        Color color = _aimColorPicker.Color;
        PlayerPrefs.SetFloat(AimColorR, color.r);
        PlayerPrefs.SetFloat(AimColorG, color.g);
        PlayerPrefs.SetFloat(AimColorB, color.b);

        PlayerPrefs.SetFloat(AimScale, _aimColorPicker.Scale);

        PlayerPrefs.Save();
    }

    private void Load()
    {
        _sliderHorizontalRotation.SetValue(PlayerPrefs.GetFloat(HorizontalRotation, ValueHorizontalRotation));
        _sliderVerticalRotation.SetValue(PlayerPrefs.GetFloat(VerticalRotation, ValueVerticalRotation));
        _sliderVolumeGame.SetValue(PlayerPrefs.GetFloat(VolumeGame, ValueVolumeGame));
        _sliderVolumeMusic.SetValue(PlayerPrefs.GetFloat(VolumeMusic, ValueVolumeMusic));
        _sliderLighting.SetValue(PlayerPrefs.GetFloat(Lighting, ValueLighting));

        float r = PlayerPrefs.GetFloat(AimColorR, ValueAimColorR);
        float g = PlayerPrefs.GetFloat(AimColorG, ValueAimColorG);
        float b = PlayerPrefs.GetFloat(AimColorB, ValueAimColorB);

        _aimColorPicker.SetColor(new(r, g, b));
        _aimColorPicker.SetScale(PlayerPrefs.GetFloat(AimScale, ValueAimScale));
    }
}