using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicMenu : MonoBehaviour
{
    private AudioSource _audioSource;

    private void Awake() =>
        _audioSource = GetComponent<AudioSource>();

    public void PlayMusic() =>
        _audioSource.Play();

    public void StopMusic() => 
        _audioSource.Stop();
}