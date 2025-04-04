using UnityEngine;

namespace GameConsoleFiXiK
{
    public class Hotkeys : MonoBehaviour
    {
        void Update()
        {
            if (Input.GetKeyDown(Link.Panel.GetKeyShowHidePanel))
                Link.Panel.SwitchShow();

            if (Input.GetKeyDown(Link.Panel.GetKeyClearMessage) && Link.Panel.IsShowing) 
                Link.Panel.Clear();
        }
    }
}