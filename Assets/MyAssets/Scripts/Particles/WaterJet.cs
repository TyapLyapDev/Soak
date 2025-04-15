using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class WaterJet : MonoBehaviour
{
    private ParticleSystem _jet;
    private List<ParticleCollisionEvent> _collisionEvents = new(1);

    public event Action<Vector3, Quaternion> Collided;

    private void Awake() =>
        _jet = GetComponent<ParticleSystem>();

    private void OnParticleCollision(GameObject other)
    {
        int countEvents = ParticlePhysicsExtensions.GetCollisionEvents(_jet, other, _collisionEvents);

        if (countEvents > 0)
        {
            Vector3 position = _collisionEvents[0].intersection;
            Quaternion rotation = Quaternion.LookRotation(_collisionEvents[0].normal);

            Collided?.Invoke(position, rotation);            
        }

        //for (int i = 0; i < countEvents; i++)
        //{
        //    Vector3 position = _collisionEvents[i].intersection;
        //    Quaternion rotation = Quaternion.LookRotation(_collisionEvents[i].normal);

        //    if (_sprayPool.TryGet(out Spray spray))
        //        spray.transform.SetPositionAndRotation(position, rotation);
        //}
    }
}