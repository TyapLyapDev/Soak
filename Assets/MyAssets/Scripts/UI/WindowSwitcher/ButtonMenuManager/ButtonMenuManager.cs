using System;
using UnityEngine;

public class ButtonMenuManager
{
    private readonly ButtonMenu[] _buttons;
    private readonly Color _buttonSelectColor;
    private readonly SoundEffectPlayer2D _sfx;

    public event Action<ButtonMenu> ButtonPressed;

    public ButtonMenuManager(Transform parent, Color buttonSelectColor, SoundEffectPlayer2D sfx)
    {
        _buttons = parent.GetComponentsInChildren<ButtonMenu>(true);
        _buttonSelectColor = buttonSelectColor;
        _sfx = sfx;
        ShowButtons();
    }

    public void Subscribe()
    {
        foreach (ButtonMenu button in _buttons)
        {
            button.CursorEntered += OnCursorEntered;
            button.CursorExited += OnCursorExited;
            button.DownPressed += OnButtonPressed;
        }
    }

    public void Unsubscribe()
    {
        foreach (ButtonMenu button in _buttons)
        {
            button.CursorEntered -= OnCursorEntered;
            button.CursorExited -= OnCursorExited;
            button.DownPressed -= OnButtonPressed;
        }
    }

    private void OnCursorEntered(ButtonMenu button)
    {
        button.SetColor(_buttonSelectColor);
        _sfx.PlayHover();
    }

    private void OnCursorExited(ButtonMenu button)
    {
        button.SetInitialColor();
        _sfx.PlayHover();
    }

    private void OnButtonPressed(ButtonMenu button)
    {
        button.SetInitialColor();
        ButtonPressed?.Invoke(button);
    }

    public void HideButtons()
    {
        foreach (ButtonMenu button in _buttons)
            button.Disable();
    }

    public void ShowButtons()
    {
        foreach (ButtonMenu button in _buttons)
            button.Enable();
    }
}