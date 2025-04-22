using System;
using System.Collections.Generic;
using UnityEngine;

public class BotVisibilityEnemyChecker
{
    private const float MaxRayDistance = 100f;

    private readonly Transform _eyes;
    private readonly LayerMask _layerMask;
    private readonly HashSet<Body> _selfBodiesSet;
    private Ray _ray;
    private readonly RaycastHit[] _raycastBuffer = new RaycastHit[8];

    public BotVisibilityEnemyChecker(Character selfCharacter, Transform eyes, LayerMask layerMask)
    {
        _eyes = eyes;
        _layerMask = layerMask;
        _selfBodiesSet = new HashSet<Body>(selfCharacter.transform.GetComponentsInChildren<Body>(true));
    }

    public bool TrySeeEnemy(Character enemyTarget)
    {
        HashSet<Collider> colliders = enemyTarget.Colliders;

        foreach (Collider collider in colliders)
        {
            Vector3 direction = (collider.bounds.center - _eyes.position).normalized;
            _ray = new Ray(_eyes.position, direction);

            int hitsCount = Physics.RaycastNonAlloc(_ray, _raycastBuffer, MaxRayDistance, _layerMask);

            Array.Sort(_raycastBuffer, 0, hitsCount, new RaycastHitComparer());


            for (int i = 0; i < hitsCount; i++)
            {
                if (IsSelfBody(_raycastBuffer[i].collider))
                    continue;

                if (_raycastBuffer[i].collider.TryGetComponent(out Body _))
                    return true;
            }

            return false;
        }

        return false;
    }

    private bool IsSelfBody(Collider collider)
    {
        if (collider == null)
            return false;

        return collider.TryGetComponent(out Body body) && _selfBodiesSet.Contains(body);
    }

    private class RaycastHitComparer : IComparer<RaycastHit>
    {
        public int Compare(RaycastHit x, RaycastHit y) =>
            x.distance.CompareTo(y.distance);
    }
}