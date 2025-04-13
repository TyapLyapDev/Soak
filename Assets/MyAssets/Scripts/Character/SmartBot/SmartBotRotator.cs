using System;
using System.Collections.Generic;
using UnityEngine;

public class SmartBotRotator
{
    private readonly GazeDirection _gazeDirection;
    private readonly BotRotationTypeSwitcher _rotationTypeSwitcher;
    private readonly BotRotatorToTarget _rotatorToTarget;
    private readonly BotLookingAroundRotator _lookingAroundRotator;
    private readonly BotForwardRotator _forwardRotator;
    private readonly Rutine _rutine;

    private Dictionary<LookingType, Action> _states;

    public SmartBotRotator(MonoBehaviour mono, GazeDirection gazeDirection)
    {
        _rotationTypeSwitcher = new(mono);
        _gazeDirection = gazeDirection;
        _rotatorToTarget = new(mono.transform, _gazeDirection.transform);
        _lookingAroundRotator = new(mono.transform, _gazeDirection.transform);
        _forwardRotator = new(mono.transform, _gazeDirection.transform);
        _rutine = new(mono, UpdateRotation);

        InitStates();
        _rutine.Start();
        _rotatorToTarget.NoTarget += OnLossTarget;
    }

    private void InitStates()
    {
        _states = new Dictionary<LookingType, Action>
        { 
            { LookingType.Target, _rotatorToTarget.UpdateRotation },
            { LookingType.Around, _lookingAroundRotator.UpdateRotation },
            { LookingType.Forward, _forwardRotator.UpdateRotation },
        };
    }

    private void OnLossTarget() =>
        _rotationTypeSwitcher.SetNewRandomType();

    private void UpdateRotation()
    {
        if(_states.TryGetValue(_rotationTypeSwitcher.GetCurrentType, out Action action))
            action?.Invoke();
    }

    public void UpdateLastMovementDirection(Vector3 direction) =>
        _forwardRotator.UpdateLastMovementDirection(direction);

    public void UpdateTargetPosition(Vector3 targetPosition) =>
        _rotatorToTarget.UpdateTargetPosition(targetPosition);
}