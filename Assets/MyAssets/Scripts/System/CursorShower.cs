using UnityEngine;

public class CursorShower : MonoBehaviour
{
    [SerializeField] private MenuShower _menuShower;
    [SerializeField] private PanelTeamSelectionPlayer _panelTeamSelectionPlayer;

    private void OnEnable()
    {
        _menuShower.ShowingStateChanged += OnShowingStateChanged;
        _panelTeamSelectionPlayer.ShowingStateChanged += OnShowingStateChanged;
    }

    private void OnDisable()
    {
        _menuShower.ShowingStateChanged -= OnShowingStateChanged;
        _panelTeamSelectionPlayer.ShowingStateChanged -= OnShowingStateChanged;
    }

    private void OnApplicationFocus(bool focus)
    {
        if (focus && (_menuShower.IsShowing == false && _panelTeamSelectionPlayer.gameObject.activeInHierarchy == false))
            HideCursor();
        else
            ShowCursor();
    }

    private void OnShowingStateChanged()
    {
        if (_menuShower.IsShowing || _panelTeamSelectionPlayer.gameObject.activeInHierarchy)
            ShowCursor();
        else
            HideCursor();
    }

    private void ShowCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    private void HideCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}