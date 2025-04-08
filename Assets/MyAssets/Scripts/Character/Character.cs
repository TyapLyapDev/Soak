using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public abstract class Character : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _gravityForce;
    [SerializeField] private float _jumpForce;

    private CharacterAnimatorWrapper _characterAnimatorWrapper;
    private CharacterController _characterController;
    private DeltaMovementCalculator _deltaCalculator;
    private Mover _mover;
    private Jumper _jumper;

    protected virtual void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _deltaCalculator = new(transform);
        _mover = new(_characterController, _gravityForce, _movementSpeed);
        _jumper = new(_mover, _jumpForce);
        _characterAnimatorWrapper = new(_animator);
    }

    protected void SwitchSneacking(bool isOn) =>
        _characterAnimatorWrapper.SwitchSneacking(isOn);

    protected void Move(Vector2 direction)
    {
        _mover.Move(direction);
        _characterAnimatorWrapper.UpdateMovement(_deltaCalculator.GetNormalizedDelta(_movementSpeed));
    }

    protected void Jump()
    {
        if (_characterController.isGrounded)
            _jumper.Jump();
    }
}