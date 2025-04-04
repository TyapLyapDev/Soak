using UnityEngine;

namespace GameConsoleFiXiK
{
    public class PanelAnimation : MonoBehaviour
    {
        private readonly string _textIsOpen = "IsOpen";
        private readonly string _textIsClosed = "IsClosed";

        private Animator _animator;
        private bool _isShowing;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            Link.Panel.ActionPanelShowChanged += Switch;
        }

        private void OnDisable()
        {
            Link.Panel.ActionPanelShowChanged -= Switch;
        }

        private void Switch()
        {
            if (_isShowing != Link.Panel.IsShowing)
            {
                _isShowing = Link.Panel.IsShowing;
                Animate();
            }
        }

        private void Animate()
        {
            _animator.SetBool(_textIsOpen, Link.Panel.IsShowing);
            _animator.SetBool(_textIsClosed, !Link.Panel.IsShowing);
        }
    }
}