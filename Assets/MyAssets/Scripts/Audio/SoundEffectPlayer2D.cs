using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundEffectPlayer2D : MonoBehaviour
{
    [SerializeField] private WindowSwitcher _windowSwitcher;
    [SerializeField] private AudioClip _hoverButton;
    [SerializeField] private AudioClip _clickButton;

    private AudioSource _audioSource;

    private void Awake() =>
        _audioSource = GetComponent<AudioSource>();

    private void OnEnable()
    {
        _windowSwitcher.ButtonMenuHovered += PlayHover;
        _windowSwitcher.ButtonMenuPressed += PlayClickButton;
    }

    private void PlayHover() =>
        _audioSource.PlayOneShot(_hoverButton);

    private void PlayClickButton() =>
        _audioSource.PlayOneShot(_clickButton);
}