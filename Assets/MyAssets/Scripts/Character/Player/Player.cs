using UnityEngine;

public class Player : Character
{
    [SerializeField] private InputInformer _informer;
    [SerializeField] private LayerMask _aimLayerMask;
    
    private CharacterRotator _rotator;
    private PlayerModelConfigurations _modelConfigurations;
    private CharacterDetector _characterDetector;
    private Shooter _shooter;

    public CharacterDetector CharacterDetector => _characterDetector;

    protected override void Awake()
    {
        base.Awake();

        Transform camera = Camera.main.transform;
        WaterJet waterJet = camera.GetComponentInChildren<WaterJet>(true);

        _rotator = new(transform, camera);
        _modelConfigurations = new(transform);
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
    }

    public override void Resurrect()
    {
        base.Resurrect();
        _modelConfigurations.ProcessResurrect();
    }

    protected override void OnDied()
    {
        base.OnDied();
        _shooter.StopRay();
        _modelConfigurations.ProcessDied();
    }

    private void OnMovePressed(Vector2 inputs)
    {
        if (IsDeath)
            return;

        Move(inputs);
    }

    private void OnRotatePressed(Vector2 direction)
    {
        if (IsDeath)
            return;

        _rotator.Rotate(direction);
    }

    private void OnJumpPressed()
    {
        if (IsDeath)
            return;

        Jump();
    }

    private void OnSneackPressed()
    {
        if (IsDeath)
            return;

        Sneack();
    }

    private void OnRisePressed()
    {
        if (IsDeath)
            return;

        Rise();
    }

    private void OnShootingPressed()
    {
        if (IsDeath)
            return;

        _shooter.StartRay();
    }

    private void OnShootingUnpressed()
    {
        if (IsDeath)
            return;

        _shooter.StopRay();
    }
}