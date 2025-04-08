using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public abstract class Character : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private CharacterController _characterController;
    private CharacterAnimatorWrapper _characterAnimatorWrapper;
    private DeltaMovementCalculator _deltaCalculator;
    private Mover _mover;
    private Jumper _jumper;

    protected virtual void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _characterAnimatorWrapper = new(_animator);
        _deltaCalculator = new(transform);
        _mover = new(_characterController, DataParams.Character.MovementSpeed);
        _jumper = new(_mover, DataParams.Character.JumpingForce);
    }

    protected void SwitchSneacking(bool isOn) =>
        _characterAnimatorWrapper.SwitchSneacking(isOn);

    protected void OnMove(Vector2 direction)
    {
        _mover.Move(direction);
        _characterAnimatorWrapper.UpdateMovement(_deltaCalculator.GetNormalizedDelta(DataParams.Character.MovementSpeed));
    }

    protected void OnJump()
    {
        if (_characterController.isGrounded)
            _jumper.Jump();
    }
}