using UnityEngine;
using UnityEngine.UI;

namespace GameConsoleFiXiK
{
    public class ToggleAutoscroll : MonoBehaviour
    {
        private Toggle _toggle;

        private void Awake()
        {
            _toggle = GetComponent<Toggle>();
            _toggle.onValueChanged.AddListener(OnValueChanged);
        }

        private void OnEnable()
        {
            Switch();
            Link.Panel.ActionAutoscrollChanged += Switch;
        }

        private void OnDisable()
        {
            Link.Panel.ActionAutoscrollChanged -= Switch;
        }

        private void OnValueChanged(bool isOn)
        {
            if (Link.Panel.IsAutoscrollOn != isOn)
                Link.Panel.SetAutoscroll(isOn);
        }

        private void Switch()
        {
            if (_toggle.isOn != Link.Panel.IsAutoscrollOn)
                _toggle.isOn = Link.Panel.IsAutoscrollOn;
        }
    }
}