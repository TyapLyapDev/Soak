using UnityEngine;

namespace GameConsoleFiXiK
{
    public static class Link
    {
        private static PanelGameConsole _panel;

        public static PanelGameConsole Panel
        {
            get
            {
                if (_panel == null)
                {
                    _panel = Object.FindFirstObjectByType<PanelGameConsole>();


                    if (_panel == null)
                    {
                        Debug.LogWarning("PanelGameConsole не найден в сцене.");
                    }
                }

                return _panel;
            }
        }
    }
}