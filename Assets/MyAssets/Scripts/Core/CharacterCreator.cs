using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterCreator : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private SmartBot _smartBotPrefab;
    [SerializeField] private PointFolder _folder;
    [SerializeField] private int _countPlayers;

    private List<SpawnPoint> _points;
    private Pool<SmartBot> _smartBotPool;

    private void Awake()
    {
        _points = _folder.transform.GetComponentsInChildren<SpawnPoint>(true).ToList();
        _smartBotPool = new(_smartBotPrefab, transform);
    }

    private void Start()
    {
        List<SpawnPoint> tempPoints = new(_points);

        SetPosition(tempPoints, _player);

        for (int i = 0; i < _countPlayers - 1; i++)
        {
            if (tempPoints.Count == 0)
                break;

            if (_smartBotPool.TryGet(out SmartBot smartBot))
                SetPosition(tempPoints, smartBot);
        }
    }

    private void SetPosition(List<SpawnPoint> tempPoints, Character character)
    {
        int id = Random.Range(0, tempPoints.Count);
        SpawnPoint point = tempPoints[id];
        tempPoints.Remove(point);
        character.transform.SetPositionAndRotation(point.transform.position, point.transform.rotation);
    }
}