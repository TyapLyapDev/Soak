using System;
using UnityEngine;

public class SmartBot : Character, IDeactivatable<SmartBot>
{    
    [SerializeField] private PointToHide[] _pointToHide;
    [SerializeField] private GazeDirection _gazeDirection;
    [SerializeField] private bool _isPatrolling;

    private BotTargetSwitcher _targetSwitcher;
    private BotFreeMovement _freeMovement;
    private SmartBotMover _botMover;
    private SmartBotRotator _botRotator;

    public event Action<SmartBot> Deactivated;

    protected override void Awake()
    {
        base.Awake();
        _botMover = new(this);
        _freeMovement = new(this);
        _botRotator = new(this, _gazeDirection);
        //_targetSwitcher = new(transform, _pointToHide.Select(t => t.transform).ToArray());
        _targetSwitcher = new(transform, null);
    }

    private void Update() =>
        OnMove();

    protected override void OnEnable()
    {
        base.OnEnable();
        _botMover.Sneaked += OnSneacked;
        _botMover.Rised += OnRised;
        _botMover.Slowed += OnSlowed;
        _targetSwitcher.Switched += OnTargetSwitched;
        _freeMovement.JumpOpened += OnJumpOpened;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        _botMover.Sneaked -= OnSneacked;
        _botMover.Rised -= OnRised;
        _botMover.Slowed -= OnSlowed;
        _targetSwitcher.Switched -= OnTargetSwitched;
        _freeMovement.JumpOpened -= OnJumpOpened;
    }

    public void Deactivate() =>
        Deactivated?.Invoke(this);

    private void OnMove()
    {
        Vector2 input;

        if (_isPatrolling)
        {
            input = _targetSwitcher.GetInputToTarget();
        }
        else
        {
            input = _freeMovement.Input;
        }

        if (_botMover.IsMoving == false)
            input = Vector2.zero;

        Move(input);
    }

    private void OnSneacked()
    {
        Sneack();
        SetRunningStep();
    }

    private void OnRised()
    {
        Rise();
        SetRunningStep();
    }

    private void OnSlowed()
    {
        Rise();
        SetSlowingStep();
    }

    private void OnTargetSwitched() =>
        _botRotator.UpdateTarget(_targetSwitcher.Target);

    private void OnJumpOpened() =>
        Jump();    
}