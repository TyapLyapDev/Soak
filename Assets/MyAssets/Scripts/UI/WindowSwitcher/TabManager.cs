using System.Linq;
using UnityEngine;

public class TabManager
{
    private readonly TabContainer[] _tabsContainer;
    private readonly ButtonTab[] _buttons;
    private readonly Sprite _selectedSprite;
    private readonly Color _selectTextColor = Color.yellow;

    public TabManager(ButtonTab[] buttons, TabContainer[] tabsContainer, Sprite selectedSprite)
    {
        _buttons = buttons.Where(b => b != null).ToArray();
        _tabsContainer = tabsContainer;
        _selectedSprite = selectedSprite;
        Init();
        Subscribe();
        Select<ButtonTabRotation>();
    }

    private void Init()
    {
        foreach (ButtonTab button in _buttons)
            button.Init();
    }

    private void Subscribe()
    {
        foreach (ButtonTab button in _buttons)
            button.Clicked += OnClickButton;
    }

    private void OnClickButton(ButtonTab button)
    {
        DeselectButtons();
        SelectButton(button);
    }

    private void Select<T>() where T : ButtonTab
    {
        DeselectButtons();

        foreach (ButtonTab button in _buttons)
            if(button is T)
                SelectButton(button);
    }

    private void SelectButton(ButtonTab button)
    {
        button.Select(_selectedSprite, _selectTextColor);        

        switch (button)
        {
            case ButtonTabRotation:
                ShowContainer<ContainerRotation>();
                break;

            case ButtonTabVolume:
                ShowContainer<ContainerVolume>();
                break;

            case ButtonTabLighting:
                ShowContainer<ContainerLighting>();
                break;

            case ButtonTabAim:
                ShowContainer<ContainerAim>();
                break;
        }
    }

    private void DeselectButtons()
    {
        foreach (ButtonTab button in _buttons)
            button.SetDefault();
    }

    private void ShowContainer<T>() where T : TabContainer
    {
        HideContainers();

        foreach (TabContainer container in _tabsContainer)
            if (container is T)
                container.Show();
    }

    private void HideContainers()
    {
        foreach (TabContainer container in _tabsContainer)
            container.Hide();
    }
}