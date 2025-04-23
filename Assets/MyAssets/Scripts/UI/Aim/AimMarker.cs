using UnityEngine;
using UnityEngine.UI;

public class AimMarker : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Image _image;
    
    public void SetColor(Color color) =>
        _image.color = color;
    
    public void SetLocalScale(float value) =>
        transform.localScale = Vector3.one * value;

    private void OnEnable()
    {
        if (_player == null)
            return;

        _player.Died += OnDied;
        _player.Revived += OnRevived;
    }

    private void OnDisable()
    {
        if (_player == null)
            return;

        _player.Died -= OnDied;
        _player.Revived -= OnRevived;
    }

    private void OnDied(Character _) =>
        _image.enabled = false;
    
    private void OnRevived(Character _) =>
        _image.enabled = true;
}