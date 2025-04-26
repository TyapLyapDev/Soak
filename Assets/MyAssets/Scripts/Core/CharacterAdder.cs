using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterAdder
{
    private readonly Character _counterTerroristBotPrefab;
    private readonly Character _terroristBotPrefab;
    private readonly Transform _map;
    private readonly List<Character> _bots = new();
    private readonly List<string> _names;
    private bool _isCounterTerrorist;

    public CharacterAdder(Character counterTerroristrefab, Character terroristrefab, Transform map)
    {
        _map = map;
        _counterTerroristBotPrefab = counterTerroristrefab;
        _terroristBotPrefab = terroristrefab;
        _names = DataParams.Texts.Names.ToList();
    }

    public int Count => _bots.Count;

    public void SetNextTeamBot(bool isCounterTerrorist) =>
        _isCounterTerrorist = isCounterTerrorist;

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
        TeamType team = GetTeamType();

        Character bot = team == TeamType.CounterTerrorist ?
            Object.Instantiate(_counterTerroristBotPrefab, _map) :
            Object.Instantiate(_terroristBotPrefab, _map);

        _bots.Add(bot);

        string name = GetName();

        bot.SetName(name);
        bot.SetTeam(team);

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
            if (bot.IsDead == false)
                bot.Kill();
    }

    private string GetName()
    {
        string name = _names[Random.Range(0, _names.Count)];
        _names.Remove(name);

        return name;
    }

    private TeamType GetTeamType()
    {
        if (DataParams.SaveOptions.TeamTypeIndex == 1)
            return TeamType.AgainstEveryone;

        _isCounterTerrorist = !_isCounterTerrorist;
        return _isCounterTerrorist ? TeamType.Terrorist : TeamType.CounterTerrorist;
    }
}