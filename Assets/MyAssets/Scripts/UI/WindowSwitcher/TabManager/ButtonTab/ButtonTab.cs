using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
[RequireComponent(typeof(Image))]
public abstract class ButtonTab : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;

    private Button _button;
    private Image _image;
    private Sprite _initialSprite;
    private Color _initialTextColor;
    private Vector3 InitialLocalScale;
    private readonly Vector3 _selectLocalScale = new(1, 1.1f, 1);
    private bool _isInitial;

    public event Action<ButtonTab> Clicked;

    private void Awake() =>
        Init();

    public void Init()
    {
        if (_isInitial)
            return;

        _isInitial = true;

        _button = GetComponent<Button>();
        _image = GetComponent<Image>();
        _initialSprite = _image.sprite;
        _initialTextColor = _text.color;
        InitialLocalScale = transform.localScale;
    }

    private void OnEnable() =>
        _button.onClick.AddListener(OnClick);

    private void OnDisable() =>
        _button.onClick.RemoveListener(OnClick);

    public void SetDefault()
    {
        _image.sprite = _initialSprite;
        _text.color = _initialTextColor;
        transform.localScale = InitialLocalScale;
    }

    public void Select(Sprite sprite, Color textColor)
    {
        _image.sprite = sprite;
        _text.color = textColor;
        transform.localScale = _selectLocalScale;
    }

    private void OnClick() =>
        Clicked?.Invoke(this);
}