using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class WaterJet : MonoBehaviour
{
    private const float DamageValue = 0.17f;

    private Character _character;

    private ParticleSystem _jet;
    private readonly List<ParticleCollisionEvent> _collisionEvents = new();

    public event Action<Vector3, Quaternion> Collided;

    private void Awake()
    {
        _jet = GetComponent<ParticleSystem>();
        _character = GetComponentInParent<Character>();
        Stop();
    }

    private void OnParticleCollision(GameObject other)
    {
        int countEvents = ParticlePhysicsExtensions.GetCollisionEvents(_jet, other, _collisionEvents);

        if (countEvents == 0)
            return;

        for (int i = 0; i < countEvents; i++)
            if (other.TryGetComponent(out Body body))
            {
                Vector3 forceDirection = (other.transform.position - transform.position).normalized;
                body.TakeDamage(_character, forceDirection, DamageValue);
            }

        ProcessCollision(_collisionEvents[0]);
    }

    public void Play() =>
        _jet.Play();

    public void Stop() =>
        _jet.Stop();

    private void ProcessCollision(ParticleCollisionEvent collisionEvent)
    {
        Vector3 position = collisionEvent.intersection;
        Quaternion rotation = Quaternion.LookRotation(collisionEvent.normal);
        Collided?.Invoke(position, rotation);
    }
}