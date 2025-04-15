using UnityEngine;

public class Player : Character
{
    [SerializeField] private InputInformer _inputInformer;

    private VerticalRotator _verticalRotator;
    private HorizontalRotator _horizontalRotator;

    protected override void Awake()
    {
        base.Awake();
        _horizontalRotator = new(transform);
        _verticalRotator = new(Camera.main.transform);
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        _inputInformer.MovementPressed += Move;
        _inputInformer.RotationPressed += OnRotate;
        _inputInformer.JumpPressed += Jump;
        _inputInformer.SneackPressed += Sneack;
        _inputInformer.Rised += Rise;
        _inputInformer.SlowingStepPressed += SetSlowingStep;
        _inputInformer.RunningStepPressed += SetRunningStep;
        _inputInformer.ShootingPressed += StartShooting;
        _inputInformer.ShootingUnpressed += StopShooting;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        _inputInformer.MovementPressed -= Move;
        _inputInformer.RotationPressed -= OnRotate;
        _inputInformer.JumpPressed -= Jump;
        _inputInformer.SneackPressed -= Sneack;
        _inputInformer.Rised -= Rise;
        _inputInformer.SlowingStepPressed -= SetSlowingStep;
        _inputInformer.RunningStepPressed -= SetRunningStep;
        _inputInformer.ShootingPressed -= StartShooting;
        _inputInformer.ShootingUnpressed -= StopShooting;
    }

    private void OnRotate(Vector2 direction)
    {
        _horizontalRotator.Rotate(direction.x);
        _verticalRotator.Rotate(direction.y);
    }
}