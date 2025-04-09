using System;
using UnityEngine;

public class BotTargetSwitcher
{
    private const float Deviation = 0.1f;

    private readonly Transform _transform;
    private readonly Transform[] _points;
    private int _targetId = 0;

    public event Action TargetAchieved;

    public BotTargetSwitcher(Transform transform, Transform[] points)
    {
        _transform = transform;
        _points = points;
    }

    public Transform GetCurrentTarget => _points[_targetId];

    public Vector3 GetCurrentTargetPosition()
    {
        if (IsNearCurrentTarget())
        {
            NextTarget();
            TargetAchieved?.Invoke();
        }

        return _points[_targetId].position;
    }

    private bool IsNearCurrentTarget()
    {
        Vector3 currentPosition = ResetHeight(_transform.position);
        Vector3 targetPosition = ResetHeight(_points[_targetId].position);

        return (currentPosition - targetPosition).sqrMagnitude < Deviation;
    }

    private void NextTarget() =>
        _targetId = (_targetId + 1) % _points.Length;

    private Vector3 ResetHeight(Vector3 value)
    {
        value.y = 0;

        return value;
    }
}