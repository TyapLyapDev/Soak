using System;
using UnityEngine;

public class BotTargetSwitcher
{
    private const float Deviation = 0.1f;

    private readonly Rutine _rutine;
    private readonly Transform _transform;
    private readonly Transform[] _points;
    private int _targetId = 0;
    private Transform _currentTarget;

    public event Action Switched;

    public BotTargetSwitcher(Transform transform, Transform[] points)
    {
        _rutine = new(transform.GetComponent<MonoBehaviour>(), UpdateInfo);
        _transform = transform;
        _points = points;

        SetNewCurrentTarget();
        _rutine.Start();
    }

    public Transform Target => _currentTarget;

    public Vector2 GetDirectionToTarget()
    {
        Vector3 targetPosition = _currentTarget.position;
        Vector2 input = Utils.NormalizeToMoveOnPlane(_transform, targetPosition);

        return input;
    }

    private void UpdateInfo()
    {
        if (IsNearCurrentTarget() == false)
            return;

        NextTarget();
        Switched?.Invoke();
    }

    private bool IsNearCurrentTarget()
    {
        Vector3 currentPosition = Utils.ResetHeight(_transform.position);
        Vector3 targetPosition = Utils.ResetHeight(_currentTarget.position);

        return (currentPosition - targetPosition).sqrMagnitude < Deviation;
    }

    private void NextTarget()
    {
        _targetId = (_targetId + 1) % _points.Length;
        SetNewCurrentTarget();
    }

    private void SetNewCurrentTarget() =>
        _currentTarget = _points[_targetId];
}