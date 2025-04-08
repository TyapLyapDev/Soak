using UnityEngine;

public class VerticalRotator
{
    private const float MinimumVerticalAngle = -90;
    private const float MaximumVerticalAngle = 90;

    private readonly Transform _transform;
    private float _currentAngle;

    public VerticalRotator(Transform transform)
    {
        _transform = transform;
        _currentAngle = _transform.eulerAngles.x;
    }

    public void Rotate(float value)
    {
        _currentAngle -= value;
        _currentAngle = Mathf.Clamp(_currentAngle, MinimumVerticalAngle, MaximumVerticalAngle);
        Quaternion rotation = Quaternion.Euler(_currentAngle, _transform.eulerAngles.y, 0);
        _transform.rotation = rotation;
    }
}