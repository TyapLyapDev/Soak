using UnityEngine;

public class VerticalRotator
{
    private const float MinimumVerticalAngle = -90;
    private const float MaximumVerticalAngle = 90;

    private readonly Transform _transform;
    private readonly float _speed;
    private float _currentAngle;

    public VerticalRotator(Transform transform, float speed)
    {
        _transform = transform;
        _speed = speed;
        _currentAngle = _transform.eulerAngles.x;
    }

    public void Rotate(float value)
    {
        _currentAngle -= value * _speed * Time.deltaTime;
        _currentAngle = Mathf.Clamp(_currentAngle, MinimumVerticalAngle, MaximumVerticalAngle);
        Quaternion rotation = Quaternion.Euler(_currentAngle, _transform.eulerAngles.y, 0);
        _transform.rotation = rotation;
    }
}