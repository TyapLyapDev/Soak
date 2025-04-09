using System;
using UnityEngine;

public class ButtonPanelManager
{
    private readonly ButtonPanel[] _buttons;

    public event Action<ButtonPanel> ButtonClicked;

    public ButtonPanelManager(Transform parent)
    {
        _buttons = parent.GetComponentsInChildren<ButtonPanel>(true);
    }

    public void Subscribe()
    {
        foreach (ButtonPanel button in _buttons)
            button.Clicked += OnClick;
    }

    public void Unsubscribe()
    {
        foreach (ButtonPanel button in _buttons)
            button.Clicked += OnClick;
    }

    private void OnClick(ButtonPanel button) =>
        ButtonClicked?.Invoke(button);
}