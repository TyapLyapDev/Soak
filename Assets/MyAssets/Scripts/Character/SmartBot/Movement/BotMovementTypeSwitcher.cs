using System;
using UnityEngine;

public enum MovementType
{
    Standing,
    Sitting,
    Walking,
    Running,
    Sneacking,
}

public class BotMovementTypeSwitcher
{
    private readonly Rutine _rutine;
    private readonly Vector2 _durationLimits = new(2f, 5f);
    private MovementType _currentType;
    private float _timer;
    private float _duration;

    public event Action Switched;

    public BotMovementTypeSwitcher(MonoBehaviour mono)
    {
        _rutine = new(mono, UpdateInfo);

        SetNewRandomType();
        _rutine.Start();
    }

    public MovementType GetCurrentType => _currentType;

    private void UpdateInfo()
    {
        _timer += Time.deltaTime;

        if (_timer >= _duration)
        {
            _timer = 0f;
            _duration = UnityEngine.Random.Range(_durationLimits.x, _durationLimits.y);
            SetNewRandomType();
        }
    }

    private void SetNewRandomType()
    {
        _currentType = (MovementType)UnityEngine.Random.Range(0, System.Enum.GetValues(typeof(MovementType)).Length);
        Switched?.Invoke();
    }
}