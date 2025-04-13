using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(AudioSource))]
public abstract class Character : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private CharacterModel _model;
    [SerializeField] private EventAnimation _eventAnimation;
    [SerializeField] private AudioClip _audioClip;

    private CharacterController _controller;
    private CharacterAnimatorWrapper _characterAnimatorWrapper;
    private DeltaMovementCalculator _deltaCalculator;
    private Mover _mover;
    private Jumper _jumper;
    private Sneacker _sneacker;
    private AudioSource _audioSource;
    private bool _isSlowingStep = false;

    private float _currentSpeed;

    protected virtual void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _audioSource = GetComponent<AudioSource>();

        _characterAnimatorWrapper = new(_animator);
        _deltaCalculator = new(transform);
        _mover = new(_controller);
        _jumper = new(_mover, DataParams.Character.JumpingForce);
        _sneacker = new(_controller, _model);
    }

    protected virtual void OnEnable() =>
        _eventAnimation.Stepped += OnStep;

    protected virtual void OnDisable() =>
        _eventAnimation.Stepped -= OnStep;

    protected void Move(Vector2 direction)
    {
        if (_sneacker.IsSneacking && _controller.isGrounded)
            direction *= DataParams.Character.SneakingStepMultiplierSpeed;
        else if (_isSlowingStep)
            direction *= DataParams.Character.SlowingStepMultiplierSpeed;

        _mover.Move(direction);

        _currentSpeed = Mathf.Abs(direction.x) + Mathf.Abs(direction.y);

        Vector2 movementAnimation = _deltaCalculator.GetNormalizedDelta();
        _characterAnimatorWrapper.UpdateMovement(movementAnimation);
    }

    protected void Jump()
    {
        if (_controller.isGrounded)
            _jumper.Jump();
    }

    protected void Sneack()
    {
        _sneacker.Sneack();
        SwitchSneackingAnimation();
    }

    protected void Rise()
    {
        _sneacker.Rise();
        SwitchSneackingAnimation();
    }

    private void SwitchSneackingAnimation() =>
        _characterAnimatorWrapper.SwitchSneacking(_sneacker.IsSneacking);

    protected void SetSlowingStep() =>
        _isSlowingStep = true;

    protected void SetRunningStep() =>
        _isSlowingStep = false;

    private void OnStep()
    {
        if (_currentSpeed > 0.9f)
            _audioSource.PlayOneShot(_audioClip);
    }
}