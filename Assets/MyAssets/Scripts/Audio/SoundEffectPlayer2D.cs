using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundEffectPlayer2D : MonoBehaviour
{
    [SerializeField] private AudioClip _hoverButton;
    [SerializeField] private AudioClip _clickButton;
    [SerializeField] private AudioClip _clickTabButton;
    [SerializeField] private AudioClip _sliderDownPressed;
    [SerializeField] private AudioClip _sliderUpPressed;
    [SerializeField] private AudioClip _counterTerroristWin;
    [SerializeField] private AudioClip _terroristWin;
    [SerializeField] private AudioClip _noTeamCharacterWin;
    [SerializeField] private AudioClip _voiceStartRound;

    private AudioSource _audioSource;

    private void Awake() =>
        _audioSource = GetComponent<AudioSource>();

    public void PlayHover() =>
        _audioSource.PlayOneShot(_hoverButton);

    public void PlayClickButton() =>
        _audioSource.PlayOneShot(_clickButton);
    
    public void PlayClickTabButton() =>
        _audioSource.PlayOneShot(_clickTabButton);

    public void PlaySliderDownPressed() =>
        _audioSource.PlayOneShot(_sliderDownPressed);

    public void PlaySliderUpPressed() =>
        _audioSource.PlayOneShot(_sliderUpPressed);
    
    public void PlayCounterTerroristWin() =>
        _audioSource.PlayOneShot(_counterTerroristWin);

    public void PlayTerroristWin() =>
        _audioSource.PlayOneShot(_terroristWin);

    public void PlayNoTeamCharacterWin() =>
        _audioSource.PlayOneShot(_noTeamCharacterWin);

    public void PlayStartRound() =>
        _audioSource.PlayOneShot(_voiceStartRound);
}