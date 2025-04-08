using UnityEngine;

public class Player : Character
{
    [SerializeField] private InputInformer _inputInformer;

    private VerticalRotator _verticalRotator;
    private HorizontalRotator _horizontalRotator;

    protected override void Awake()
    {
        base.Awake();
        _horizontalRotator = new(transform);
        _verticalRotator = new(Camera.main.transform);
    }

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
        _horizontalRotator.Rotate(direction.x);
        _verticalRotator.Rotate(direction.y);
    }

    private void OnJump() =>
        Jump();
}