using System;
using UnityEngine;

public class EventAnimation : MonoBehaviour
{
    public event Action Stepped;

    public void OnLeftStep() =>
        Stepped?.Invoke();

    public void OnRightStep() =>
        Stepped?.Invoke();
}