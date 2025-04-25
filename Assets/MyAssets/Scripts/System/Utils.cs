using System.Collections.Generic;
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

    public static bool IsSelfBodyParts(HashSet<Body> selfBodyParts, Collider collider)
    {
        if (collider == null)
            return false;

        return collider.TryGetComponent(out Body body) && selfBodyParts.Contains(body);
    }

    public static bool IsSelfColliders(HashSet<Collider> selfColliders, Collider collider)
    {
        if (collider == null)
            return false;

        return selfColliders.Contains(collider);
    }

    public static string GetTeamName(TeamType teamType)
    {
        return teamType switch
        {
            TeamType.Terrorist => DataParams.Texts.TeamTerroristsName,
            TeamType.CounterTerrorist => DataParams.Texts.TeamCounterTerroristsName,
            TeamType.AgainstEveryone => DataParams.Texts.TeamAgainstEveryoneName,
            TeamType.Observer => DataParams.Texts.TeamObserverName,
            _ => string.Empty
        };
    }
}