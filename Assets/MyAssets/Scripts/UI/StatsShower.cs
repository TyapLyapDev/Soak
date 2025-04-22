using System;
using UnityEngine;

public class StatsShower : MonoBehaviour
{
    [SerializeField] private InputInformer _informer;
    [SerializeField] private WindowStats _window;

    private void Awake() =>
        _window.gameObject.SetActive(false);

    private void OnEnable()
    {
        _informer.StatsPressed += OnStatsPressed;
        _informer.StatsUnpressed += OnStatsUnpressed;
    }

    private void OnDisable()
    {
        _informer.StatsPressed -= OnStatsPressed;
        _informer.StatsUnpressed -= OnStatsUnpressed;
    }

    private void OnStatsPressed() =>
        _window.gameObject.SetActive(true);

    private void OnStatsUnpressed() =>
        _window.gameObject.SetActive(false);
}