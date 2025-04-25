using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class WindowStats : MonoBehaviour
{
    [SerializeField] private CharacterManager _manager;
    [SerializeField] private TeamStats _teamViewPrefab;
    [SerializeField] private Transform _content;
    [SerializeField] private ScrollRect _layout;

    private static readonly List<TeamType> SortingOrder = new()
    {
        TeamType.Terrorist,
        TeamType.CounterTerrorist,
        TeamType.AgainstEveryone,
        TeamType.Observer
    };

    private readonly Dictionary<TeamType, TeamStats> _teamStats = new();
    private List<TeamStats> _cachedOrder = new();
    private VerticalLayoutGroup _verticalLayoutGroup;

    private void Awake()
    {
        foreach (Transform child in _content)
            Destroy(child.gameObject);

        _verticalLayoutGroup = _layout.content.GetComponent<VerticalLayoutGroup>();
    }

    private void Start()
    {
        if (_manager.Characters == null)
            throw new ArgumentNullException("Список игроков не был инициализирован");

        InitializeTeams();
        _manager.CharacterAdded += AddCharacterToTeam;
        _manager.CharacterRemoved += RemoveCharacterFromTeam;
        _manager.TeamWinChanged += UpdateTeamWinCount;
        _manager.NoneTeamCharacterWinChanged += UpdateCharacterWinAgainstEveryone;
        _manager.Died += UpdateSorting;
        _manager.Killed += UpdateSorting;
    }

    private void OnEnable() =>
        UpdateLayoutComponents();

    private void OnDestroy()
    {
        _manager.CharacterAdded -= AddCharacterToTeam;
        _manager.CharacterRemoved -= RemoveCharacterFromTeam;
        _manager.TeamWinChanged -= UpdateTeamWinCount;
        _manager.NoneTeamCharacterWinChanged -= UpdateCharacterWinAgainstEveryone;
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

        if (_teamStats.TryGetValue(teamType, out TeamStats teamView) == false)
        {
            teamView = CreateTeamStats(teamType);
            _teamStats[teamType] = teamView;
            SortTeamViews();
        }

        teamView.AddCharacter(character);

        UpdateLayoutComponents();
    }

    private void RemoveCharacterFromTeam(Character character)
    {
        if (_teamStats.TryGetValue(character.Team, out TeamStats teamStats) == false)
            throw new KeyNotFoundException($"Команда {character.Team} не найдена в списке");

        teamStats.RemoveCharacter(character);

        if (teamStats.CountCharacters == 0)
            DestroyTeamStats(character.Team);

        UpdateLayoutComponents();
    }

    private TeamStats CreateTeamStats(TeamType teamType)
    {
        var teamStats = Instantiate(_teamViewPrefab, _content);
        teamStats.Initialize(teamType, _manager.GetWinCount(teamType));

        return teamStats;
    }

    private void DestroyTeamStats(TeamType teamType)
    {
        if (_teamStats.TryGetValue(teamType, out var teamView) == false)
            return;

        Destroy(teamView.gameObject);
        _teamStats.Remove(teamType);
    }

    private void UpdateTeamWinCount(TeamType team, int count)
    {
        if (_teamStats.TryGetValue(team, out var teamView))
            teamView.UpdateWinCount(count);
    }

    private void UpdateCharacterWinAgainstEveryone(Character character)
    {
        if (_teamStats.TryGetValue(character.Team, out var teamView))
            teamView.UpdateWinCount(_manager.GetWinCount(character.Team));
    }

    private void ClearTeams()
    {
        foreach (var teamView in _teamStats.Values)
            Destroy(teamView.gameObject);

        _teamStats.Clear();
    }

    private void UpdateSorting(Character character)
    {
        if (_teamStats.TryGetValue(character.Team, out TeamStats teamStats) == false)
            throw new KeyNotFoundException($"Команда {character.Team} не найдена в списке");

        teamStats.UpdateSorting();
    }

    private void SortTeamViews()
    {
        List<TeamStats> teamList = new(_teamStats.Values.Count);

        foreach (TeamType teamType in SortingOrder)
            if (_teamStats.TryGetValue(teamType, out var team))
                teamList.Add(team);

        if (teamList.SequenceEqual(_cachedOrder))
            return;

        for (int i = 0; i < teamList.Count; i++)
            teamList[i].transform.SetSiblingIndex(i);

        _cachedOrder = teamList;
    }

    private void UpdateLayoutComponents()
    {
        if (_verticalLayoutGroup == null)
            return;

        LayoutRebuilder.ForceRebuildLayoutImmediate(_layout.content);
        Canvas.ForceUpdateCanvases();
        _verticalLayoutGroup.CalculateLayoutInputVertical();
        _verticalLayoutGroup.SetLayoutVertical();
    }
}