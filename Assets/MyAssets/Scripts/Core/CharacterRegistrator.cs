using System;
using System.Collections.Generic;
using System.Linq;

public class CharacterRegistrator
{
    private readonly Dictionary<TeamType, List<Character>> _teams = new();
    private readonly List<Character> _allCharacters = new();

    private readonly Action<Character> _died;

    public event Action<Character> Registered;
    public event Action<Character> Deregistered;

    public CharacterRegistrator(Action<Character> died)
    {
        _died = died;

        foreach (TeamType team in Enum.GetValues(typeof(TeamType)))
            _teams[team] = new List<Character>();
    }

    public int AllCount => _allCharacters.Count;

    public int GetTeamCount(TeamType team) => _teams[team].Count;

    public List<Character> Characters => _allCharacters;

    public void Register(Character character)
    {
        _allCharacters.Add(character);
        _teams[character.Team].Add(character);
        character.Init(_allCharacters);

        character.Died += _died;

        Registered?.Invoke(character);
    }

    public void Deregister(Character character)
    {
        _allCharacters.Remove(character);
        _teams[character.Team].Remove(character);

        character.Died -= _died;

        Deregistered?.Invoke(character);
    }

    public void ChangeTeam(Character character, TeamType oldTeam, TeamType newTeam)
    {
        _teams[oldTeam].Remove(character);
        _teams[newTeam].Add(character);
    }

    public bool IsEveryoneDead() =>
        _allCharacters.TrueForAll(c => c.IsDead);

    public bool IsTeamDead(TeamType team)
    {
        bool isCurrentTeamDeath = _teams[team].TrueForAll(c => c.IsDead);
        bool isNoTeamDeath = _teams[TeamType.AgainstEveryone].TrueForAll(c => c.IsDead);

        return isCurrentTeamDeath && isNoTeamDeath;
    }

    public bool IsOnlyOneLeftInNoTeam(out Character aliveCharacter)
    {
        aliveCharacter = null;

        var aliveInNoTeam = _teams[TeamType.AgainstEveryone].Where(c => !c.IsDead).ToList();

        bool isValid = aliveInNoTeam.Count == 1
                   && _teams[TeamType.CounterTerrorist].All(c => c.IsDead)
                   && _teams[TeamType.Terrorist].All(c => c.IsDead);

        if (isValid)
            aliveCharacter = aliveInNoTeam.First();

        return isValid;
    }
}