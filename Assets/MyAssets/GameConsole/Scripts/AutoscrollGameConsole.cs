using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GameConsoleFiXiK
{
    public class AutoscrollGameConsole : MonoBehaviour, IPointerDownHandler
    {
        private ScrollRect _scrollRect;

        private void Awake()
        {
            _scrollRect = GetComponent<ScrollRect>();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Link.Panel.SetAutoscroll(false);
        }

        private void Update()
        {
            Scroll();
        }

        private void Scroll()
        {
            if (_scrollRect.verticalNormalizedPosition <= 0 || Link.Panel.IsAutoscrollOn == false || _scrollRect.content.rect.height <= 0)
                return;

            float currentSpeed = Link.Panel.GetSpeedScrolling * Time.deltaTime;
            _scrollRect.verticalNormalizedPosition -= currentSpeed / _scrollRect.content.rect.height;
        }
    }
}