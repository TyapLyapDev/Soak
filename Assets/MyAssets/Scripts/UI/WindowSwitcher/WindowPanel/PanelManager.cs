using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PanelManager
{
    private readonly WindowPanel[] _panels;
    private readonly Dictionary<Type, WindowPanel> _panelsDictionary;

    public PanelManager(Transform parent)
    {
        _panels = parent.GetComponentsInChildren<WindowPanel>(true);
        _panelsDictionary = _panels.ToDictionary(panel => panel.GetType());
        HidePanels();
    }

    public void ShowPanel<T>() where T : StateElement
    {
        if (_panelsDictionary.TryGetValue(typeof(T), out WindowPanel panel) == false)
            throw new Exception($"Данный тип панели {typeof(T)} не зарегистрирован в словаре");

        HidePanels();
        panel.gameObject.SetActive(true);
    }

    public void HidePanel<T>() where T : StateElement
    {
        if (_panelsDictionary.TryGetValue(typeof(T), out WindowPanel panel) == false)
            throw new Exception($"Данный тип панели {typeof(T)} не зарегистрирован в словаре");

        panel.gameObject.SetActive(false);
    }

    public void HidePanels() =>
        _panelsDictionary.Values.ToList().ForEach(panel => panel.gameObject.SetActive(false));
}