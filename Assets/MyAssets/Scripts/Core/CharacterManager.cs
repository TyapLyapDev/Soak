using System;
using System.Collections.Generic;
using UnityEngine;

public enum ShooterType
{
    Teams,
    Loner,
}

public enum TeamTypes
{
    None,
    Terrorist,
    CounterTerrorist,
}

public enum CharacterType
{
    Player,
    SmartBot,
}

public class CharacterManager : MonoBehaviour
{
    private const string PlayerName = ">>-Стрелок->";
    private const TeamTypes PlayerTeam = TeamTypes.CounterTerrorist;

    [SerializeField] private InputInformer _informer;
    [SerializeField] private SoundEffectPlayer2D _soundPlayer;
    [SerializeField] private SprayFromWallShower _spray;
    [SerializeField] private Player _player;
    [SerializeField] private SmartBot _counterTerroristBotPrefab;
    [SerializeField] private SmartBot _terroristBotPrefab;
    [SerializeField] private PointFolder _folder;
    [SerializeField] private int _countBot;

    private CharacterPositionAssigner _positionAssigner;
    private CharacterAdder _adder;
    private RoundRestarter _roundRestarter;

    private readonly List<Character> _characters = new();

    private int _ctCountWin;
    private int _terCountWin;
    private bool _isRoundFinished;

    public event Action Changed;
    public event Action TeamWinChanged;
    public event Action<Character, float> HealthChanged;
    public event Action<Character, Character> Murdered;

    public int CounterTerroristTeamCountWin => _ctCountWin;

    public int TerroristTeamCountWin => _terCountWin;

    public IReadOnlyList<Character> Characters => _characters;

    private void Awake()
    {
        _positionAssigner = new(_folder.transform);
        _roundRestarter = new(this, _positionAssigner, _characters);
    }

    private void Start()
    {
        _adder = new(_counterTerroristBotPrefab, _terroristBotPrefab, ShooterType.Teams, PlayerTeam);

        if (_countBot > _positionAssigner.PointsCount - 1)
            _countBot = _positionAssigner.PointsCount - 1;

        RegisterPlayer();
        AddRangeBots();
    }

    private void RegisterPlayer()
    {
        _player.Init(PlayerName, PlayerTeam);
        Subscribe(_player);
        _positionAssigner.SetPosition(_player);
        _characters.Add(_player);
    }

    private void AddRangeBots()
    {
        for (int i = 0; i < _countBot; i++)
        {
            Character character = _adder.Add();
            Subscribe(character);
            _positionAssigner.SetPosition(character);
            _characters.Add(character);
        }
    }

    private void OnEnable()
    {
        _informer.BotAddPressed += OnBotAddPressed;
        _informer.BotRemovePressed += OnBotRemovePressed;
        _informer.BotKillPressed += OnBotKillPressed;
        _roundRestarter.Restarted += OnRoundRestarted;
    }

    private void OnDisable()
    {
        _informer.BotAddPressed -= OnBotAddPressed;
        _informer.BotRemovePressed -= OnBotRemovePressed;
        _informer.BotKillPressed -= OnBotKillPressed;
        _roundRestarter.Restarted -= OnRoundRestarted;
    }

    private void OnBotAddPressed()
    {
        if (_positionAssigner.PointsCount <= _characters.Count)
            return;

        Character bot = _adder.Add();
        _positionAssigner.SetPosition(bot);
        _characters.Add(bot);
        Subscribe(bot);

        Changed?.Invoke();
    }

    private void OnBotRemovePressed()
    {
        if (_characters.Count == 0)
            return;

        if (_adder.RemoveBot(out Character bot) == false)
            return;

        _characters.Remove(bot);

        if (bot.IsDeath == false)
            Unsubscribe(bot);

        Destroy(bot.gameObject);

        Changed?.Invoke();
    }

    private void OnBotKillPressed()
    {
        _adder.KillBots();
        TeamWinChanged?.Invoke();
    }

    private void OnRoundRestarted() =>
        _isRoundFinished = false;

    private void OnHealthChanged(Character character, float healthValue) =>
        HealthChanged?.Invoke(character, healthValue);

    private void OnDied(Character character)
    {
        Character killer = character.Killer;

        if (killer != null)
        {
            if (killer.Team == character.Team && (killer.Team == TeamTypes.Terrorist || killer.Team == TeamTypes.CounterTerrorist))
                killer.DecreaseCountKill();
            else
                killer.IncreaseCountKill();
            TeamWinChanged?.Invoke();
        }
        else
            character.IncreaseCountDeath();

        if (_isRoundFinished == false)
        {
            if (IsAllDeath())
            {
                RestartRound();
            }
            else if (IsAllTeamDeath(TeamTypes.CounterTerrorist))
            {
                _terCountWin++;
                _soundPlayer.PlayTerroristWin();
                RestartRound();
            }
            else if (IsAllTeamDeath(TeamTypes.Terrorist))
            {
                _ctCountWin++;
                _soundPlayer.PlayCounterTerroristWin();
                RestartRound();
            }
        }            

        Murdered?.Invoke(killer, character);
        Changed?.Invoke();
    }

    private void RestartRound()
    {
        _isRoundFinished = true;
        _roundRestarter.Restart();
        TeamWinChanged?.Invoke();
    }

    private void Subscribe(Character character)
    {
        character.SetListCharacters(_characters);
        character.Died += OnDied;
        character.HealthChanged += OnHealthChanged;
        _spray.Subscribe(character.Jet);
    }

    private void Unsubscribe(Character character)
    {
        character.Died -= OnDied;
        character.HealthChanged -= OnHealthChanged;
        _spray.Subscribe(character.Jet);
    }

    private bool IsAllTeamDeath(TeamTypes team)
    {
        foreach (Character character in _characters)
        {
            if (character.Team != team)
                continue;

            if (character.IsDeath == false)
                return false;
        }

        return true;
    }

    private bool IsAllDeath()
    {
        foreach (Character character in _characters)
            if (character.IsDeath == false)
                return false;

        return true;
    }
}