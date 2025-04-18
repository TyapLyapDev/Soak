using UnityEngine;
using UnityEngine.UI;

namespace GameConsoleFiXiK
{
    public class ButtonClearGameConsole : MonoBehaviour
    {
        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            Link.Panel.Clear();
        }
    }
}