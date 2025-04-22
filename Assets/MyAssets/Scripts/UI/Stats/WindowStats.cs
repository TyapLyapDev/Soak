using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WindowStats : MonoBehaviour
{
    [SerializeField] private CharacterManager _manager;
    [SerializeField] private TeamView _teamViewPrefab;
    [SerializeField] private StatsLineView _statsLineViewPrefab;
    [SerializeField] private GameObject _content;

    private readonly Dictionary<string, StatsLineView> _statsLineViews = new();
    private readonly Dictionary<string, TeamView> _teamViews = new();

    private void ClearContent()
    {
        foreach (Transform child in _content.transform)
            Destroy(child.gameObject);

        _statsLineViews.Clear();
        _teamViews.Clear();
    }

    private void OnEnable()
    {
        HandleStats();
        _manager.Changed += HandleStats;
        _manager.HealthChanged += OnHealthChanged;
        _manager.TeamWinChanged += HandleStats;
    }

    private void OnDisable()
    {
        _manager.Changed -= HandleStats;
        _manager.HealthChanged -= OnHealthChanged;
        _manager.TeamWinChanged -= HandleStats;
    }

    private void HandleStats()
    {
        ClearContent();

        List<Character> characters = GetSortedCharacters();        

        UpdateTeamStats(characters, _manager.TerroristTeamCountWin, TeamTypes.Terrorist, DataParams.Character.TeamTerroristsName);
        UpdateTeamStats(characters, _manager.CounterTerroristTeamCountWin, TeamTypes.CounterTerrorist, DataParams.Character.TeamCounterTerroristName);
        UpdateTeamStats(characters, -1, TeamTypes.None, DataParams.Character.TeamNoName);
    }

    private List<Character> GetSortedCharacters()
    {
        return _manager.Characters
            .OrderByDescending(ch => ch.CountKill)
            .ThenBy(ch => ch.CountDeath)
            .ToList();
    }

    private void UpdateTeamStats(List<Character> characters, int teamCountWin, TeamTypes teamType, string teamName)
    {
        var teamCharacters = characters.Where(ch => ch.Team == teamType).ToList();

        if (teamCharacters.Count == 0)
            return;

        Color color = TeamColors.Instance.Get(teamType);

        UpdateTeamView(teamCharacters, teamCountWin, teamName, color);
        UpdateCharacterStats(teamCharacters, color);
    }

    private void UpdateTeamView(List<Character> teamCharacters, int countWin, string teamName, Color color)
    {
        if (_teamViews.ContainsKey(teamName) == false)
        {
            TeamView team = Instantiate(_teamViewPrefab, _content.transform);
            team.SetColor(color);
            team.SetTeamName(teamName, teamCharacters.Count);
            team.SetCountWin(countWin);
            _teamViews[teamName] = team;

            return;
        }

        _teamViews[teamName].SetTeamName(teamName, teamCharacters.Count);
    }

    private void UpdateCharacterStats(List<Character> teamCharacters, Color color)
    {
        foreach (Character character in teamCharacters)
        {
            if (!_statsLineViews.ContainsKey(character.Name))
                CreateCharacterStatsLine(character, color);
            else
                UpdateExistingCharacterStats(character);
        }
    }

    private void CreateCharacterStatsLine(Character character, Color color)
    {
        StatsLineView line = Instantiate(_statsLineViewPrefab, _content.transform);
        line.SetColor(color);
        line.SetStats(character.Name, character.IsDeath, character.Health, character.CountKill, character.CountDeath);
        _statsLineViews[character.Name] = line;
    }

    private void UpdateExistingCharacterStats(Character character)
    {
        var line = _statsLineViews[character.Name];
        line.SetStats(character.Name, character.IsDeath, character.Health, character.CountKill, character.CountDeath);
    }

    private void OnHealthChanged(Character character, float healthValue)
    {
        if (_statsLineViews.TryGetValue(character.Name, out StatsLineView line))
            line.SetHealth(healthValue);
    }
}