using UnityEngine;
using UnityEngine.Audio;

public class VolumeAccepter : MonoBehaviour
{
    private const string Game = nameof(Game);
    private const string Music = nameof(Music);

    [SerializeField] private AudioMixer _mixer;
    [SerializeField] private SliderChangeInformer _music;
    [SerializeField] private SliderChangeInformer _game;

    private ModifierVolume _modifier;

    private void Awake() =>
        _modifier = new ModifierVolume(_mixer);

    private void Start()
    {
        OnChangedMusic(_music.Value);
        OnChangedGame(_game.Value);
    }

    private void OnEnable()
    {
        _music.ValueChanged += OnChangedMusic;
        _game.ValueChanged += OnChangedGame;
    }

    private void OnDisable()
    {
        _music.ValueChanged -= OnChangedMusic;
        _game.ValueChanged -= OnChangedGame;
    }

    private void OnChangedMusic(float value) =>
        _modifier.SetLevel(Music, value);

    private void OnChangedGame(float value) =>
        _modifier.SetLevel(Game, value);
}