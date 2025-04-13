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

    public void UpdateMovement(Vector2 movementDirection)
    {
        _animator.SetFloat(DataParams.Animator.RightMoving, movementDirection.x);
        _animator.SetFloat(DataParams.Animator.ForwardMoving, movementDirection.y);
    }
}