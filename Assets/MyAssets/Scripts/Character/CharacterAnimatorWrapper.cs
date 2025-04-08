using UnityEngine;

public class CharacterAnimatorWrapper
{
    private const string IsSneaking = nameof(IsSneaking);
    private const string RightMoving = nameof(RightMoving);
    private const string ForwardMoving = nameof(ForwardMoving);
    private const string Jump = nameof(Jump);

    private readonly Animator _animator;

    public CharacterAnimatorWrapper(Animator animator)
    {
        _animator = animator;
    }

    public void SwitchSneacking(bool isOn) =>
        _animator.SetBool(IsSneaking, isOn);

    public void UpdateMovement(Vector2 moveDirection)
    {
        _animator.SetFloat(RightMoving, moveDirection.x);
        _animator.SetFloat(ForwardMoving, moveDirection.y);
    }
}