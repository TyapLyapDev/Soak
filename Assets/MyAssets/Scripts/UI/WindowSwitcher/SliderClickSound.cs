using System;
using UnityEngine;

public class SliderClickSound : MonoBehaviour
{
    [SerializeField] private SoundEffectPlayer2D _sfx;
    [SerializeField] private MusicMenu _musicMenu;
    [SerializeField] private bool _isGameScene;

    private SliderChangeInformer[] _sliders;

    private void Awake()
    {
        _sliders = GetComponentsInChildren<SliderChangeInformer>(true);

        foreach (SliderChangeInformer slider in _sliders)
            slider.Init();
    }

    private void OnEnable() =>
        Subscribe();

    private void OnDisable() =>
        Unsubscribe();

    private void Subscribe()
    {
        foreach (SliderChangeInformer slider in _sliders)
        {
            slider.DownPressed += OnSliderDownPresssed;
            slider.UpPressed += OnSliderUpPresssed;
        }
    }

    private void Unsubscribe()
    {
        foreach (SliderChangeInformer slider in _sliders)
        {
            slider.DownPressed -= OnSliderDownPresssed;
            slider.UpPressed -= OnSliderUpPresssed;
        }
    }

    private void OnSliderDownPresssed(Type type)
    {
        if (type == typeof(SliderVolumeMusic))
            PlayMusic();
        else
            _sfx.PlaySliderDownPressed();
    }

    private void OnSliderUpPresssed(Type type)
    {
        if (type == typeof(SliderVolumeMusic))
            StopMusic();
        else
            _sfx.PlaySliderUpPressed();
    }

    private void PlayMusic()
    {
        if (_isGameScene)
            _musicMenu.PlayMusic();
    }

    private void StopMusic()
    {
        if (_isGameScene)
            _musicMenu.StopMusic();
    }
}