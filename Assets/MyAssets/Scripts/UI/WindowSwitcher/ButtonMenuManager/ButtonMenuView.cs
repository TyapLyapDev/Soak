using TMPro;
using UnityEngine;

public class ButtonMenuView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;

    private Color _initialColor;

    private void Start() => 
        _initialColor = _text.color;

    public void SetInitialColor() =>
        _text.color = _initialColor;

    public void SetColor(Color color) =>
        _text.color = color;
}