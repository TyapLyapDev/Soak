using System;
using System.Collections.Generic;

public class CharacterRegistrator
{
    private readonly List<Character> _characters = new();
    private readonly Action<Character> _died;
    private readonly Action<Character> _revived;

    public event Action<Character> Registered;
    public event Action<Character> Deregistered;

    public CharacterRegistrator(Action<Character> died)
    {
        _died = died;
    }

    public int Count => _characters.Count;

    public List<Character> Characters => _characters;

    public void Register(Character character)
    {
        _characters.Add(character);
        character.SetListCharacters(_characters);

        character.Died += _died;
        character.Revived += _revived;

        Registered?.Invoke(character);
    }

    public void Deregister(Character character)
    {
        _characters.Remove(character);
        character.Died -= _died;
        character.Revived -= _revived;

        Deregistered?.Invoke(character);
    }

    public bool IsEveryoneDead()
    {
        foreach (Character character in _characters)
            if (character.IsDeath == false)
                return false;

        return true;
    }

    public bool IsTeamDeath(TeamType team)
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
}