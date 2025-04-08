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
        _inputInformer.RotationPressed += OnRotate;
        _inputInformer.JumpPressed += OnJump;
    }

    private void OnDisable()
    {
        _inputInformer.RotationPressed -= OnRotate;
        _inputInformer.JumpPressed -= OnJump;
    }

    private void OnMove() =>
        OnMove(_inputInformer.GetMovement());

    private void OnRotate(Vector2 direction)
    {
        _horizontalRotator.Rotate(direction.x);
        _verticalRotator.Rotate(direction.y);
    }
}