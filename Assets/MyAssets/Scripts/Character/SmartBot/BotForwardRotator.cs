using UnityEngine;

public class BotForwardRotator
{
    private readonly Transform _horizontal;
    private readonly Transform _vertical;

    private readonly Vector2 _speedLimits = new(2f, 8f);
    private readonly Vector2 _durationLimits = new(1f, 4f);

    private Transform _target;
    private float _lookTimer;
    private float _duration;
    private float _currentSpeed;

    public BotForwardRotator(Transform horizontal, Transform vertical)
    {
        _horizontal = horizontal;
        _vertical = vertical;
    }

    public void StartRotation() =>
        _currentSpeed = Random.Range(_speedLimits.x, _speedLimits.y);

    public void UpdateTarget(Transform target) =>
        _target = target;

    public void UpdateRotation()
    {
        if (_target == null)
            return;

        RotateHorizontal();
        ResetVerticalRotation();

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

        if (_target.position == Vector3.zero)
            return;

        Quaternion targetRotation = Quaternion.LookRotation(direction);
        _horizontal.rotation = Quaternion.Slerp(_horizontal.rotation, targetRotation, _currentSpeed * Time.deltaTime);
    }

    private void ResetVerticalRotation() =>
        _vertical.localRotation = Quaternion.Slerp(_vertical.localRotation, Quaternion.identity, _currentSpeed * Time.deltaTime);
}