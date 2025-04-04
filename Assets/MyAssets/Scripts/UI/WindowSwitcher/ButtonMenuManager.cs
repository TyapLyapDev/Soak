using System;
using System.Collections.Generic;
using System.Linq;

public class ButtonMenuManager
{
    private ButtonMenu[] _buttons;

    public ButtonMenuManager(ButtonMenu[] buttons, Action<ButtonMenu> OnCursorEntered, Action<ButtonMenu> OnCursorExited, Action<ButtonMenu> OnPressed)
    {
        _buttons = buttons.Where(b => b != null).ToArray();
        Subscribe(OnCursorEntered, OnCursorExited, OnPressed);
        ShowButtons();
    }

    private void Subscribe(Action<ButtonMenu> OnCursorEntered, Action<ButtonMenu> OnCursorExited, Action<ButtonMenu> OnPressed)
    {
        foreach (ButtonMenu button in _buttons)
        {
            button.CursorEntered += OnCursorEntered;
            button.CursorExited += OnCursorExited;
            button.DownPressed += OnPressed;
        }
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