using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MurderLine : MonoBehaviour, IDeactivatable<MurderLine>
{
    private const float FontSizeDecreaseMultiplier = 0.1f;

    [SerializeField] private Image _image;
    [SerializeField] private TextMeshProUGUI _killer;
    [SerializeField] private TextMeshProUGUI _sacrifice;

    private RectTransform _killerRectTransform;
    private RectTransform _sacrificeRectTransform;

    private float _initialFontSize;
    private float _modifierFontSize;

    public event Action<MurderLine> Deactivated;

    private void Awake()
    {
        _killerRectTransform = _killer.GetComponent<RectTransform>();
        _sacrificeRectTransform = _sacrifice.GetComponent<RectTransform>();

        _initialFontSize = _killer.fontSize;
        _modifierFontSize = _initialFontSize - _initialFontSize * FontSizeDecreaseMultiplier;
    }

    public void SetSprite(Sprite sprite) =>
        _image.sprite = sprite;

    public void SetColor(Color killer, Color sacrifice)
    {
        _killer.color = killer;
        _sacrifice.color = sacrifice;
    }

    public void SetNames(string killer, string sacrifice)
    {
        _killer.text = killer;
        _sacrifice.text = sacrifice;

        LayoutRebuilder.ForceRebuildLayoutImmediate(_killerRectTransform);
        LayoutRebuilder.ForceRebuildLayoutImmediate(_sacrificeRectTransform);

        SetFontSize(_modifierFontSize);
    }

    public void ReturnInPool()
    {
        SetFontSize(_initialFontSize);

        Deactivated?.Invoke(this);
    }

    private void SetFontSize(float value)
    {
        _killer.fontSize = value;
        _sacrifice.fontSize -= value;
    }
}