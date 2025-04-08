using UnityEngine;

public class VerticalRotator
{
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
        _currentAngle = Mathf.Clamp(_currentAngle, DataParams.Character.MinimumVerticalRotationAngle, DataParams.Character.MaximumVerticalRotationAngle);
        Quaternion rotation = Quaternion.Euler(_currentAngle, _transform.eulerAngles.y, 0);
        _transform.rotation = rotation;
    }
}