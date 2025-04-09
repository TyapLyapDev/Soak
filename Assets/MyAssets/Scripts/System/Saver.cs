using UnityEngine;

public class Saver : MonoBehaviour
{
    [SerializeField] private SliderChangeInformer _sliderHorizontalRotation;
    [SerializeField] private SliderChangeInformer _sliderVerticalRotation;
    [SerializeField] private SliderChangeInformer _sliderVolumeGame;
    [SerializeField] private SliderChangeInformer _sliderVolumeMusic;
    [SerializeField] private SliderChangeInformer _sliderLighting;
    [SerializeField] private AimColorPicker _aimColorPicker;

    private void Start() =>
        Load();

    public void Save()
    {
        PlayerPrefs.SetFloat(DataParams.SaveOptions.HorizontalRotation, _sliderHorizontalRotation.Value);
        PlayerPrefs.SetFloat(DataParams.SaveOptions.VerticalRotation, _sliderVerticalRotation.Value);
        PlayerPrefs.SetFloat(DataParams.SaveOptions.VolumeGame, _sliderVolumeGame.Value);
        PlayerPrefs.SetFloat(DataParams.SaveOptions.VolumeMusic, _sliderVolumeMusic.Value);
        PlayerPrefs.SetFloat(DataParams.SaveOptions.Lighting, _sliderLighting.Value);

        Color color = _aimColorPicker.Color;
        PlayerPrefs.SetFloat(DataParams.SaveOptions.AimColorR, color.r);
        PlayerPrefs.SetFloat(DataParams.SaveOptions.AimColorG, color.g);
        PlayerPrefs.SetFloat(DataParams.SaveOptions.AimColorB, color.b);
        PlayerPrefs.SetFloat(DataParams.SaveOptions.AimScale, _aimColorPicker.Scale);

        PlayerPrefs.Save();
    }

    public void Load()
    {
        _sliderHorizontalRotation.SetValue(PlayerPrefs.GetFloat(DataParams.SaveOptions.HorizontalRotation, DataParams.SaveOptions.ValueHorizontalRotation));
        _sliderVerticalRotation.SetValue(PlayerPrefs.GetFloat(DataParams.SaveOptions.VerticalRotation, DataParams.SaveOptions.ValueVerticalRotation));
        _sliderVolumeGame.SetValue(PlayerPrefs.GetFloat(DataParams.SaveOptions.VolumeGame, DataParams.SaveOptions.ValueVolumeGame));
        _sliderVolumeMusic.SetValue(PlayerPrefs.GetFloat(DataParams.SaveOptions.VolumeMusic, DataParams.SaveOptions.ValueVolumeMusic));
        _sliderLighting.SetValue(PlayerPrefs.GetFloat(DataParams.SaveOptions.Lighting, DataParams.SaveOptions.ValueLighting));

        float r = PlayerPrefs.GetFloat(DataParams.SaveOptions.AimColorR, DataParams.SaveOptions.ValueAimColorR);
        float g = PlayerPrefs.GetFloat(DataParams.SaveOptions.AimColorG, DataParams.SaveOptions.ValueAimColorG);
        float b = PlayerPrefs.GetFloat(DataParams.SaveOptions.AimColorB, DataParams.SaveOptions.ValueAimColorB);

        _aimColorPicker.SetColor(new(r, g, b));
        _aimColorPicker.SetScale(PlayerPrefs.GetFloat(DataParams.SaveOptions.AimScale, DataParams.SaveOptions.ValueAimScale));
    }
}