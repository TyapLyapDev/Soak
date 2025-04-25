using System;
using UnityEngine;

public class PanelTeamSelectionPlayer : MonoBehaviour
{
    [SerializeField] private PanelTeamSelectionInformer _panelSelectionInformer;
    [SerializeField] private InputInformer _playerInputInformer;
    [SerializeField] private Player _player;
    [SerializeField] private CharacterManager _characterManager;
    [SerializeField] private ViewpointLister _viewpointLister;

    public event Action ShowingStateChanged;

    private void Awake() =>
        _playerInputInformer.enabled = false;

    private void Start() =>
        _player.DisableControl();

    private void OnEnable()
    {
        _panelSelectionInformer.TerroristTeamPressed += OnPressedTerroristTeam;
        _panelSelectionInformer.CounterTerroristTeamPressed += OnPressedCounterTerroristTeam;
        _panelSelectionInformer.RandomTeamPressed += OnPressedRandomTeam;
        _panelSelectionInformer.AgainstEveryonePressed += OnAgainstEveryone;
        _panelSelectionInformer.ObserverPressed += OnPressedObserver;
    }

    private void OnDisable()
    {
        _panelSelectionInformer.TerroristTeamPressed -= OnPressedTerroristTeam;
        _panelSelectionInformer.CounterTerroristTeamPressed -= OnPressedCounterTerroristTeam;
        _panelSelectionInformer.RandomTeamPressed -= OnPressedRandomTeam;
        _panelSelectionInformer.AgainstEveryonePressed -= OnAgainstEveryone;
        _panelSelectionInformer.ObserverPressed -= OnPressedObserver;
    }

    private void OnPressedTerroristTeam() =>
        HandleSelectTeam(TeamType.Terrorist);

    private void OnPressedCounterTerroristTeam() =>
        HandleSelectTeam(TeamType.CounterTerrorist);

    private void OnPressedRandomTeam()
    {
        TeamType teamType = UnityEngine.Random.Range(0, 2) == 0 ? TeamType.Terrorist : TeamType.CounterTerrorist;
        HandleSelectTeam(teamType);
    }

    private void OnAgainstEveryone() =>
        HandleSelectTeam(TeamType.AgainstEveryone);

    private void OnPressedObserver() =>
        HandleSelectTeam(TeamType.Observer);

    private void HandleSelectTeam(TeamType teamType)
    {
        gameObject.SetActive(false);
        _viewpointLister.gameObject.SetActive(false);

        _playerInputInformer.enabled = true;

        _characterManager.RegisterPlayer(teamType);
        _characterManager.StartGame();
        ShowingStateChanged?.Invoke();

        if (teamType == TeamType.Observer)
            _player.LeaveOnlySoul();
        else
            _player.EnableControl();
    }
}