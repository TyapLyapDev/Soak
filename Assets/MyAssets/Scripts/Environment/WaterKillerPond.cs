using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class WaterKillerPond : MonoBehaviour
{
    [SerializeField] private float _buoyancyForce;
    [SerializeField] private float _waterHeight;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Body body) && body.Character.IsDead != true)
            body.Character.Kill();

        if(other.TryGetComponent(out Rigidbody rigidbody))
        {
            rigidbody.useGravity = false;
            StartCoroutine(ApplyBuoyancy(rigidbody));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Rigidbody rigidbody))
        {
            rigidbody.useGravity = true;
            StopCoroutine(ApplyBuoyancy(rigidbody));
        }
    }

    private IEnumerator ApplyBuoyancy(Rigidbody rigidbody)
    {
        bool isUnderwater = true;

        while (isUnderwater)
        {
            if (rigidbody.position.y < _waterHeight && rigidbody != null)
            {
                float depth = _waterHeight - rigidbody.position.y;
                Vector3 buoyancy = _buoyancyForce * depth * Vector3.up;
                rigidbody.AddForce(buoyancy, ForceMode.Acceleration);
            }
            else
            {
                isUnderwater = false;
            }

            if (rigidbody == null) 
                yield break;

            yield return null;
        }

        if (rigidbody != null)
            rigidbody.useGravity = true;
    }
}