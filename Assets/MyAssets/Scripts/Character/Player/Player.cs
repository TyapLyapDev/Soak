using System;
using UnityEngine;

public class Player : Character
{
    [SerializeField] private AimMarker _aim;
    [SerializeField] private InputInformer _informer;
    [SerializeField] private LayerMask _aimLayerMask;
    
    private CharacterRotator _rotator;
    private PlayerModelConfigurations _modelConfigurations;
    private CharacterDetector _characterDetector;
    private Shooter _shooter;

    public event Action DeadShowed;

    public CharacterDetector CharacterDetector => _characterDetector;

    protected override void Awake()
    {
        base.Awake();

        Transform camera = Camera.main.transform;
        WaterJet waterJet = camera.GetComponentInChildren<WaterJet>(true);

        _rotator = new(transform, camera);
        _modelConfigurations = new(this);
        _shooter = new(camera, waterJet, _aimLayerMask, Colliders);
        _characterDetector = new(Camera.main.transform, Colliders, _aimLayerMask);
        _characterDetector.Start();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        _informer.MovementPressed += OnMovePressed;
        _informer.RotationPressed += OnRotatePressed;
        _informer.JumpPressed += OnJumpPressed;
        _informer.SneackPressed += OnSneackPressed;
        _informer.Rised += OnRisePressed;
        _informer.SlowingStepPressed += SetSlowingStep;
        _informer.RunningStepPressed += SetNormalStep;
        _informer.ShootingPressed += OnShootingPressed;
        _informer.ShootingUnpressed += OnShootingUnpressed;
        _modelConfigurations.DeadShowed += DeadShowed;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        _informer.MovementPressed -= OnMovePressed;
        _informer.RotationPressed -= OnRotatePressed;
        _informer.JumpPressed -= OnJumpPressed;
        _informer.SneackPressed -= OnSneackPressed;
        _informer.Rised -= OnRisePressed;
        _informer.SlowingStepPressed -= SetSlowingStep;
        _informer.RunningStepPressed -= SetNormalStep;
        _informer.ShootingPressed -= OnShootingPressed;
        _informer.ShootingUnpressed -= OnShootingUnpressed;
        _modelConfigurations.DeadShowed -= DeadShowed;
    }

    public void LeaveOnlySoul()
    {
        _modelConfigurations.LeaveOnlySoul();
        _aim.gameObject.SetActive(false);
    }

    public void DisableControl()
    {
        _modelConfigurations.DisableControl();
        _aim.gameObject.SetActive(false);
    }

    public void EnableControl()
    {
        _modelConfigurations.ProcessResurrect();
        _aim.gameObject.SetActive(IsDead == false);
    }

    public override void Resurrect()
    {
        base.Resurrect();
        _modelConfigurations.ProcessResurrect();
        _aim.gameObject.SetActive(true);
    }

    protected override void OnDied()
    {
        _shooter.StopRay();
        StopPlayWaterJet();

        base.OnDied();

        _modelConfigurations.ProcessDied(Killer);
        _aim.gameObject.SetActive(false);
    }

    private void OnMovePressed(Vector2 inputs)
    {
        if (IsDead || Team == TeamType.Observer)
            return;

        Move(inputs);
    }

    private void OnRotatePressed(Vector2 direction)
    {
        if (IsDead || Team == TeamType.Observer)
            return;

        _rotator.Rotate(direction);
    }

    private void OnJumpPressed()
    {
        if (IsDead || Team == TeamType.Observer)
            return;

        Jump();
    }

    private void OnSneackPressed()
    {
        if (IsDead || Team == TeamType.Observer)
            return;

        Sneack();
    }

    private void OnRisePressed()
    {
        if (IsDead || Team == TeamType.Observer)
            return;

        Rise();
    }

    private void OnShootingPressed()
    {
        if (IsDead || Team == TeamType.Observer)
            return;

        _shooter.StartRay();
        StartPlayWaterJet();
    }

    private void OnShootingUnpressed()
    {
        if (IsDead || Team == TeamType.Observer)
            return;

        _shooter.StopRay();
        StopPlayWaterJet();
    }
}