using UnityEngine;

public class AudioClipList : MonoBehaviour
{
    [SerializeField] private AudioClip _step;

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
}