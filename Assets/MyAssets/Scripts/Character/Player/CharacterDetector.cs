using System;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDetector
{
    private readonly Transform _eyes;
    private readonly HashSet<Collider> _selfColliders;
    private readonly Rutine _rutine;
    private LayerMask _layerMask;
    private readonly RaycastHit[] _raycastBuffer = new RaycastHit[8];

    public event Action<Character> Detected;
    public event Action Undetected;

    public CharacterDetector(Transform eyes, HashSet<Collider> selfColliders, LayerMask layerMask)
    {
        _eyes = eyes;
        _layerMask = layerMask;
        _selfColliders = selfColliders;
        _rutine = new(eyes, Update);
    }

    public void Start() =>
        _rutine.Start();

    public void Stop() => 
        _rutine.Stop();

    private void Update()
    {
        int hitsCount = Physics.RaycastNonAlloc(
            new Ray(_eyes.position, _eyes.forward), 
            _raycastBuffer, 
            DataParams.Character.MaximumRayDistance,
            _layerMask);

        Array.Sort(_raycastBuffer, 0, hitsCount, new RaycastHitComparer());

        for (int i = 0; i < hitsCount; i++)
        {
            if (Utils.IsSelfColliders(_selfColliders, _raycastBuffer[i].collider))
                continue;

            if (_raycastBuffer[i].collider.TryGetComponent(out Body body) && body.Character.IsDead == false)
                Detected?.Invoke(body.Character);
            else
                Undetected?.Invoke();

            return;
        }

        Undetected?.Invoke();
    }    

    private class RaycastHitComparer : IComparer<RaycastHit>
    {
        public int Compare(RaycastHit x, RaycastHit y) =>
            x.distance.CompareTo(y.distance);
    }
}