using UnityEngine;
using UnityEngine.UI;

public class SpongeShower : MonoBehaviour
{
    private const float MaximumHealth = 100f;
    private const float TransparentMaxValue = 0f;
    private const float TransparentMinValue = 0.15f;

    [SerializeField] private Player _player;
    [SerializeField] private Image _image;

    private void OnEnable()
    {
        _player.HealthChanged += OnHealthChanged;
        _player.Died += OnDied;
    }

    private void OnDisable()
    {
        _player.HealthChanged -= OnHealthChanged;
        _player.Died -= OnDied;
    }

    private void OnHealthChanged(Character character)
    {
        float normalizedHealth = character.Health / MaximumHealth;
        float alpha = Mathf.Lerp(TransparentMinValue, TransparentMaxValue, normalizedHealth);

        Color color = _image.color;
        color.a = alpha;
        _image.color = color;
    }

    private void OnDied(Character _)
    {
        Color color = _image.color;
        color.a = TransparentMaxValue;
        _image.color = color;
    }
}