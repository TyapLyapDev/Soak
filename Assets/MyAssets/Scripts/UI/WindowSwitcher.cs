using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WindowSwitcher : MonoBehaviour
{
    [SerializeField] private Color _buttonSelectColor;

    private WindowPanel[] _panels;
    private ButtonMenu[] _buttonsMenu;
    private ButtonPanel[] _buttonsPanels;

    private ButtonMenuManager _buttonMenuManager;
    private ButtonPanelManager _buttonPanelManager;
    private PanelManager _panelManager;

    public event Action ReturnToGamePressed;
    public event Action ButtonMenuHovered;
    public event Action ButtonMenuPressed;

    private void Awake()
    {
        FindComponents();
        _buttonMenuManager = new(_buttonsMenu, OnButtonMenuCursorEntered, OnButtonMenuCursorExited, OnButtonMenuPressed);
        _buttonPanelManager = new(_buttonsPanels, OnButtonPanelClicked);
        _panelManager = new(_panels);
    }

    private void FindComponents()
    {
        _panels = GetComponentsInChildren<WindowPanel>(true);
        _buttonsMenu = GetComponentsInChildren<ButtonMenu>(true);
        _buttonsPanels = GetComponentsInChildren<ButtonPanel>(true);
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
                SceneManager.LoadScene(NameData.Scenes.Menu);
                break;

            case ButtonNewGame:
                SceneManager.LoadScene(NameData.Scenes.Game);
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
                _panelManager.HidePanels();
                _buttonMenuManager.ShowButtons();
                break;

            case ButtonCancelExit:
                _panelManager.HidePanels();
                _buttonMenuManager.ShowButtons();
                break;

            case ButtonExitConfirmation:
                Application.Quit();
                break;
        }
    }
}