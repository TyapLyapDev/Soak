using UnityEngine;

public class Saver : MonoBehaviour
{
    [SerializeField] private SliderHorizontalRotationSensitivity _sliderHorizontalRotationSensitivity;
    [SerializeField] private SliderVerticalRotationSensitivity _sliderVerticalRotationSensitivity;
    [SerializeField] private SliderVolumeGame _sliderVolumeGame;
    [SerializeField] private SliderVolumeMusic _sliderVolumeMusic;
    [SerializeField] private SliderLighting _sliderLighting;
    [SerializeField] private SliderAimSizer _sliderAimSizer;
    [SerializeField] private SliderAimColorRed _sliderAimColorRed;
    [SerializeField] private SliderAimColorGreen _sliderAimColorGreen;
    [SerializeField] private SliderAimColorBlue _sliderAimColorBlue;

    private void Start() =>
        Load();

    public void Save()
    {
        PlayerPrefs.SetFloat(DataParams.SaveOptions.HorizontalRotation, _sliderHorizontalRotationSensitivity.Value);
        PlayerPrefs.SetFloat(DataParams.SaveOptions.VerticalRotation, _sliderVerticalRotationSensitivity.Value);
        PlayerPrefs.SetFloat(DataParams.SaveOptions.VolumeGame, _sliderVolumeGame.Value);
        PlayerPrefs.SetFloat(DataParams.SaveOptions.VolumeMusic, _sliderVolumeMusic.Value);
        PlayerPrefs.SetFloat(DataParams.SaveOptions.Lighting, _sliderLighting.Value);
        PlayerPrefs.SetFloat(DataParams.SaveOptions.AimScale, _sliderAimSizer.Value);
        PlayerPrefs.SetFloat(DataParams.SaveOptions.AimColorR, _sliderAimColorRed.Value);
        PlayerPrefs.SetFloat(DataParams.SaveOptions.AimColorG, _sliderAimColorGreen.Value);
        PlayerPrefs.SetFloat(DataParams.SaveOptions.AimColorB, _sliderAimColorBlue.Value);

        PlayerPrefs.Save();
    }

    public void Load()
    {
        _sliderHorizontalRotationSensitivity.SetValue(PlayerPrefs.GetFloat(DataParams.SaveOptions.HorizontalRotation, DataParams.SaveOptions.ValueHorizontalRotation));
        _sliderVerticalRotationSensitivity.SetValue(PlayerPrefs.GetFloat(DataParams.SaveOptions.VerticalRotation, DataParams.SaveOptions.ValueVerticalRotation));
        _sliderVolumeGame.SetValue(PlayerPrefs.GetFloat(DataParams.SaveOptions.VolumeGame, DataParams.SaveOptions.ValueVolumeGame));
        _sliderVolumeMusic.SetValue(PlayerPrefs.GetFloat(DataParams.SaveOptions.VolumeMusic, DataParams.SaveOptions.ValueVolumeMusic));
        _sliderLighting.SetValue(PlayerPrefs.GetFloat(DataParams.SaveOptions.Lighting, DataParams.SaveOptions.ValueLighting));
        _sliderAimSizer.SetValue(PlayerPrefs.GetFloat(DataParams.SaveOptions.AimScale, DataParams.SaveOptions.ValueAimScale));
        _sliderAimColorRed.SetValue(PlayerPrefs.GetFloat(DataParams.SaveOptions.AimColorR, DataParams.SaveOptions.ValueAimColorR));
        _sliderAimColorGreen.SetValue(PlayerPrefs.GetFloat(DataParams.SaveOptions.AimColorG, DataParams.SaveOptions.ValueAimColorG));
        _sliderAimColorBlue.SetValue(PlayerPrefs.GetFloat(DataParams.SaveOptions.AimColorB, DataParams.SaveOptions.ValueAimColorB));
    }
}