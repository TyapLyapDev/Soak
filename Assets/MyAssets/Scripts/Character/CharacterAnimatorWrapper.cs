using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CharacterAnimatorWrapper : MonoBehaviour
{
    private Animator _animator;

    private void Awake() =>
        _animator = GetComponent<Animator>();

    public void PlaySneacking() =>
        _animator.SetBool(DataParams.Animator.IsSneaking, true);

    public void PlayRising() =>
        _animator.SetBool(DataParams.Animator.IsSneaking, false);

    public void UpdateMovement(Vector2 movementDirection)
    {
        _animator.SetFloat(DataParams.Animator.RightMoving, movementDirection.x);
        _animator.SetFloat(DataParams.Animator.ForwardMoving, movementDirection.y);
    }

    public void EnableAnimator() =>
        _animator.enabled = true;

    public void DisableAnimator() =>
        _animator.enabled = false;
}