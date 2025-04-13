using System;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BotRotatorToTarget
{
    private readonly Transform _horizontal;
    private readonly Transform _vertical;

    private readonly Vector2 _speedLimits = new(2f, 8f);
    private readonly Vector2 _durationLimits = new(1f, 4f);

    private Transform _target;
    private float _lookTimer;
    private float _duration;
    private float _currentSpeed;

    public BotRotatorToTarget(Transform horizontal, Transform vertical)
    {
        _horizontal = horizontal;
        _vertical = vertical;

        SetRotationParams();
    }

    public void UpdateTarget(Transform target) =>
        _target = target;

    public void UpdateRotation()
    {
        if (_target == null)
            return;

        RotateHorizontal();
        RotateVerticalTowards();

        _lookTimer += Time.deltaTime;

        if (_lookTimer >= _duration)
            SetRotationParams();
    }

    private void SetRotationParams()
    {
        _lookTimer = 0f;
        _currentSpeed = UnityEngine.Random.Range(_speedLimits.x, _speedLimits.y);
        _duration = UnityEngine.Random.Range(_durationLimits.x, _durationLimits.y);
    }

    private void RotateHorizontal()
    {
        Vector3 direction = _target.position - _horizontal.position;
        direction.y = 0;

        if (direction == Vector3.zero)
            return;

        Quaternion targetRotation = Quaternion.LookRotation(direction);
        _horizontal.rotation = Quaternion.Slerp(_horizontal.rotation, targetRotation, _currentSpeed * Time.deltaTime);
    }

    private void RotateVerticalTowards()
    {
        Vector3 direction = _target.position - _vertical.transform.position;
        float angle = Vector3.SignedAngle(_horizontal.forward, direction, _horizontal.right);
        angle = Mathf.Clamp(angle, DataParams.Character.MinimumVerticalRotationAngle, DataParams.Character.MaximumVerticalRotationAngle);
        Quaternion targetRotation = Quaternion.Euler(angle, 0, 0);

        _vertical.transform.localRotation = Quaternion.Slerp(_vertical.transform.localRotation, targetRotation, _currentSpeed * Time.deltaTime);
    }
}