using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundEffectPlayer2D : MonoBehaviour
{
    [SerializeField] private AudioClip _hoverButton;
    [SerializeField] private AudioClip _clickButton;

    private AudioSource _audioSource;

    private void Awake() =>
        _audioSource = GetComponent<AudioSource>();

    public void PlayHover() =>
        _audioSource.PlayOneShot(_hoverButton);

    public void PlayClickButton() =>
        _audioSource.PlayOneShot(_clickButton);
}