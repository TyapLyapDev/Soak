using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterAdder
{
    private readonly Character _counterTerroristBotPrefab;
    private readonly Character _terroristBotPrefab;
    private readonly List<Character> _bots = new();
    private readonly List<string> _names;
    private readonly ShooterType _shooterType;
    private bool _isCounterTerrorist;

    public CharacterAdder(Character counterTerroristrefab, Character terroristrefab, ShooterType shooterType, TeamTypes player)
    {
        _counterTerroristBotPrefab = counterTerroristrefab;
        _terroristBotPrefab = terroristrefab;
        _names = DataParams.Character.Names.ToList();
        _shooterType = shooterType;
        _isCounterTerrorist = player == TeamTypes.CounterTerrorist;
    }

    public List<Character> Add(int count)
    {
        List<Character> bots = new();

        for (int i = 0; i < count; i++)
                bots.Add(Add());

        _bots.AddRange(bots);

        return bots;
    }

    public Character Add()
    {
        TeamTypes teamType = GetTeamType();

        Character bot = teamType == TeamTypes.CounterTerrorist ? 
            Object.Instantiate(_counterTerroristBotPrefab, null) : 
            Object.Instantiate(_terroristBotPrefab, null);

        _bots.Add(bot);

        string name = GetName();
        

        bot.Init(name, teamType);

        return bot;
    }

    public bool RemoveBot(out Character bot)
    {
        bot = null;

        if (_bots.Count == 0)
            return false;

        int id = Random.Range(0, _bots.Count);
        bot = _bots[id];
        _names.Add(bot.Name);
        _bots.Remove(bot);

        return true;
    }

    public void KillBots()
    {
        foreach (Character bot in _bots)
            if (bot.IsDeath == false)
                bot.Kill();
    }

    private string GetName()
    {
        string name = _names[Random.Range(0, _names.Count)];
        _names.Remove(name);

        return name;
    }

    private TeamTypes GetTeamType()
    {
        if (_shooterType == ShooterType.Loner)
            return TeamTypes.None;

        _isCounterTerrorist = !_isCounterTerrorist;
        return _isCounterTerrorist ? TeamTypes.CounterTerrorist : TeamTypes.Terrorist;
    }
}