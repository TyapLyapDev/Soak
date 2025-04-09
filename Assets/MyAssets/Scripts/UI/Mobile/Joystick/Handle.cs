using UnityEngine;
using UnityEngine.UI;

namespace UI.Joystick
{
    [RequireComponent(typeof(Image))]
    public class Handle : MonoBehaviour
    {
        [SerializeField] private Color _activeColor;

        private Image _image;
        private Color _inactiveColor;

        private void Awake()
        {
            _image = GetComponent<Image>();
            _inactiveColor = _image.color;
        }

        private void Start() =>
            Deselect();

        public void Select() =>
            _image.color = _activeColor;

        public void Deselect() =>
            _image.color = _inactiveColor;
    }
}