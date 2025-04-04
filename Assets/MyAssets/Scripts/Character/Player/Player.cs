using UnityEngine;

public class Player : Character
{
    [SerializeField] private InputInformer _inputInformer;

    private void Update() =>
        OnMove();

    private void OnEnable()
    {
        _inputInformer.JumpPressed += OnJump;
        _inputInformer.RotationPressed += OnRotate;
    }

    private void OnDisable()
    {
        _inputInformer.JumpPressed -= OnJump;
        _inputInformer.RotationPressed -= OnRotate;
    }

    private void OnMove() =>
        Move(_inputInformer.GetMovement());

    private void OnRotate(Vector2 direction)
    {
        RotateHorizontal(direction.x);
        RotateVertical(direction.y);
    }

    private void OnJump() =>
        Jump();
}