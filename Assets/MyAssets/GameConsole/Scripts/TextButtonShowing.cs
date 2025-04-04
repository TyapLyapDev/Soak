using TMPro;
using UnityEngine;

namespace GameConsoleFiXiK
{
    public class TextButtonShowing : MonoBehaviour
    {
        private readonly string _textArrowLeft = "<";
        private readonly string _textArrowRight = ">";

        private TextMeshProUGUI _text;

        private void Awake()
        {
            _text = GetComponent<TextMeshProUGUI>();
        }

        private void OnEnable()
        {
            ReplaceText();
            Link.Panel.ActionPanelShowChanged += ReplaceText;
        }

        private void OnDisable()
        {
            Link.Panel.ActionPanelShowChanged -= ReplaceText;
        }

        private void ReplaceText()
        {
            _text.text = Link.Panel.IsShowing ? _textArrowRight : _textArrowLeft;
        }
    }
}