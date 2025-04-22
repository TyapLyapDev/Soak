using UnityEngine;

public class CharacterAudio
{
    private readonly AudioSource _audioSource;

    public CharacterAudio(Transform character)
    {
        _audioSource = character.GetComponent<AudioSource>();
    }

    public void PlayStep() =>
        _audioSource.PlayOneShot(AudioClipList.Instance.Step);
}