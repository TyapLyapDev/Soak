using System;
using System.Collections.Generic;
using UnityEngine;

public class SmartBotMover
{
    private readonly BotMovementTypeSwitcher _movementTypeSwitcher;

    private Dictionary<MovementType, Action> _states;
    private bool _isMoving = false;

    public event Action Sneaked;
    public event Action Rised;
    public event Action Slowed;

    public SmartBotMover(MonoBehaviour mono)
    {
        _movementTypeSwitcher = new(mono);
        _movementTypeSwitcher.Switched += OnMovementTypeSwitched;
        InitStates();
    }

    public bool IsMoving => _isMoving;

    private void InitStates()
    {
        _states = new Dictionary<MovementType, Action>
        {
            { MovementType.Standing, HandleStanding },
            { MovementType.Sitting, HandleSitting },
            { MovementType.Walking, HandleSlowingStep },
            { MovementType.Running, HandleRunning },
            { MovementType.Sneacking, HandleSneacking },
        };
    }

    private void OnMovementTypeSwitched()
    {
        if (_states.TryGetValue(_movementTypeSwitcher.GetCurrentType, out Action action))
            action.Invoke();
    }

    private void HandleStanding()
    {
        Rised?.Invoke();
        _isMoving = false;
    }

    private void HandleSitting()
    {
        Sneaked?.Invoke();
        _isMoving = false;
    }

    private void HandleSlowingStep()
    {
        Slowed?.Invoke();
        _isMoving = true;
    }

    private void HandleRunning()
    {
        Rised?.Invoke();
        _isMoving = true;
    }

    private void HandleSneacking()
    {
        Sneaked?.Invoke();
        _isMoving = true;
    }
}