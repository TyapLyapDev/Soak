using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class MobileButtonSpriteSwitcher : MonoBehaviour
{
    [SerializeField] private TouchInputReader _inputReader;
    [SerializeField] private Sprite _sitToStand;
    [SerializeField] private Sprite _standToSit;

    private Image _image;

    private void Awake() =>
        _image = GetComponent<Image>();

    private void OnEnable()
    {
        _inputReader.SneackPressed += OnSneacking;
        _inputReader.Rised += OnRised;
    }

    private void OnDisable()
    {
        _inputReader.SneackPressed -= OnSneacking;
        _inputReader.Rised -= OnRised;
    }

    private void OnSneacking() =>
        _image.sprite = _sitToStand;

    private void OnRised() =>
        _image.sprite = _standToSit;
}