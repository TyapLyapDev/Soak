using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Body : MonoBehaviour
{
    public event Action<Character, float> DamageTaked;

    private Character _character;

    public void Init(Character character) =>
        _character = character;

    public Character Character => _character;

    public void TakeDamage(Character other, float value) =>
        DamageTaked?.Invoke(other, value);
}