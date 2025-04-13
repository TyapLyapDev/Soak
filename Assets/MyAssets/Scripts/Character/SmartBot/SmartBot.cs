using System.Linq;
using UnityEngine;

public class SmartBot : Character
{
    [SerializeField] private PointToHide[] _pointToHide;
    [SerializeField] private GazeDirection _gazeDirection;

    private BotTargetSwitcher _targetSwitcher;
    private SmartBotMover _botMover;
    private SmartBotRotator _botRotator;

    protected override void Awake()
    {
        base.Awake();
        _botMover = new(this);
        _botRotator = new(this, _gazeDirection);
        _targetSwitcher = new(transform, _pointToHide.Select(t => t.transform).ToArray());
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
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        _botMover.Sneaked -= OnSneacked;
        _botMover.Rised -= OnRised;
        _botMover.Slowed -= OnSlowed;
        _targetSwitcher.Switched -= OnTargetSwitched;
    }

    private void OnMove()
    {
        Vector2 input = _targetSwitcher.GetDirectionToTarget();

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
}