using System;
using System.Collections.Generic;
using System.Linq;

public class PanelManager
{
    private WindowPanel[] _panels;
    private Dictionary<Type, WindowPanel> _panelsDictionary;

    public PanelManager(WindowPanel[] panels)
    {
        _panels = panels.Where(p => p != null).ToArray();
        _panelsDictionary = _panels.ToDictionary(panel => panel.GetType());
        HidePanels();
    }

    public void ShowPanel<T>() where T : WindowPanel
    {
        if (_panelsDictionary.TryGetValue(typeof(T), out var panel) == false)
            return;

        HidePanels();
        panel.Enable();
    }

    public void HidePanels() =>
        _panelsDictionary.Values.ToList().ForEach(panel => panel.Disable());
}