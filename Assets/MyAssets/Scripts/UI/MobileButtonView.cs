using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class MobileButtonView : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Color _selectedColor;
    
    private Image _image;
    private Color _defaultColor;

    private void Awake() =>
        _image = GetComponent<Image>();

    private void Start() =>
        _defaultColor = _image.color;

    public void OnPointerDown(PointerEventData eventData) =>
        _image.color = _selectedColor;

    public void OnPointerUp(PointerEventData eventData) =>
        _image.color = _defaultColor;
}