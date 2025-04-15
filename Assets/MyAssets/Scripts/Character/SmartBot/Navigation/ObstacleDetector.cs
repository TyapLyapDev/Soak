using System;
using UnityEngine;

public class ObstacleDetector
{
    private const float MovementRayDistance = 1.2f;
    private const float JumpRayDistance = 0.7f;

    private readonly Transform _transform;

    public event Action JumpOpened;

    public ObstacleDetector(Transform transform)
    {
        _transform = transform;
    }

    public bool IsCanMovement(Vector2 direction)
    {
        Vector3 localDirection = new(direction.x, 0, direction.y);
        localDirection = _transform.TransformDirection(localDirection);

        Vector3 rayOrigin = _transform.position;
        rayOrigin.y += 1f;

        if (Physics.Raycast(rayOrigin, localDirection, MovementRayDistance))
        {
            Debug.DrawLine(rayOrigin, rayOrigin + localDirection * MovementRayDistance, Color.red);

            return false;
        }

        if (IsPossibilityJump(localDirection))
            JumpOpened?.Invoke();

        Debug.DrawLine(rayOrigin, rayOrigin + localDirection * MovementRayDistance, Color.yellow);

        return true;
    }

    private bool IsPossibilityJump(Vector3 localDirection)
    {
        localDirection.y = -0.45f;
        Vector3 rayOrigin = _transform.position;
        rayOrigin.y += 0.8f;

        if (Physics.Raycast(rayOrigin, localDirection, JumpRayDistance))
        {
            Debug.DrawLine(rayOrigin, rayOrigin + localDirection * JumpRayDistance, Color.green);

            return true;
        }

        Debug.DrawLine(rayOrigin, rayOrigin + localDirection * JumpRayDistance, Color.blue);

        return false;
    }
}