using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundEffectPlayer2D : MonoBehaviour
{
    [SerializeField] private AudioClip _hoverButton;
    [SerializeField] private AudioClip _clickButton;
    [SerializeField] private AudioClip _clickTabButton;
    [SerializeField] private AudioClip _sliderDownPressed;
    [SerializeField] private AudioClip _sliderUpPressed;

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
}