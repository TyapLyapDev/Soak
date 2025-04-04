using UnityEngine;

public class CursorShower : MonoBehaviour
{
    [SerializeField] private MenuShower _menuShower;

    private void OnEnable() =>
        _menuShower.ShowingStateChanged += OnShowingStateChanged;

    private void OnDisable() =>
        _menuShower.ShowingStateChanged -= OnShowingStateChanged;

    private void OnShowingStateChanged(bool isShowing) =>
        Cursor.visible = isShowing;

    private void OnApplicationFocus(bool focus) =>
        Cursor.visible = focus && _menuShower.IsShowing == false ? false : true;
}