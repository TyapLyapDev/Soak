using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterPositionAssigner
{
    private readonly Dictionary<TeamType, List<SpawnPoint>> _teamPoints = new();
    private Dictionary<TeamType, List<SpawnPoint>> _availablePoints;

    public CharacterPositionAssigner(Transform folderPoints)
    {
        if (folderPoints == null)
            throw new ArgumentNullException(nameof(folderPoints), "Попытка найти точки спавна в несуществующей папке");

        _teamPoints[TeamType.Terrorist] = folderPoints
            .GetComponentsInChildren<TerroristPointSpawn>(true)
            .Cast<SpawnPoint>()
            .ToList();

        _teamPoints[TeamType.CounterTerrorist] = folderPoints
            .GetComponentsInChildren<CounterTerroristPointSpawn>(true)
            .Cast<SpawnPoint>()
            .ToList();

        _teamPoints[TeamType.AgainstEveryone] = folderPoints
            .GetComponentsInChildren<NoTeamPointSpawn>(true)
            .Cast<SpawnPoint>()
            .ToList();

        ResetPoints();
    }

    public int PointsCount => _teamPoints.Values.Sum(list => list.Count);

    public void ResetPoints()
    {
        _availablePoints = _teamPoints.ToDictionary(
            kvp => kvp.Key,
            kvp => new List<SpawnPoint>(kvp.Value)
        );
    }

    public void SetPosition(Character character)
    {
        if (character == null)
            throw new ArgumentNullException(nameof(character), "Попытка установить позицию для несуществующего Character");

        var team = character.Team;
        var points = GetAvailablePoints(team);

        if (TryGetRandomPoint(points, out SpawnPoint point))
        {
            SetCharacterPosition(point, character);
            RemoveUsedPoint(team, point);

            return;
        }

        SetFallbackPosition(character);
    }

    private List<SpawnPoint> GetAvailablePoints(TeamType team)
    {
        return _availablePoints.TryGetValue(team, out var points)
            ? points
            : new List<SpawnPoint>();
    }

    private bool TryGetRandomPoint(List<SpawnPoint> points, out SpawnPoint point)
    {
        point = null;
        if (points.Count == 0) return false;

        int index = UnityEngine.Random.Range(0, points.Count);
        point = points[index];

        return true;
    }

    private void SetCharacterPosition(SpawnPoint point, Character character)
    {
        character.Controller.enabled = false;
        character.transform.SetPositionAndRotation(point.transform.position, point.transform.rotation);
        character.Controller.enabled = true;
    }

    private void RemoveUsedPoint(TeamType team, SpawnPoint point) =>
        _availablePoints[team].Remove(point);

    private void SetFallbackPosition(Character character)
    {
        var allPoints = _availablePoints.Values.SelectMany(list => list).ToList();
        if (allPoints.Count == 0) allPoints = _teamPoints.Values.SelectMany(list => list).ToList();

        if (TryGetRandomPoint(allPoints, out SpawnPoint point))
        {
            SetCharacterPosition(point, character);
            character.Kill();

            return;
        }

        throw new InvalidOperationException("Нет доступных точек спавна");
    }
}