using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterPositionAssigner
{
    private readonly List<SpawnPoint> _allPoints;
    private readonly List<SpawnPoint> _terroristPoints;
    private readonly List<SpawnPoint> _counterTerroristPoints;

    private List<SpawnPoint> _remainingAvailablePoints;
    private List<SpawnPoint> _remainingAvailableTerroristPoints;
    private List<SpawnPoint> _remainingAvailableCounterTerroristPoints;

    public CharacterPositionAssigner(Transform folderPoints)
    {
        if (folderPoints == null)
            throw new ArgumentNullException(nameof(folderPoints), "Попытка найти точки спавна в несуществующей папке");

        _allPoints = folderPoints.transform.GetComponentsInChildren<SpawnPoint>(true).ToList();
        _terroristPoints = _allPoints.Where(p => p is TerroristPointSpawn).ToList();
        _counterTerroristPoints = _allPoints.Where(p => p is CounterTerroristPointSpawn).ToList();
        ResetPoints();
    }

    public int PointsCount => _allPoints.Count;

    public void ResetPoints()
    {
        _remainingAvailablePoints = new(_allPoints);
        _remainingAvailableTerroristPoints = new(_terroristPoints);
        _remainingAvailableCounterTerroristPoints = new(_counterTerroristPoints);
    }

    public void SetPosition(Character character)
    {
        if (character == null)
            throw new ArgumentNullException(nameof(character), "Попытка установить позицию для несуществующего Character");

        List<SpawnPoint> pointsToUse = character.Team switch
        {
            TeamType.CounterTerrorist => _remainingAvailableCounterTerroristPoints,
            TeamType.Terrorist => _remainingAvailableTerroristPoints,
            _ => _remainingAvailablePoints,
        };

        SetPosition(pointsToUse, character);
    }

    private void SetPosition(List<SpawnPoint> points, Character character)
    {
        bool isNonePoints = points.Count == 0;
        List<SpawnPoint> pointsToUse = isNonePoints ? _allPoints : points;

        SetPosition(pointsToUse, character, isNonePoints);

        if(isNonePoints)
            character.Kill();
    }

    private void SetPosition(List<SpawnPoint> points, Character character, bool isNonePoints)
    {
        int index = UnityEngine.Random.Range(0, points.Count);
        SpawnPoint point = points[index];
        character.transform.SetPositionAndRotation(point.transform.position, point.transform.rotation);

        if (isNonePoints)
            return;

        points.RemoveAt(index);

        if (points != _remainingAvailablePoints)
            _remainingAvailablePoints.Remove(point);
    }
}