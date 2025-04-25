using System.Collections.Generic;
using UnityEngine;

public class RagdollHandler
{
    private List<Rigidbody> _bodies;

    public RagdollHandler(Transform transform)
    {
        _bodies = new List<Rigidbody>(transform.GetComponentsInChildren<Rigidbody>(true));
        Disable();
    }

    public void Enable()
    {
        foreach (Rigidbody rigidbody in _bodies)
        {
            rigidbody.useGravity = !DataParams.SaveOptions.IsGravigravitationalAnomaliesChecked;
            rigidbody.isKinematic = false;
        }
    }

    public void Disable()
    {
        foreach (Rigidbody rigidbody in _bodies)
        {            
            rigidbody.useGravity = false;
            rigidbody.isKinematic = true;
        }
    }
}