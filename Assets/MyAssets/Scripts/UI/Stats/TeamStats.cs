using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class TeamStats : MonoBehaviour
{
    [SerializeField] private TeamStatsView _view;
    [SerializeField] private CharacterStats _statsPrefab;
    [SerializeField] private Transform _content;

    private TeamType _teamType;
    private ContentSizeFitter _sizeFitter;
    private readonly Dictionary<Character, CharacterStats> _charactersStats = new();
    private List<CharacterStats> _lastSorted = new();

    public int CountCharacters => _charactersStats.Count;

    private void Awake()
    {
        foreach (Transform child in _content)
            Destroy(child.gameObject);

        _sizeFitter = _content.GetComponent<ContentSizeFitter>();
    }

    public void Initialize(TeamType teamType, int winCount)
    {
        _teamType = teamType;

        _view.UpdateColor(teamType);
        UpdateTeamHeader();
        UpdateWinCount(winCount);
    }

    public void AddCharacter(Character character)
    {
        if (_charactersStats.ContainsKey(character)) 
            return;

        CharacterStats characterStats = Instantiate(_statsPrefab, _content);
        characterStats.Initialize(character);
        _charactersStats.Add(character, characterStats);

        UpdateTeamHeader();
        UpdateSorting();
    }

    public void RemoveCharacter(Character character)
    {
        if (_charactersStats.TryGetValue(character, out var view) == false)
            return;

        Destroy(view.gameObject);
        _charactersStats.Remove(character);

        UpdateTeamHeader();
        UpdateSorting();
    }

    public void UpdateWinCount(int count)
    {
        if(_teamType == TeamType.Observer)
            _view.UpdateWinCount(-1);
        else
            _view.UpdateWinCount(count);
    }        

    public void UpdateSorting()
    {
        if (SortCharacters()) 
            UpdateLayout();
    }

    private void UpdateTeamHeader() =>
        _view.UpdateTeamHeader(_teamType, _charactersStats.Count);

    private bool SortCharacters()
    {
        var sorted = _charactersStats.Values
            .OrderByDescending(v => v.Character.CountKill)
            .ThenBy(v => v.Character.CountDeath)
            .ToList();

        if (sorted.SequenceEqual(_lastSorted)) return false;

        for (int i = 0; i < sorted.Count; i++)
            sorted[i].transform.SetSiblingIndex(i);

        _lastSorted = sorted;

        return true;
    }

    private void UpdateLayout()
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)_content);
        _sizeFitter.SetLayoutVertical();
    }
}