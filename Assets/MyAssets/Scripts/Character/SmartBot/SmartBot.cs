using System;
using System.Collections.Generic;
using UnityEngine;

public class SmartBot : Character
{
    [SerializeField] private LayerMask _aimLayerMask;

    private GazeDirection _gazeDirection;
    private BotTargetSwitcher _targetSwitcher;
    private BotFreeMovement _freeMovement;
    private SmartBotMover _botMover;
    private SmartBotRotator _botRotator;
    private BotEnemyAttacker _botEnemyAttacker;
    private Shooter _shooter;

    protected override void Awake()
    {
        base.Awake();
        _gazeDirection = GetComponentInChildren<GazeDirection>(true);

        _botMover = new(this);
        _freeMovement = new(this);
        _botRotator = new(this, _gazeDirection);
        //_targetSwitcher = new(transform, _pointToHide.Select(t => t.transform).ToArray());
        _targetSwitcher = new(transform, null);

        GazeDirection eyes = GetComponentInChildren<GazeDirection>(true);
        WaterJet waterJet = GetComponentInChildren<WaterJet>(true);

        _shooter = new(eyes.transform, waterJet, _aimLayerMask, Colliders);
    }

    private void Update()
    {
        if (IsDead)
            return;

        OnMove();
    }

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

    public override void Init(List<Character> characters)
    {
        base.Init(characters);
        _botEnemyAttacker = new(this, _aimLayerMask, characters);

        if (IsDead == false)
            _botEnemyAttacker.Start();

        _botEnemyAttacker.EnemySerched += OnEnemySearched;
        _botEnemyAttacker.EnemyLost += OnEnemyLost;
        _botEnemyAttacker.ShootPressed += StartShooting;
        _botEnemyAttacker.ShootUnpressed += StopShooting;
    }

    private void OnMove()
    {
        if (IsDead)
            throw new Exception("Ћогика продолжает работать после гибели бота");

        Vector2 input = _botMover.IsMoving ? _freeMovement.Input : Vector2.zero;
        Move(input);
    }

    private void OnSneacked()
    {
        if (IsDead)
            throw new Exception("Ћогика продолжает работать после гибели бота");

        Sneack();
        SetNormalStep();
    }

    private void OnRised()
    {
        if (IsDead)
            throw new Exception("Ћогика продолжает работать после гибели бота");

        Rise();
        SetNormalStep();
    }

    private void OnSlowed()
    {
        if (IsDead)
            throw new Exception("Ћогика продолжает работать после гибели бота");

        Rise();
        SetSlowingStep();
    }

    private void OnTargetSwitched()
    {
        if (IsDead)
            throw new Exception("Ћогика продолжает работать после гибели бота");

        _botRotator.UpdateTarget(_targetSwitcher.Target);
    }

    private void OnJumpOpened()
    {
        if (IsDead)
            throw new Exception("Ћогика продолжает работать после гибели бота");

        Jump();
    }

    private void OnEnemySearched(Character character)
    {
        if (IsDead)
            throw new Exception("Ћогика продолжает работать после гибели бота");

        _botRotator.RotateToEnemyTarget();
        _targetSwitcher.Stop();
        _targetSwitcher.SetTarget(character.transform);
    }

    private void OnEnemyLost()
    {
        if (IsDead)
            throw new Exception("Ћогика продолжает работать после гибели бота");

        _targetSwitcher.SetTarget(null);
        _botRotator.RotateWithoutEnemy();
    }

    protected void StartShooting()
    {
        if (IsDead)
            throw new Exception("Ћогика продолжает работать после гибели бота");

        _shooter.StartRay();
        StartPlayWaterJet();
    }

    protected void StopShooting()
    {
        _shooter.StopRay();
        StopPlayWaterJet();
    }

    protected override void OnDied()
    {
        StopShooting();
        _targetSwitcher.Stop();
        _freeMovement.Stop();
        _botRotator.Stop();
        _botMover.Stop();

        _botEnemyAttacker?.Stop();

        base.OnDied();
    }

    public override void Resurrect()
    {
        base.Resurrect();
        _botEnemyAttacker.Start();
        _freeMovement.Start();
        _botRotator.Start();
        _botMover.Start();
    }
}