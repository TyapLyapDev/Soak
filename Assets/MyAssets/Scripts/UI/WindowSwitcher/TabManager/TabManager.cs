﻿using System;
using UnityEngine;

public class TabManager
{
    private readonly SoundEffectPlayer2D _sfx;
    private readonly TabContainer[] _tabsContainer;
    private readonly ButtonTab[] _buttons;
    private readonly Sprite _selectedSprite;
    private readonly Color _selectTextColor = Color.yellow;

    private Type _currentButton;

    public TabManager(Transform parent, SoundEffectPlayer2D sfx, Sprite selectedSprite)
    {
        _sfx = sfx;
        _buttons = parent.GetComponentsInChildren<ButtonTab>(true);
        _tabsContainer = parent.GetComponentsInChildren<TabContainer>(true);
        _selectedSprite = selectedSprite;
        InitButtons();

        _currentButton = typeof(ButtonTabRotation);
        Select<ButtonTabRotation>();
    }

    public void Subscribe()
    {
        foreach (ButtonTab button in _buttons)
            button.Clicked += OnClickButton;
    }

    public void Unsubscribe()
    {
        foreach (ButtonTab button in _buttons)
            button.Clicked += OnClickButton;
    }

    private void InitButtons()
    {
        foreach (ButtonTab button in _buttons)
            button.Init();
    }

    private void OnClickButton(ButtonTab button)
    {
        if (button.GetType() == _currentButton)
            return;

        _currentButton = button.GetType();
        _sfx.PlayClickTabButton();

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