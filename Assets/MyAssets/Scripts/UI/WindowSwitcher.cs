using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WindowSwitcher : MonoBehaviour
{
    [SerializeField] private Color _buttonSelectColor;
    [SerializeField] private Sprite _buttonTabSelectSprite;

    private WindowPanel[] _panels;
    private ButtonMenu[] _buttonsMenu;
    private ButtonPanel[] _buttonsPanels;
    private ButtonTab[] _buttonsTab;
    private TabContainer[] _tabsContainer;

    private ButtonMenuManager _buttonMenuManager;
    private ButtonPanelManager _buttonPanelManager;
    private PanelManager _panelManager;
    private TabManager _tabManager;

    public event Action ReturnToGamePressed;
    public event Action ButtonMenuHovered;
    public event Action ButtonMenuPressed;
    public event Action ChangesApplied;
    public event Action ChangesCanceled;

    private void Awake()
    {
        FindComponents();
        _buttonMenuManager = new(_buttonsMenu, OnButtonMenuCursorEntered, OnButtonMenuCursorExited, OnButtonMenuPressed);
        _buttonPanelManager = new(_buttonsPanels, OnButtonPanelClicked);
        _panelManager = new(_panels);
        _tabManager = new(_buttonsTab, _tabsContainer, _buttonTabSelectSprite);
    }

    private void FindComponents()
    {
        _panels = GetComponentsInChildren<WindowPanel>(true);
        _buttonsMenu = GetComponentsInChildren<ButtonMenu>(true);
        _buttonsPanels = GetComponentsInChildren<ButtonPanel>(true);
        _buttonsTab = GetComponentsInChildren<ButtonTab>(true);
        _tabsContainer = GetComponentsInChildren<TabContainer>(true);
    }

    private void OnButtonMenuCursorEntered(ButtonMenu button)
    {
        button.SetColor(_buttonSelectColor);
        ButtonMenuHovered?.Invoke();
    }

    private void OnButtonMenuCursorExited(ButtonMenu button)
    {
        button.SetInitialColor();
        ButtonMenuHovered?.Invoke();
    }

    private void OnButtonMenuPressed(ButtonMenu button)
    {
        button.SetInitialColor();
        ButtonMenuPressed?.Invoke();

        switch (button)
        {
            case ButtonReturnToGame:
                ReturnToGamePressed?.Invoke();
                break;

            case ButtonDisconnect:
                SceneManager.LoadScene(DataParams.SceneNames.Menu);
                break;

            case ButtonNewGame:
                SceneManager.LoadScene(DataParams.SceneNames.Game);
                break;

            case ButtonSettings:
                _panelManager.ShowPanel<PanelSettings>();
                _buttonMenuManager.HideButtons();
                break;

            case ButtonQuitGame:
                _panelManager.ShowPanel<PanelExit>();
                _buttonMenuManager.HideButtons();
                break;
        }
    }

    private void OnButtonPanelClicked(ButtonPanel button)
    {
        switch (button)
        {
            case ButtonClosePanel:
            case ButtonCancelPreferences:
                _panelManager.HidePanels();
                _buttonMenuManager.ShowButtons();
                break;

            case ButtonCancelExit:
                _panelManager.HidePanels();
                _buttonMenuManager.ShowButtons();
                ChangesCanceled?.Invoke();
                break;

            case ButtonOkPreferences:
                _panelManager.HidePanels();
                _buttonMenuManager.ShowButtons();
                ChangesApplied?.Invoke();
                break;

            case ButtonApplyPreferences:
                ChangesApplied?.Invoke();
                break;

            case ButtonExitConfirmation:
                Application.Quit();
                break;
        }
    }
}