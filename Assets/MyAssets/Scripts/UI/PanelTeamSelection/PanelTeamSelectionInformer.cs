using System;
using UnityEngine;

public class PanelTeamSelectionInformer : MonoBehaviour
{
    private const KeyCode KeyTerroristTeam = KeyCode.Alpha1;
    private const KeyCode KeyCounterTerroristTeam = KeyCode.Alpha2;
    private const KeyCode KeyRandomTeam = KeyCode.Alpha3;
    private const KeyCode KeyAgainstEveryone = KeyCode.Alpha4;
    private const KeyCode KeyObserver = KeyCode.Alpha5;

    [SerializeField] private ButtonTeamSelection _buttonTerroristTeam;
    [SerializeField] private ButtonTeamSelection _buttonCounterTerroristTeam;
    [SerializeField] private ButtonTeamSelection _buttonRandomTeam;
    [SerializeField] private ButtonTeamSelection _buttonAgainstEveryone;
    [SerializeField] private ButtonTeamSelection _buttonObserver;

    public event Action TerroristTeamPressed;
    public event Action CounterTerroristTeamPressed;
    public event Action RandomTeamPressed;
    public event Action AgainstEveryonePressed;
    public event Action ObserverPressed;

    private void Update()
    {
        if (Input.GetKeyDown(KeyTerroristTeam))
            OnPressedTerroristTeam();

        if (Input.GetKeyDown(KeyCounterTerroristTeam))
            OnPressedCounterTerroristTeam();        

        if (Input.GetKeyDown(KeyRandomTeam))
            OnPressedRandomTeam();

        if (Input.GetKeyDown(KeyAgainstEveryone))
            OnPressedAgainstEveryone();

        if (Input.GetKeyDown(KeyObserver))
            OnPressedObserver();
    }

    private void OnEnable()
    {
        _buttonTerroristTeam.Clicked += OnPressedTerroristTeam;
        _buttonCounterTerroristTeam.Clicked += OnPressedCounterTerroristTeam;
        _buttonRandomTeam.Clicked += OnPressedRandomTeam;
        _buttonAgainstEveryone.Clicked += OnPressedAgainstEveryone;
        _buttonObserver.Clicked += OnPressedObserver;
    }

    private void OnDisable()
    {
        _buttonTerroristTeam.Clicked -= OnPressedTerroristTeam;
        _buttonCounterTerroristTeam.Clicked -= OnPressedCounterTerroristTeam;
        _buttonRandomTeam.Clicked -= OnPressedRandomTeam;
        _buttonAgainstEveryone.Clicked -= OnPressedAgainstEveryone;
        _buttonObserver.Clicked -= OnPressedObserver;
    }

    private void OnPressedTerroristTeam() =>
        TerroristTeamPressed?.Invoke();

    private void OnPressedCounterTerroristTeam() =>
        CounterTerroristTeamPressed?.Invoke();

    private void OnPressedRandomTeam() =>
        RandomTeamPressed?.Invoke();
    
    private void OnPressedAgainstEveryone() =>
        AgainstEveryonePressed?.Invoke();

    private void OnPressedObserver() =>
        ObserverPressed?.Invoke();
}