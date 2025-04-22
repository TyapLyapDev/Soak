using System;
using UnityEngine;

public class PlayerSoul
{
    private readonly SoulMover _soulMover;
    private readonly SoulRotator _soulRotator;

    public PlayerSoul()
    {
        Camera transform = Camera.main;

        if (transform.TryGetComponent(out _soulMover) == false)
            throw new NullReferenceException("Не удалось найти компонент SoulMover на компоненте Camera");

        if (transform.TryGetComponent(out _soulRotator) == false)
            throw new NullReferenceException("Не удалось найти компонент SoulRotator на компоненте Camera");
    }

    public void DisableMovementContol()
    {
        _soulMover.enabled = false;
        _soulRotator.enabled = false;
    }

    public void EnableMovementContol()
    {
        _soulMover.enabled = true;
        _soulRotator.enabled = true;
    }
}