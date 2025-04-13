using UnityEngine;

public static class Utils
{
    public static Vector3 ResetHeight(Vector3 value)
    {
        value.y = 0;

        return value;
    }

    public static Vector2 NormalizeToMoveOnPlane(Transform transform, Vector3 targetPosition)
    {
        Vector3 normalizeDirection = (targetPosition - transform.position).normalized;
        normalizeDirection.y = 0;

        if (normalizeDirection.magnitude > 0.01f)
            normalizeDirection = transform.InverseTransformDirection(normalizeDirection.normalized);

        Vector2 directionToMove = new(normalizeDirection.x, normalizeDirection.z);

        return directionToMove;
    }
}