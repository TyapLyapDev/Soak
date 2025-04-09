using UnityEngine;

public class DirectionNormalizer
{
    private readonly Transform _transform;

    public DirectionNormalizer(Transform transform)
    {
        _transform = transform;
    }

    public Vector2 NormalizeToMoveOnPlane(Vector3 targetPosition)
    {
        Vector3 normalizeDirection = (targetPosition - _transform.position).normalized;
        normalizeDirection.y = 0;

        if (normalizeDirection.magnitude > 0.01f)
            normalizeDirection = _transform.InverseTransformDirection(normalizeDirection.normalized);

        Vector2 directionToMove = new(normalizeDirection.x, normalizeDirection.z);

        return directionToMove;
    }
}