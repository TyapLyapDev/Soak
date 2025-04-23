using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonTeamSelection : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerClickHandler
{
    [SerializeField] private Image _image;
    [SerializeField] private Sprite _selectionSprite;
    [SerializeField] private TextMeshProUGUI _text;

    private Sprite _initialSprite;

    public event Action<ButtonTeamSelection> Clicked;

    private void Awake() =>
        _initialSprite = _image.sprite;

    public void OnPointerEnter(PointerEventData eventData) =>
        _image.sprite = _selectionSprite;

    public void OnPointerExit(PointerEventData eventData) =>
        _image.sprite = _initialSprite;

    public void OnPointerDown(PointerEventData eventData) =>
        _image.sprite = _initialSprite;

    public void SetText(string text) =>
        _text.text = text;

    public void OnPointerClick(PointerEventData eventData) =>
        Clicked?.Invoke(this);
}