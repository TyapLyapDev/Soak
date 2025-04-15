using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class Spray : MonoBehaviour, IDeactivatable<Spray>
{
    private ParticleSystem _spray;

    public event Action<Spray> Deactivated;

    private void Awake()
    {
        _spray = GetComponent<ParticleSystem>();
        Configure();
    }

    private void Configure()
    {
        ParticleSystem.MainModule module = _spray.main;
        module.loop = false;
    }

    private void OnEnable() =>
        StartCoroutine(DeactivateOverTime());

    public void Deactivate() =>
        Deactivated?.Invoke(this);

    private IEnumerator DeactivateOverTime()
    {
        _spray.Play();

        while (_spray.isPlaying)
            yield return null;

        Deactivate();
    }
}