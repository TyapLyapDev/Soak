using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameConsoleFiXiK
{
    public class MessageBox
    {
        public MessageBox(GameObject gameObject, string message, TypeMessage type)
        {
            GameObject = gameObject;
            Message = message;
            Type = type;
            Image = GameObject.GetComponent<Image>();
            Text = GameObject.GetComponentInChildren<TextMeshProUGUI>();
        }

        public GameObject GameObject { get; }

        public string Message { get; }

        public TypeMessage Type { get; }

        public Image Image { get; }

        public TextMeshProUGUI Text { get; }
    }
}