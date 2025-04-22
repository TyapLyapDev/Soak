using System;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleDetector
{
    private const float MovementRayDistance = 1.2f;
    private const float MovementRayVerticalOffset = 1f;
    private const float JumpRayDistance = 0.9f;
    private const float JumpRayVerticalOffset = 0.8f;
    private const float JumpSlopeAngle = -0.45f;

    private readonly Transform _transform;
    private readonly HashSet<Body> _bodiesSet;
    private readonly RaycastHit[] _raycastBuffer = new RaycastHit[8];

    private Ray _movementRay;
    private Ray _jumpRay;

    public event Action JumpOpened;

    public ObstacleDetector(Transform transform)
    {
        _transform = transform;
        Body[] bodies = _transform.GetComponentsInChildren<Body>(true);
        _bodiesSet = new HashSet<Body>(bodies);
    }

    public bool IsCanMovement(Vector2 direction)
    {
        Vector3 localDirection = CalculateWorldDirection(direction);
        Vector3 rayOrigin = GetMovementRayOrigin();

        bool hasObstacle = IsMovementObstacle(rayOrigin, localDirection);
        Color color = hasObstacle ? Color.red : Color.yellow;

        DrawDebug(rayOrigin, localDirection * MovementRayDistance, color);

        if (hasObstacle) 
            return false;

        if (IsPossibilityJump(localDirection))
            JumpOpened?.Invoke();

        return true;
    }

    private bool IsMovementObstacle(Vector3 origin, Vector3 direction)
    {
        _movementRay = new Ray(origin, direction);
        int hitsCount = Physics.RaycastNonAlloc(_movementRay, _raycastBuffer, MovementRayDistance);

        for (int i = 0; i < hitsCount; i++)
        {
            if (IsSelfBody(_raycastBuffer[i].collider)) 
                continue;

            return true;
        }

        return false;
    }

    private bool IsPossibilityJump(Vector3 baseDirection)
    {
        Vector3 rayDirection = new (baseDirection.x, JumpSlopeAngle, baseDirection.z);
        Vector3 rayOrigin = _transform.position + Vector3.up * JumpRayVerticalOffset;

        _jumpRay = new Ray(rayOrigin, rayDirection);

        int hitsCount = Physics.RaycastNonAlloc(_jumpRay, _raycastBuffer, JumpRayDistance);
        bool canJump = false;

        for (int i = 0; i < hitsCount; i++)
        {
            if (IsSelfBody(_raycastBuffer[i].collider)) 
                continue;

            canJump = true;

            break;
        }

        Color color = canJump ? Color.green : Color.blue;
        DrawDebug(rayOrigin, rayDirection * JumpRayDistance, color);

        return canJump;
    }

    private bool IsSelfBody(Collider collider)
    {
        Body body = collider.GetComponent<Body>();

        return body != null && _bodiesSet.Contains(body);
    }

    private Vector3 CalculateWorldDirection(Vector2 inputDirection) =>
        _transform.TransformDirection(new(inputDirection.x, 0, inputDirection.y));

    private Vector3 GetMovementRayOrigin() =>
        _transform.position + Vector3.up * MovementRayVerticalOffset;

    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    private void DrawDebug(Vector3 start, Vector3 direction, Color color) =>
        Debug.DrawLine(start, start + direction, color);
}