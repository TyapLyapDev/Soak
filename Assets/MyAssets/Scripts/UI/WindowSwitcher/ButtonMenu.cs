using System;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(ButtonMenuView))]
public class ButtonMenu : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    private ButtonMenuView _view;

    public event Action<ButtonMenu> DownPressed;
    public event Action<ButtonMenu> CursorEntered;
    public event Action<ButtonMenu> CursorExited;

    private void Awake() =>
        _view = GetComponent<ButtonMenuView>();    

    public void OnPointerDown(PointerEventData eventData) =>
        DownPressed?.Invoke(this);

    public void OnPointerEnter(PointerEventData eventData) =>
        CursorEntered?.Invoke(this);

    public void OnPointerExit(PointerEventData eventData) =>
        CursorExited?.Invoke(this);

    public void SetInitialColor() =>
        _view.SetInitialColor();

    public void SetColor(Color color) =>
        _view.SetColor(color);

    public void Enable() =>
        gameObject.SetActive(true);

    public void Disable() =>
        gameObject.SetActive(false);
}