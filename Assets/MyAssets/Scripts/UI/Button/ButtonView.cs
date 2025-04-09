using UnityEngine;
using UnityEngine.UI;

public class ButtonView
{
    private readonly Image _image;
    private readonly Color _initialColor;
    private readonly Color _selectedColor;    

    public ButtonView(Image image, Color selectedColor)
    {
        _image = image;
        _initialColor = _image.color;
        _selectedColor = selectedColor;
    }

    public void Select() =>
        _image.color = _selectedColor;

    public void Deselect() =>
        _image.color = _initialColor;
}