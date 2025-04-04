using UnityEngine;
using UnityEngine.EventSystems;

namespace GameConsoleFiXiK
{
    public class ScrollBar : MonoBehaviour, IPointerDownHandler
    {
        public void OnPointerDown(PointerEventData eventData)
        {
            Link.Panel.SetAutoscroll(false);
        }
    }
}