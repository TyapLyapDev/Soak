using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BotTargetsFinder
{
    private readonly Vector2 VerticalViewAngle = new(-60, 90);
    private readonly Vector2 HorizontalViewAngle = new(-60, 60);

    private readonly Transform _eyes;
    private readonly TeamTypes _team;
    private readonly List<Character> _allCharacters;

    public BotTargetsFinder(Transform eyes, TeamTypes team, List<Character> allCharacters)
    {
        _eyes = eyes;
        _team = team;
        _allCharacters = allCharacters;
    }

    public List<Character> GetTargetsSight()
    {
        List<Character> filteredCharacters = FilterFriends(_allCharacters);

        return FilterByAngle(filteredCharacters);
    }

    private List<Character> FilterFriends(List<Character> characters)
    {
        List<Character> onlyEnemy = new(characters);

        if (_team == TeamTypes.Terrorist || _team == TeamTypes.CounterTerrorist)
            onlyEnemy = new(onlyEnemy.Where(ch => ch.Team != _team && !ch.IsDeath).ToList());

        return onlyEnemy;
    }

    private List<Character> FilterByAngle(List<Character> enemies)
    {
        List<Character> visibleEnemies = new();

        foreach (var enemy in enemies)
        {
            Vector3 directionToEnemy = (enemy.transform.position - _eyes.position).normalized;

            float verticalAngle = Vector3.Angle(_eyes.forward, directionToEnemy);
            Vector3 projectedDir = Vector3.ProjectOnPlane(directionToEnemy, _eyes.up);
            float horizontalAngle = Vector3.SignedAngle(_eyes.forward, projectedDir, _eyes.up);

            if (Mathf.Abs(horizontalAngle) <= HorizontalViewAngle.y && verticalAngle <= VerticalViewAngle.y)
                visibleEnemies.Add(enemy);
        }

        return visibleEnemies;
    }
}