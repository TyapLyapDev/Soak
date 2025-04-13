using System;
using UnityEngine;

public class WindowSwitcher : MonoBehaviour
{
    [SerializeField] private SoundEffectPlayer2D _sfxPlayer;
    [SerializeField] private Saver _saver;
    [SerializeField] private Color _buttonSelectColor;
    [SerializeField] private Sprite _buttonTabSelectSprite;

    private WindowSwitchingSubscriber _subscriber;
    private ButtonMenuManager _buttonMenuManager;
    private ButtonPanelManager _buttonPanelManager;
    private PanelManager _panelManager;
    private TabManager _tabManager;

    public event Action ReturnToGamePressed;
    public event Action<Type> HasButtonPressed;

    private void Awake()
    {
        _panelManager = new(transform);
        _buttonPanelManager = new(transform);
        _buttonMenuManager = new(transform, _buttonSelectColor, _sfxPlayer);
        _tabManager = new(transform, _sfxPlayer, _buttonTabSelectSprite);
        _subscriber = new(_buttonMenuManager, _buttonPanelManager, _tabManager);
    }

    private void OnEnable() =>
        _subscriber.Subscribe(OnButtonMenuPressed);

    private void OnDisable() =>
        _subscriber.Unsubscribe(OnButtonMenuPressed);

    private void OnButtonMenuPressed(StateElement element)
    {
        HasButtonPressed?.Invoke(element.GetType());
        _sfxPlayer.PlayClickButton();        
    }

    public void ReturnToGame() =>
        ReturnToGamePressed?.Invoke();

    public void SavePreferences() =>
        _saver.Save();

    public void LoadPreferences() =>
        _saver.Load();

    public void ShowPanel<T>() where T : StateElement
    {
        _panelManager.ShowPanel<T>();
        _buttonMenuManager.HideButtons();
    }

    public void HidePanel<T>() where T : StateElement
    {
        _panelManager.HidePanel<T>();
        _buttonMenuManager.ShowButtons();
    }
}