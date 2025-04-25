using UnityEngine;

public class AudioClipList : MonoBehaviour
{
    [SerializeField] private AudioClip _step;
    [SerializeField] private AudioClip _dead;
    [SerializeField] private AudioClip _jumpDown;
    [SerializeField] private AudioClip _characterAdded;
    [SerializeField] private AudioClip _waterJet;

    public static AudioClipList Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public AudioClip Step => _step;

    public AudioClip Dead => _dead;
    
    public AudioClip JumpDown => _jumpDown;

    public AudioClip Added => _characterAdded;

    public AudioClip WaterJet => _waterJet;
}