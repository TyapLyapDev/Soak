using System;
using System.Collections.Generic;
using UnityEngine;

public enum ShooterType
{
    Teams,
    Loner,
}

public enum TeamType
{
    None,
    Terrorist,
    CounterTerrorist,
}

public class CharacterManager : MonoBehaviour
{
    private const TeamType PlayerTeam = TeamType.CounterTerrorist;

    [SerializeField] private Saver _saver;
    [SerializeField] private InputInformer _informer;
    [SerializeField] private SoundEffectPlayer2D _soundPlayer;
    [SerializeField] private Player _player;
    [SerializeField] private PointFolder _folder;
    [SerializeField] private SmartBot _counterTerroristBotPrefab;
    [SerializeField] private SmartBot _terroristBotPrefab;

    private CharacterPositionAssigner _positionAssigner;
    private CharacterAdder _adder;
    private RoundRestarter _roundRestarter;
    private CharacterRegistrator _registrator;

    private int _ctCountWin;
    private int _terCountWin;

    public event Action<Character> CharacterAdded;
    public event Action<Character> CharacterRemoved;
    public event Action<TeamType, int> TeamWinChanged;
    public event Action<Character> Died;
    public event Action<Character> Killed;
    public event Action RoundRestarted;

    public List<Character> Characters => _registrator.Characters;

    public CharacterRegistrator Registrator => _registrator;

    private void Awake()
    {
        _registrator = new(OnDied);
        _positionAssigner = new(_folder.transform);
        _roundRestarter = new(this, _positionAssigner, _registrator.Characters);
        _adder = new(_counterTerroristBotPrefab, _terroristBotPrefab, ShooterType.Teams, PlayerTeam);
    }

    private void Start()
    {
        RegisterPlayer();
        AddRangeBots();
    }

    private void OnEnable()
    {
        _informer.BotAddPressed += OnBotAddPressed;
        _informer.BotRemovePressed += OnBotRemovePressed;
        _informer.BotKillPressed += OnBotKillPressed;
        _roundRestarter.Restarted += OnRoundRestarted;
        _saver.SavesChanged += OnSavesChanged;
    }

    private void OnDisable()
    {
        _informer.BotAddPressed -= OnBotAddPressed;
        _informer.BotRemovePressed -= OnBotRemovePressed;
        _informer.BotKillPressed -= OnBotKillPressed;
        _roundRestarter.Restarted -= OnRoundRestarted;
        _saver.SavesChanged -= OnSavesChanged;
    }

    public int GetWinCount(TeamType team)
    {
        return team switch
        {
            TeamType.CounterTerrorist => _ctCountWin,
            TeamType.Terrorist => _terCountWin,
            _ => 0,
        };
    }

    private void OnBotAddPressed() =>
        AddBot();

    private void OnBotRemovePressed()
    {
        if (_adder.RemoveBot(out Character bot) == false)
            return;

        _registrator.Deregister(bot);
        CharacterRemoved?.Invoke(bot);

        Destroy(bot.gameObject);
    }

    private void OnBotKillPressed() =>
        _adder.KillBots();

    private void OnRoundRestarted() =>
        RoundRestarted?.Invoke();

    private void OnSavesChanged()
    {
        string playerName = _saver.PlayerName;

        if (_player.Name == playerName)
            return;

        _player.SetName(playerName);
    }

    private void OnDied(Character character)
    {
        UpdateFragsInfo(character);

        if (IsNeedRestartRound())
            _roundRestarter.Restart();

        Died?.Invoke(character);
    }

    private void RegisterPlayer()
    {
        _player.SetName(_saver.PlayerName);
        _player.SetTeam(PlayerTeam);
        _registrator.Register(_player);
        _positionAssigner.SetPosition(_player);

        CharacterAdded?.Invoke(_player);
    }

    private void AddRangeBots()
    {
        for (int i = 0; i < _saver.CountBot; i++)
            AddBot();
    }

    private void AddBot()
    {
        if (_registrator.Count >= _positionAssigner.PointsCount)
            return;

        Character bot = _adder.Add();
        _registrator.Register(bot);
        _positionAssigner.SetPosition(bot);

        CharacterAdded?.Invoke(bot);
    }

    private void UpdateFragsInfo(Character character)
    {
        Character killer = character.Killer;

        if (killer != null)
        {
            if (killer.Team == character.Team && killer.Team != TeamType.None)
                killer.DecreaseCountKill();
            else
                killer.IncreaseCountKill();

            Killed?.Invoke(killer);
        }     
    }

    private bool IsNeedRestartRound()
    {
        if (_roundRestarter.IsRoundFinished)
            return false;

        if (_registrator.IsEveryoneDead())
            return true;

        if (_registrator.IsTeamDeath(TeamType.CounterTerrorist))
        {
            _terCountWin++;
            _soundPlayer.PlayTerroristWin();
            TeamWinChanged?.Invoke(TeamType.Terrorist, _terCountWin);

            return true;
        }

        if (_registrator.IsTeamDeath(TeamType.Terrorist))
        {
            _ctCountWin++;
            _soundPlayer.PlayCounterTerroristWin();
            TeamWinChanged?.Invoke(TeamType.CounterTerrorist, _ctCountWin);

            return true;
        }

        return false;
    }
}