using System;
using UnityEngine;

public class CharacterPhysics
{
    private CharacterController _characterController;
    private Mover _mover;
    private Jumper _jumper;
    private DamageDetector _damageDetector;
    private RagdollHandler _ragdoll;
    private Sneacker _sneacker;
    private DeltaMovementCalculator _deltaMovementCalculator;
    private readonly Transform _transform;

    private float _currentSpeed;
    private bool _isSlowingStep;

    private Character _killer;

    public event Action<float> DamageTaked;

    public CharacterPhysics(Transform characterTransform)
    {
        _transform = characterTransform;
    }

    public Vector2 DeltaDistance => _deltaMovementCalculator.GetNormalizedDelta();

    public bool IsSneacking => _sneacker.IsSneacking;

    public float CurrentSpeed => _currentSpeed;

    public Character Killer => _killer;

    public void Init()
    {
        _characterController = _transform.GetComponent<CharacterController>();
        _mover = new(_transform);
        _jumper = new(_mover);
        _damageDetector = new(_transform);
        _ragdoll = new(_transform);
        _sneacker = new(_transform);
        _deltaMovementCalculator = new(_transform);
    }

    public void Subscribe()
    {
        _damageDetector.Subscribe();
        _damageDetector.DamageTaked += OnDamageTaked;
    }

    public void Unsubscribe()
    {
        _damageDetector.Unsubscribe();
        _damageDetector.DamageTaked -= OnDamageTaked;
    }

    public void Jump()
    {
        if (_characterController.isGrounded)
            _jumper.Jump();
    }

    public void Sneack() =>
        _sneacker.Sneack();

    public void Rise() =>
        _sneacker.Rise();

    public void SetSlowingStep() =>
        _isSlowingStep = true;

    public void SetNormalStep() =>
        _isSlowingStep = false;

    public void Move(Vector2 direction)
    {
        if (_sneacker.IsSneacking && _characterController.isGrounded)
            direction *= DataParams.Character.SneakingStepMultiplierSpeed;
        else if (_isSlowingStep)
            direction *= DataParams.Character.SlowingStepMultiplierSpeed;

        _mover.Move(direction);

        _currentSpeed = Mathf.Abs(direction.x) + Mathf.Abs(direction.y);
    }

    public void Disable()
    {
        _ragdoll.Enable();
        _characterController.enabled = false;
    }

    public void Enable()
    {
        _ragdoll.Disable();
        _characterController.enabled = true;
    }

    public void ResetKiller() =>
        _killer = null;

    private void OnDamageTaked(Character other, float value)
    {
        DamageTaked?.Invoke(value);
        _killer = other;
    }
}