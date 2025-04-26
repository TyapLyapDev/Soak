using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Saver : MonoBehaviour
{
    [SerializeField] private InputFieldView _playerName;
    [SerializeField] private InputFieldView _countBot;

    [SerializeField] private Toggle _characterWeightlessness;
    [SerializeField] private Toggle _gravigravitationalAnomalies;
    [SerializeField] private Toggle _spatialAnomalies;

    [SerializeField] private TMP_Dropdown _teamType;

    [SerializeField] private SliderHorizontalRotationSensitivity _sliderHorizontalRotationSensitivity;
    [SerializeField] private SliderVerticalRotationSensitivity _sliderVerticalRotationSensitivity;
    [SerializeField] private SliderVolumeGame _sliderVolumeGame;
    [SerializeField] private SliderVolumeMusic _sliderVolumeMusic;
    [SerializeField] private SliderLighting _sliderLighting;
    [SerializeField] private SliderAimSizer _sliderAimSizer;
    [SerializeField] private SliderAimColorRed _sliderAimColorRed;
    [SerializeField] private SliderAimColorGreen _sliderAimColorGreen;
    [SerializeField] private SliderAimColorBlue _sliderAimColorBlue;

    public event Action SavesChanged;

    public string PlayerName => PlayerPrefs.GetString(DataParams.SaveOptions.PlayerName, DataParams.Texts.PlayerName);

    public int CountBot => int.Parse(PlayerPrefs.GetString(DataParams.SaveOptions.CountBot, DataParams.Texts.CountBot));

    private void Start() =>
        Load();

    public void Save()
    {
        PlayerPrefs.SetInt(DataParams.SaveOptions.CharactertWeightlessness, Convert.ToInt32(_characterWeightlessness.isOn));
        PlayerPrefs.SetInt(DataParams.SaveOptions.GravigravitationalAnomalies, Convert.ToInt32(_gravigravitationalAnomalies.isOn));
        PlayerPrefs.SetInt(DataParams.SaveOptions.SpatialAnomalies, Convert.ToInt32(_spatialAnomalies.isOn));

        PlayerPrefs.SetInt(DataParams.SaveOptions.TeamType, _teamType.value);
        
        PlayerPrefs.SetString(DataParams.SaveOptions.PlayerName, _playerName.Text);
        PlayerPrefs.SetString(DataParams.SaveOptions.CountBot, _countBot.Text);
        
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

        DataParams.SaveOptions.IsCharacterWeightlessnessChecked = _characterWeightlessness.isOn;
        DataParams.SaveOptions.IsGravitationalAnomaliesChecked = _gravigravitationalAnomalies.isOn;
        DataParams.SaveOptions.IsSpatialAnomaliesChecked = _spatialAnomalies.isOn;

        DataParams.SaveOptions.TeamTypeIndex = _teamType.value;

        SavesChanged?.Invoke();
    }

    public void Load()
    {
        _characterWeightlessness.isOn = PlayerPrefs.GetInt(DataParams.SaveOptions.CharactertWeightlessness, DataParams.Texts.CharactertWeightlessness) != 0;
        _gravigravitationalAnomalies.isOn = PlayerPrefs.GetInt(DataParams.SaveOptions.GravigravitationalAnomalies, DataParams.Texts.GravigravitationalAnomalies) != 0;
        _spatialAnomalies.isOn = PlayerPrefs.GetInt(DataParams.SaveOptions.SpatialAnomalies, DataParams.Texts.SpatialAnomalies) != 0;

        _teamType.value = PlayerPrefs.GetInt(DataParams.SaveOptions.TeamType, DataParams.Texts.TeamType);
        
        _playerName.SetText(PlayerPrefs.GetString(DataParams.SaveOptions.PlayerName, DataParams.Texts.PlayerName));
        _countBot.SetText(PlayerPrefs.GetString(DataParams.SaveOptions.CountBot, DataParams.Texts.CountBot));
        
        _sliderHorizontalRotationSensitivity.SetValue(PlayerPrefs.GetFloat(DataParams.SaveOptions.HorizontalRotation, DataParams.SaveOptions.ValueHorizontalRotation));
        _sliderVerticalRotationSensitivity.SetValue(PlayerPrefs.GetFloat(DataParams.SaveOptions.VerticalRotation, DataParams.SaveOptions.ValueVerticalRotation));
        _sliderVolumeGame.SetValue(PlayerPrefs.GetFloat(DataParams.SaveOptions.VolumeGame, DataParams.SaveOptions.ValueVolumeGame));
        _sliderVolumeMusic.SetValue(PlayerPrefs.GetFloat(DataParams.SaveOptions.VolumeMusic, DataParams.SaveOptions.ValueVolumeMusic));
        _sliderLighting.SetValue(PlayerPrefs.GetFloat(DataParams.SaveOptions.Lighting, DataParams.SaveOptions.ValueLighting));
        _sliderAimSizer.SetValue(PlayerPrefs.GetFloat(DataParams.SaveOptions.AimScale, DataParams.SaveOptions.ValueAimScale));
        _sliderAimColorRed.SetValue(PlayerPrefs.GetFloat(DataParams.SaveOptions.AimColorR, DataParams.SaveOptions.ValueAimColorR));
        _sliderAimColorGreen.SetValue(PlayerPrefs.GetFloat(DataParams.SaveOptions.AimColorG, DataParams.SaveOptions.ValueAimColorG));
        _sliderAimColorBlue.SetValue(PlayerPrefs.GetFloat(DataParams.SaveOptions.AimColorB, DataParams.SaveOptions.ValueAimColorB));

        DataParams.SaveOptions.IsCharacterWeightlessnessChecked = _characterWeightlessness.isOn;
        DataParams.SaveOptions.IsGravitationalAnomaliesChecked = _gravigravitationalAnomalies.isOn;
        DataParams.SaveOptions.IsSpatialAnomaliesChecked = _spatialAnomalies.isOn;

        DataParams.SaveOptions.TeamTypeIndex = _teamType.value;
    }
}