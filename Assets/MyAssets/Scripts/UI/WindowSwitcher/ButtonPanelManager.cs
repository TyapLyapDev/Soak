using System;
using System.Linq;

public class ButtonPanelManager
{
    private ButtonPanel[] _buttons;

    public ButtonPanelManager(ButtonPanel[] buttons, Action<ButtonPanel> OnClicked)
    {
        _buttons = buttons.Where(b => b != null).ToArray();
        Subscribe(OnClicked);
    }

    private void Subscribe(Action<ButtonPanel> OnClicked)
    {
        foreach (ButtonPanel button in _buttons)
            button.Clicked += OnClicked;
    }
}