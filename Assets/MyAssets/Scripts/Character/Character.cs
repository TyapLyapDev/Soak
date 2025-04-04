using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public abstract class Character : MonoBehaviour
{
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _gravityForce;
    [SerializeField] private float _jumpForce;

    private CharacterController _characterController;
    private Mover _mover;
    private Jumper _jumper;
    private HorizontalRotator _horizontalRotator;
    private VerticalRotator _verticalRotator;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _mover = new(_characterController, _gravityForce, _movementSpeed);
        _jumper = new(_mover, _jumpForce);
        _horizontalRotator = new(transform, _rotationSpeed);
        _verticalRotator = new(Camera.main.transform, _rotationSpeed);
    }

    protected void Move(Vector2 direction) =>
        _mover.Move(direction);

    protected void RotateHorizontal(float value) =>
        _horizontalRotator.Rotate(value);

    protected void RotateVertical(float value) =>
        _verticalRotator.Rotate(value);

    protected void Jump()
    {
        if (_characterController.isGrounded)
            _jumper.Jump();
    }
}