using UnityEngine;

public class CursorShower : MonoBehaviour
{
    [SerializeField] private MenuShower _menuShower;

    private void OnEnable() =>
        _menuShower.ShowingStateChanged += OnShowingStateChanged;

    private void OnDisable() =>
        _menuShower.ShowingStateChanged -= OnShowingStateChanged;

    private void OnApplicationFocus(bool focus)
    {
        Cursor.visible = focus && _menuShower.IsShowing == false ? false : true;
        if (focus && _menuShower.IsShowing == false)
            HideCursor();
        else
            ShowCursor();
    }

    private void OnShowingStateChanged(bool isShowing)
    {
        if (isShowing)
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