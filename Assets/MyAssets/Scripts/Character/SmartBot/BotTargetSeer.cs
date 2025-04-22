using System;
using System.Collections.Generic;
using UnityEngine;

public class BotTargetSeer
{
    private const float MaxRayDistance = 100f;

    private readonly Transform _eyes;
    private readonly LayerMask _layerMask;
    private readonly HashSet<Collider> _selfColliders;
    private readonly RaycastHit[] _raycastBuffer = new RaycastHit[8];

    public BotTargetSeer(Character selfCharacter, Transform eyes, LayerMask layerMask)
    {
        _eyes = eyes;
        _layerMask = layerMask;
        _selfColliders = selfCharacter.Colliders;

        if (_selfColliders == null || _selfColliders.Count == 0)
            throw new ArgumentNullException($"У игрока {selfCharacter.Name} коллайдеры отсутствуют");
    }

    public bool TrySeeEnemy(List<Character> enemies, out Character nearestTarget)
    {
        nearestTarget = null;
        float distance = float.MaxValue;

        foreach (Character enemy in enemies)
        {
            if (enemy.Center == null)
                throw new NullReferenceException($"Метод TrySeeEnemy: enemy.Center равен null, {enemy.gameObject.name}");

            Vector3 direction = (enemy.Center.position - _eyes.position).normalized;

            int hitsCount = Physics.RaycastNonAlloc(
                new Ray(_eyes.position, direction), 
                _raycastBuffer, 
                MaxRayDistance, 
                _layerMask);

            Array.Sort(_raycastBuffer, 0, hitsCount, new RaycastHitComparer());

            bool targetFound = false;
            Body body = null;

            for (int i = 0; i < hitsCount; i++)
            {
                if (Utils.IsSelfColliders(_selfColliders, _raycastBuffer[i].collider))
                    continue;

                if (_raycastBuffer[i].collider.TryGetComponent(out body))
                    targetFound = true;

                break;
            }

            if (targetFound && Vector3.Distance(_eyes.position, enemy.transform.position) < distance)
                nearestTarget = enemy;
        }

        return nearestTarget != null;
    }

    private class RaycastHitComparer : IComparer<RaycastHit>
    {
        public int Compare(RaycastHit x, RaycastHit y) =>
            x.distance.CompareTo(y.distance);
    }
}