using System;
using UnityEngine;

public class BotRotatorToTarget
{
    private readonly Transform _horizontalTransform;
    private readonly Transform _verticalTransform;
    private readonly Vector2 _speedLimits = new(2f, 8f);
    private readonly Vector2 _durationLimits = new(2f, 4f);

    private Vector3 _targetPosition;
    private float _lookTimer;
    private float _duration;
    private float _currentSpeed;

    public event Action NoTarget;

    public BotRotatorToTarget(Transform horizontalTransform, Transform verticalTtransform)
    {
        _horizontalTransform = horizontalTransform;
        _verticalTransform = verticalTtransform;
    }

    public void UpdateTargetPosition(Vector3 targetPosition) =>
        _targetPosition = targetPosition;

    public void UpdateRotation()
    {
        RotateHorizontalTowards();
        RotateVerticalTowards();

        _lookTimer += Time.deltaTime;

        if (_lookTimer >= _duration)
            StartRotation();
    }

    private void StartRotation()
    {
        _lookTimer = 0f;
        _currentSpeed = UnityEngine.Random.Range(_speedLimits.x, _speedLimits.y);
        _duration = UnityEngine.Random.Range(_durationLimits.x, _durationLimits.y);
    }

    private void RotateHorizontalTowards()
    {
        Vector3 direction = _targetPosition - _horizontalTransform.position;
        direction.y = 0;

        if (direction == Vector3.zero) 
            return;

        Quaternion targetRotation = Quaternion.LookRotation(direction);
        _horizontalTransform.rotation = Quaternion.Slerp(_horizontalTransform.rotation, targetRotation, _currentSpeed * Time.deltaTime);
    }

    private void RotateVerticalTowards()
    {
        Vector3 direction = _targetPosition - _verticalTransform.transform.position;
        float angle = Vector3.SignedAngle(_horizontalTransform.forward, direction, _horizontalTransform.right);
        angle = Mathf.Clamp(angle, DataParams.Character.MinimumVerticalRotationAngle, DataParams.Character.MaximumVerticalRotationAngle);
        Quaternion targetRotation = Quaternion.Euler(angle, 0, 0);

        _verticalTransform.transform.localRotation = Quaternion.Slerp(_verticalTransform.transform.localRotation, targetRotation, _currentSpeed * Time.deltaTime);
    }
}