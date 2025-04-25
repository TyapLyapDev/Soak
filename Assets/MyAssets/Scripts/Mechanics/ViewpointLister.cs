using System;
using System.Collections;
using UnityEngine;

public class ViewpointLister : MonoBehaviour
{
    private const float DelayInSeconds = 6f;

    [SerializeField] private Transform _folderPoints;

    private StartViewPoint[] _points;
    private Transform _object;
    private int _index;

    private void Awake()
    {
        _object = Camera.main.transform;
        _points = _folderPoints.GetComponentsInChildren<StartViewPoint>();

        if (_points.Length == 0)
            throw new ArgumentNullException("В папке не найдены подходящие точки");
    }

    private void Start() =>
        StartCoroutine(UpdatingPosition());

    private IEnumerator UpdatingPosition()
    {
        WaitForSeconds wait = new (DelayInSeconds);

        while (true)
        {
            UpdatePosition();

            yield return wait;
        }
    }

    private void UpdatePosition()
    {
        Transform target = _points[++_index % _points.Length].transform;
        _object.SetPositionAndRotation(target.position, target.rotation);
    }
}