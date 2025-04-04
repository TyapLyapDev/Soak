using UnityEngine;
using UnityEngine.UI;

namespace UI.Joystick
{
    [RequireComponent(typeof(Image))]
    public class Background : MonoBehaviour
    {
        private const float CenterAnchorPosition = 0.5f;

        [SerializeField] private Color _activeColor;

        private Color _inactiveColor;
        private Image _image;
        private RectTransform _rectTransform;

        private Vector2 _initialPosition;
        private Vector2 _initialAnchorsMin;
        private Vector2 _initialAnchorsMax;
        private Vector2 _initialPivot;

        private void Awake()
        {
            _image = GetComponent<Image>();
            _rectTransform = _image.rectTransform;
            _inactiveColor = _image.color;
        }

        private void Start()
        {
            SaveInitialProperties();
            Deselect();
        }        

        public void Select()
        {
            _image.color = _activeColor;
            SetCenterAnchorsAndPivot();
        }

        public void Deselect()
        {
            _image.color = _inactiveColor;
            ResetToInitialProperties();
        }

        private void SaveInitialProperties()
        {
            _initialPosition = _rectTransform.anchoredPosition;
            _initialAnchorsMin = _rectTransform.anchorMin;
            _initialAnchorsMax = _rectTransform.anchorMax;
            _initialPivot = _rectTransform.pivot;
        }

        private void SetCenterAnchorsAndPivot()
        {
            Vector2 centerAnchor = new Vector2(CenterAnchorPosition, CenterAnchorPosition);
            _rectTransform.pivot = centerAnchor;
            _rectTransform.anchorMin = centerAnchor;
            _rectTransform.anchorMax = centerAnchor;
        }

        private void ResetToInitialProperties()
        {
            _rectTransform.anchoredPosition = _initialPosition;
            _rectTransform.pivot = _initialPivot;
            _rectTransform.anchorMin = _initialAnchorsMin;
            _rectTransform.anchorMax = _initialAnchorsMax;
        }
    }
}