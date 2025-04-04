using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicMenu : MonoBehaviour
{
    private AudioSource _source;

    private void Awake() =>
        _source = GetComponent<AudioSource>();

    public void Play() =>
        _source.Play();

    public void Stop() =>
        _source.Stop();
}