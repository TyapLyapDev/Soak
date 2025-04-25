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

    public event Action Clicked;
    public event Action Entered;
    public event Action Exited;

    private void Awake() =>
        _initialSprite = _image.sprite;

    private void OnEnable() =>
        _image.sprite = _initialSprite;

    public void OnPointerEnter(PointerEventData eventData)
    {
        _image.sprite = _selectionSprite;
        Entered?.Invoke();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _image.sprite = _initialSprite;
        Exited?.Invoke();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _image.sprite = _initialSprite;
        Entered?.Invoke();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Clicked?.Invoke();
        Exited?.Invoke();
    }

    public void SetText(string text) =>
        _text.text = text;
}