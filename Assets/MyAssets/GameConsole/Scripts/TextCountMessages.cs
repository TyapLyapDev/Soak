using TMPro;
using UnityEngine;

namespace GameConsoleFiXiK
{
    public class TextCountMessages : MonoBehaviour
    {
        private TextMeshProUGUI _text;

        private void Awake()
        {
            _text = GetComponent<TextMeshProUGUI>();
        }

        private void OnEnable()
        {
            SetTextNewCount();
            Link.Panel.ActionNumberMessagesChanged += SetTextNewCount;
        }

        private void OnDisable()
        {
            Link.Panel.ActionNumberMessagesChanged -= SetTextNewCount;
        }

        private void SetTextNewCount()
        {
            _text.text = Link.Panel.NumberMessages.ToString();
        }
    }
}