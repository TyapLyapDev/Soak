using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class Body : MonoBehaviour
{
    private const float Force = 3f;

    public event Action<Character, float> DamageTaked;

    private Character _character;
    private Rigidbody _rigidbody;

    private void Awake() =>
        _rigidbody = GetComponent<Rigidbody>();

    public void Init(Character character) =>
        _character = character;

    public Character Character => _character;

    public void TakeDamage(Character other, Vector3 forceDirection, float value)
    {
        DamageTaked?.Invoke(other, value);

        if(Character.IsDead)
            _rigidbody.AddForceAtPosition(forceDirection * Force, transform.position, ForceMode.Impulse);
    }
}