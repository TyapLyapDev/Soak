using System;
using System.Collections.Generic;
using UnityEngine;

public class SightAdjuster
{
    private const float MaxRayDistance = 100f;

    private readonly Transform _eyes;
    private readonly Transform _jet;
    private readonly Rutine _rutine;
    private readonly HashSet<Collider> _selfColliders;
    private readonly LayerMask _layerMask;
    private readonly Quaternion _initRotation;
    private readonly RaycastHit[] _raycastBuffer = new RaycastHit[8];

    public SightAdjuster(Transform eyes, Transform waterJet, LayerMask layerMask, HashSet<Collider> colliders)
    {
        _eyes = eyes;
        _jet = waterJet;
        _layerMask = layerMask;
        _selfColliders = colliders;
        _initRotation = waterJet.transform.localRotation;
        _rutine = new(waterJet, UpdateRay);
    }

    public void Start() =>
        _rutine.Start();

    public void Stop()
    {
        _rutine.Stop();
        ResetJetRotation();
    }

    private void UpdateRay()
    {
        int hitsCount = Physics.RaycastNonAlloc(
            new(_eyes.position, _eyes.forward), 
            _raycastBuffer, 
            MaxRayDistance, 
            _layerMask);

        Array.Sort(_raycastBuffer, 0, hitsCount, new RaycastHitComparer());

        for (int i = 0; i < hitsCount; i++)
        {
            if (Utils.IsSelfColliders(_selfColliders, _raycastBuffer[i].collider))
                continue;

            UpdateJetTarget(_raycastBuffer[i].point);

            return;
        }

        ResetJetRotation();
    }

    private void UpdateJetTarget(Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - _jet.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        _jet.rotation = lookRotation;
    }

    private void ResetJetRotation() =>
        _jet.localRotation = _initRotation;

    private class RaycastHitComparer : IComparer<RaycastHit>
    {
        public int Compare(RaycastHit x, RaycastHit y) =>
            x.distance.CompareTo(y.distance);
    }
}