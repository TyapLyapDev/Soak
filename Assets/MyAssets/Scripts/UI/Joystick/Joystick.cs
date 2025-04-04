using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.Joystick
{
    public class Joystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
    {
        [SerializeField] private Background _background;
        [SerializeField] private Handle _handle;

        private RectTransform _handleRectTransform;
        private RectTransform _backgroundRectTransform;
        private RectTransform _rectTransform;

        private Vector2 _inputVector;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _handleRectTransform = _handle.GetComponent<RectTransform>();
            _backgroundRectTransform = _background.GetComponent<RectTransform>();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _handle.Select();
            _background.Select();

            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_rectTransform, eventData.position, null, out Vector2 backgroundPosition))
                _backgroundRectTransform.anchoredPosition = backgroundPosition;
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_backgroundRectTransform, eventData.position, null, out Vector2 localPoint) == false)
                return;

            _inputVector = CalculateInputVector(localPoint);
            UpdateHandlePosition();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            ResetInput();
            _handle.Deselect();
            _background.Deselect();
        }

        public bool IsInput(out Vector2 direction)
        {
            direction = _inputVector;

            return direction != Vector2.zero;
        }

        private Vector2 CalculateInputVector(Vector2 localPoint)
        {
            localPoint.x = localPoint.x * 2 / _backgroundRectTransform.sizeDelta.x;
            localPoint.y = localPoint.y * 2 / _backgroundRectTransform.sizeDelta.y;

            return localPoint.magnitude > 1f ? localPoint.normalized : localPoint;
        }

        private void UpdateHandlePosition()
        {
            _handleRectTransform.anchoredPosition = new Vector2(
                _inputVector.x * (_backgroundRectTransform.sizeDelta.x / 2),
                _inputVector.y * (_backgroundRectTransform.sizeDelta.y / 2));
        }

        private void ResetInput()
        {
            _inputVector = Vector2.zero;
            _handleRectTransform.anchoredPosition = Vector2.zero;
        }
    }
}