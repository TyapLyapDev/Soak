using UnityEngine;

public class HorizontalRotator
{
    private readonly Transform _transform;

    public HorizontalRotator(Transform transform)
    {
        _transform = transform;
    }

    public void Rotate(float value)
    {
        Vector3 direction = _transform.eulerAngles;
        direction.y += value;
        _transform.eulerAngles = direction;
    }
}