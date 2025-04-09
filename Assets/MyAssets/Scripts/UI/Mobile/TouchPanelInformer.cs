using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchPanelInformer : MonoBehaviour, IDragHandler
{
    public event Action<Vector2> Rotated;

    public void OnDrag(PointerEventData eventData) =>
        Rotated?.Invoke(eventData.delta / Screen.width);
}