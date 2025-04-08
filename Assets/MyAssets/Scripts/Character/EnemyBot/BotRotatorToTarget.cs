using System;
using UnityEngine;

public class BotRotatorToTarget
{
    private readonly Transform _horizontalTransform;
    private readonly Transform _verticalTransform;
    private readonly Vector2 _verticalAngleLimits = new(-90f, 90f);
    private readonly Vector2 _speedLimits = new(2f, 8f);

    private Transform _currentTarget;
    private float _currentSpeed;

    public event Action NoTarget;

    public BotRotatorToTarget(Transform horizontalTransform, Transform verticalTtransform)
    {
        _horizontalTransform = horizontalTransform;
        _verticalTransform = verticalTtransform;
    }

    public void StartRotation(Transform target)
    {
        NewRandomSpeed();
        _currentTarget = target;
    }

    public void UpdateRotation()
    {
        RotateHorizontalTowards();
        RotateVerticalTowards();
    }

    private void NewRandomSpeed() =>
        _currentSpeed = UnityEngine.Random.Range(_speedLimits.x, _speedLimits.y);

    private void RotateHorizontalTowards()
    {
        if (_currentTarget == null)
        {
            NoTarget?.Invoke();
            return;
        }

        Vector3 direction = _currentTarget.position - _horizontalTransform.position;
        direction.y = 0;

        if (direction == Vector3.zero) 
            return;

        Quaternion targetRotation = Quaternion.LookRotation(direction);
        _horizontalTransform.rotation = Quaternion.Slerp(_horizontalTransform.rotation, targetRotation, _currentSpeed * Time.deltaTime);
    }

    private void RotateVerticalTowards()
    {
        if (_currentTarget == null)
        {
            NoTarget?.Invoke();
            return;
        }

        Vector3 direction = _currentTarget.position - _verticalTransform.transform.position;
        float angle = Vector3.SignedAngle(_horizontalTransform.forward, direction, _horizontalTransform.right);
        angle = Mathf.Clamp(angle, _verticalAngleLimits.x, _verticalAngleLimits.y);
        Quaternion targetRotation = Quaternion.Euler(angle, 0, 0);

        _verticalTransform.transform.localRotation = Quaternion.Slerp(_verticalTransform.transform.localRotation, targetRotation, _currentSpeed * Time.deltaTime);
    }
}