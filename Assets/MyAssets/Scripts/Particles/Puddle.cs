using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class Puddle : MonoBehaviour, IDeactivatable<Puddle>
{
    private ParticleSystem _puddle;

    public event Action<Puddle> Deactivated;

    private void Awake()
    {
        _puddle = GetComponent<ParticleSystem>();
        Configure();
    }

    private void Configure()
    {
        ParticleSystem.MainModule module = _puddle.main;
        module.loop = false;
    }

    private void OnEnable() =>
        StartCoroutine(DeactivateOverTime());

    public void Deactivate() =>
        Deactivated?.Invoke(this);

    private IEnumerator DeactivateOverTime()
    {
        _puddle.Play();

        while (_puddle.isPlaying)
            yield return null;

        Deactivate();
    }
}