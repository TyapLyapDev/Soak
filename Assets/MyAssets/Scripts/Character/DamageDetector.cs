using System;
using UnityEngine;

public class DamageDetector
{
    private readonly Body[] _bodyParts;

    public event Action<Character, float> DamageTaked;

    public DamageDetector(Transform characterTransform)
    {
        _bodyParts = characterTransform.GetComponentsInChildren<Body>(true);

        foreach (Body part in _bodyParts)
            part.Init(characterTransform.GetComponent<Character>());
    }

    public void Subscribe()
    {
        foreach (Body part in _bodyParts)
            part.DamageTaked += OnDamageTaked;
    }

    public void Unsubscribe()
    {
        foreach (Body part in _bodyParts)
            part.DamageTaked -= OnDamageTaked;
    }

    private void OnDamageTaked(Character other, float value) =>
        DamageTaked?.Invoke(other, value);
}