using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ButtonChangeInformer : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Color _selectedColor;

    private ButtonView _view;

    private void Awake() =>
        _view = new(GetComponent<Image>(), _selectedColor);

    public event Action<ButtonChangeInformer> DownPressed;

    public void OnPointerDown(PointerEventData eventData)
    {
        _view.Select();
        DownPressed?.Invoke(this);
    }

    public void OnPointerUp(PointerEventData eventData) =>
        _view.Deselect();
}