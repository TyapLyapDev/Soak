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

    public void PlayJumpDown() =>
        _audioSource.PlayOneShot(AudioClipList.Instance.JumpDown);

    public void PlayDead() =>
        _audioSource.PlayOneShot(AudioClipList.Instance.Dead);

    public void PlayAdded() =>
        _audioSource.PlayOneShot(AudioClipList.Instance.Added);

    public void StartPlayWaterJet()
    {
        _audioSource.clip = AudioClipList.Instance.WaterJet;
        _audioSource.loop = true;
        _audioSource.Play();
    }

    public void StopPlayWaterJet()
    {
        _audioSource.Stop();
    }
}