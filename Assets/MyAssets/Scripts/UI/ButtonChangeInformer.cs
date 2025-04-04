using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonChangeInformer : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    public event Action<ButtonChangeInformer> DownPressed;
    public event Action<ButtonChangeInformer> CursorEntered;
    public event Action<ButtonChangeInformer> CursorExited;

    public void OnPointerDown(PointerEventData eventData) =>
        DownPressed?.Invoke(this);

    public void OnPointerEnter(PointerEventData eventData) =>
        CursorEntered?.Invoke(this);

    public void OnPointerExit(PointerEventData eventData) =>
        CursorExited?.Invoke(this);
}