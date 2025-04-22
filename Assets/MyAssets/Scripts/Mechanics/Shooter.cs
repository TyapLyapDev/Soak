using System.Collections.Generic;
using UnityEngine;

public class Shooter
{
    private readonly WaterJet _waterJet;
    private readonly SightAdjuster _adjuster;

    public Shooter(Transform eyes, WaterJet waterJet, LayerMask layerMask, HashSet<Collider> colliders)
    {
        _waterJet = waterJet;
        _adjuster = new(eyes, waterJet.transform, layerMask, colliders);
    }

    public void StartRay()
    {
        _adjuster.Start();
        _waterJet.Play();
    }

    public void StopRay()
    {
        _adjuster.Stop();
        _waterJet.Stop();        
    }
}