using System;
using System.Collections.Generic;
using UnityEngine;

public class WindowStats : MonoBehaviour
{
    [SerializeField] private CharacterManager _manager;
    [SerializeField] private TeamStats _teamViewPrefab;
    [SerializeField] private Transform _content;

    private readonly Dictionary<TeamType, TeamStats> _teamViews = new();

    private void Awake()
    {
        foreach (Transform child in _content)
            Destroy(child.gameObject);
    }

    private void Start()
    {
        if (_manager.Characters == null)
            throw new ArgumentNullException("Список игроков не был инициализирован");

        InitializeTeams();
        _manager.CharacterAdded += AddCharacterToTeam;
        _manager.CharacterRemoved += RemoveCharacterFromTeam;
        _manager.TeamWinChanged += UpdateTeamWinCount;
        _manager.Died += UpdateSorting;
        _manager.Killed += UpdateSorting;
    }

    private void OnDestroy()
    {
        _manager.CharacterAdded -= AddCharacterToTeam;
        _manager.CharacterRemoved -= RemoveCharacterFromTeam;
        _manager.TeamWinChanged -= UpdateTeamWinCount;
        _manager.Died -= UpdateSorting;
        _manager.Killed -= UpdateSorting;
        ClearTeams();
    }

    private void InitializeTeams()
    {
        ClearTeams();

        foreach (var character in _manager.Characters)
            AddCharacterToTeam(character);
    }

    private void AddCharacterToTeam(Character character)
    {
        var teamType = character.Team;

        if (_teamViews.TryGetValue(teamType, out TeamStats teamView) == false)
        {
            teamView = CreateTeamStats(teamType);
            _teamViews[teamType] = teamView;
        }

        teamView.AddCharacter(character);
    }

    private void RemoveCharacterFromTeam(Character character)
    {
        if (_teamViews.TryGetValue(character.Team, out TeamStats teamStats) == false)
            return;

        teamStats.RemoveCharacter(character);

        if (teamStats.CountCharacters == 0)
            DestroyTeamStats(character.Team);
    }

    private TeamStats CreateTeamStats(TeamType teamType)
    {
        var teamStats = Instantiate(_teamViewPrefab, _content);
        teamStats.Initialize(teamType, _manager.GetWinCount(teamType));

        return teamStats;
    }

    private void DestroyTeamStats(TeamType teamType)
    {
        if (_teamViews.TryGetValue(teamType, out var teamView) == false)
            return;

        Destroy(teamView.gameObject);
        _teamViews.Remove(teamType);
    }

    private void UpdateTeamWinCount(TeamType team, int count)
    {
        if (_teamViews.TryGetValue(team, out var teamView))
            teamView.UpdateWinCount(count);
    }

    private void ClearTeams()
    {
        foreach (var teamView in _teamViews.Values)
            Destroy(teamView.gameObject);

        _teamViews.Clear();
    }

    private void UpdateSorting(Character character)
    {
        if (_teamViews.TryGetValue(character.Team, out TeamStats teamStats) == false)
            return;

        teamStats.UpdateSorting();
    }
}