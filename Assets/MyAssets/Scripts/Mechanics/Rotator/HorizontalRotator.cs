using UnityEngine;

public class HorizontalRotator
{
    private readonly Transform _transform;
    private readonly float _speed;

    public HorizontalRotator(Transform transform, float speed)
    {
        _transform = transform;
        _speed = speed;
    }

    public void Rotate(float value)
    {
        Vector3 direction = _transform.eulerAngles;
        direction.y += value * _speed * Time.deltaTime;
        _transform.eulerAngles = direction;
    }
}