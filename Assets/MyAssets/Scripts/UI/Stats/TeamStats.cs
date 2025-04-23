using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TeamStats : MonoBehaviour
{
    [SerializeField] private TeamStatsView _view;
    [SerializeField] private CharacterStats _statsPrefab;
    [SerializeField] private Transform _content;

    private TeamType _teamType;
    private readonly Dictionary<Character, CharacterStats> _characterViews = new();

    public int CountCharacters => _characterViews.Count;

    private void Awake()
    {
        foreach (Transform child in _content)
            Destroy(child.gameObject);
    }

    public void Initialize(TeamType teamType, int winCount)
    {
        _teamType = teamType;
        UpdateWinCount(winCount);
        UpdateTeamName();
        _view.UpdateColor(teamType);
    }

    public void AddCharacter(Character character)
    {
        if (_characterViews.ContainsKey(character)) return;

        var view = Instantiate(_statsPrefab, _content);
        view.Initialize(character);
        _characterViews.Add(character, view);
        UpdateSorting();
        UpdateTeamName();
    }

    public void RemoveCharacter(Character character)
    {
        if (_characterViews.TryGetValue(character, out var view) == false)
            return;

        Destroy(view.gameObject);
        _characterViews.Remove(character);
        UpdateTeamName();
    }

    public void UpdateWinCount(int count) =>
        _view.UpdateWinCount(count);

    public void UpdateSorting()
    {
        var sorted = _characterViews.Values
            .OrderByDescending(v => v.Character.CountKill)
            .ThenBy(v => v.Character.CountDeath)
            .ToList();

        for (int i = 0; i < sorted.Count; i++)
            sorted[i].transform.SetSiblingIndex(i);
    }

    private void UpdateTeamName() =>
        _view.UpdateTeamName(_teamType, _characterViews.Count);    
}