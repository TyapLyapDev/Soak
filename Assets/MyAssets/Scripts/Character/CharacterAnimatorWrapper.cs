using UnityEngine;

public class CharacterAnimatorWrapper
{
    private readonly Animator _animator;

    public CharacterAnimatorWrapper(Animator animator)
    {
        _animator = animator;
    }

    public void SwitchSneacking(bool isOn) =>
        _animator.SetBool(DataParams.Animator.IsSneaking, isOn);

    public void UpdateMovement(Vector2 moveDirection)
    {
        _animator.SetFloat(DataParams.Animator.RightMoving, moveDirection.x);
        _animator.SetFloat(DataParams.Animator.ForwardMoving, moveDirection.y);
    }
}