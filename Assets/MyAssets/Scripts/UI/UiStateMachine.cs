using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiStateMachine : MonoBehaviour
{
    [SerializeField] private WindowSwitcher _windowSwitcher;

    private Dictionary<Type, Action[]> _states;

    private void Awake() =>
        InitStates();

    private void OnEnable() =>
        _windowSwitcher.HasButtonPressed += OnButtonPressed;

    private void OnDisable() =>
        _windowSwitcher.HasButtonPressed -= OnButtonPressed;

    private void InitStates()
    {
        _states = new Dictionary<Type, Action[]>
        {
            { typeof(ButtonReturnToGame),           new Action[] { _windowSwitcher.ReturnToGame } },
            { typeof(ButtonDisconnectGame),         new Action[] { () => SceneManager.LoadScene(DataParams.SceneNames.Menu) } },
            { typeof(ButtonStartNewGame),           new Action[] { () => SceneManager.LoadScene(DataParams.SceneNames.Game) } },
            { typeof(ButtonOpenPanelPreferences),   new Action[] { _windowSwitcher.ShowPanel<PanelPreferences> } },
            { typeof(ButtonOpenPanelExitGame),      new Action[] { _windowSwitcher.ShowPanel<PanelExitGame> } },
            { typeof(ButtonClosePanelPreferences),  new Action[] { _windowSwitcher.HidePanel<PanelPreferences>, _windowSwitcher.LoadPreferences } },
            { typeof(ButtonCancelPreferences),      new Action[] { _windowSwitcher.HidePanel<PanelPreferences>, _windowSwitcher.LoadPreferences } },
            { typeof(ButtonOkPreferences),          new Action[] { _windowSwitcher.HidePanel<PanelPreferences>, _windowSwitcher.SavePreferences } },
            { typeof(ButtonCancelExit),             new Action[] { _windowSwitcher.HidePanel<PanelExitGame> } },
            { typeof(ButtonApplyPreferences),       new Action[] { _windowSwitcher.SavePreferences } },
            { typeof(ButtonExitConfirmation),       new Action[] { Application.Quit } }
        };
    }

    private void OnButtonPressed(Type type)
    {
        if (_states.TryGetValue(type, out Action[] actions))
            foreach(Action action in actions)
                action.Invoke();
    }
}