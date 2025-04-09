using System;

public class WindowSwitchingSubscriber
{
    private readonly ButtonMenuManager _buttonMenuManager;
    private readonly ButtonPanelManager _buttonPanelManager;
    private readonly TabManager _tabManager;

    public WindowSwitchingSubscriber(ButtonMenuManager buttonMenuManager, ButtonPanelManager buttonPanelManager, TabManager tabManager)
    {
        _buttonMenuManager = buttonMenuManager;
        _buttonPanelManager = buttonPanelManager;
        _tabManager = tabManager;
    }

    public void Subscribe(Action<StateElement> buttonPressed)
    {
        _buttonMenuManager.Subscribe();
        _buttonPanelManager.Subscribe();
        _tabManager.Subscribe();

        _buttonMenuManager.ButtonPressed += buttonPressed;
        _buttonPanelManager.ButtonClicked += buttonPressed;
    }

    public void Unsubscribe(Action<StateElement> buttonPressed)
    {
        _buttonMenuManager.Unsubscribe();
        _buttonPanelManager.Unsubscribe();
        _tabManager.Unsubscribe();

        _buttonMenuManager.ButtonPressed -= buttonPressed;
        _buttonPanelManager.ButtonClicked -= buttonPressed;
    }
}