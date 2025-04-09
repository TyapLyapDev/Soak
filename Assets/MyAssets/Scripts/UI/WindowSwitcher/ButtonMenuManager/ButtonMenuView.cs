using TMPro;
using UnityEngine;

public class ButtonMenuView
{
    private TextMeshProUGUI _text;
    private Color _initialColor;

    public ButtonMenuView(TextMeshProUGUI text)
    {
        _text = text;
        _initialColor = _text.color;
    }

    public void SetInitialColor() =>
        _text.color = _initialColor;

    public void SetColor(Color color) =>
        _text.color = color;
}