using UnityEngine;
using UnityEngine.UI;

public class AimMarker : MonoBehaviour
{
    [SerializeField] private Image _image;
    
    public void SetColor(Color color) =>
        _image.color = color;
    
    public void SetLocalScale(float value) =>
        transform.localScale = Vector3.one * value;
}