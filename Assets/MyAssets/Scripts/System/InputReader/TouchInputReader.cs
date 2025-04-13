using System;
using UI.Joystick;
using UnityEngine;

public class TouchInputReader : MonoBehaviour
{
    [SerializeField] private CustomJoystick _joystickMovement;
    [SerializeField] private TouchPanelInformer _touchPanel;
    [SerializeField] private ButtonChangeInformer _jumpButton;
    [SerializeField] private ButtonChangeInformer _sneackButton;
    [SerializeField] private MobileButtonShowMenu _menuButton;

    private bool _isSneacking;

    public event Action<Vector2> MovementPressed;
    public event Action<Vector2> RotationPressed;
    public event Action JumpPressed;
    public event Action MenuPressed;
    public event Action SneackPressed;
    public event Action Rised;

    public bool IsSneacking => _isSneacking;

    private void Update() =>
        OnMovement();

    private void OnEnable()
    {
        _jumpButton.DownPressed += OnJumpPressed;
        _sneackButton.DownPressed += OnSneackPressed;
        _menuButton.Clicked += OnMenuPressed;
        _touchPanel.Rotated += OnRotationPressed;
    }

    private void OnDisable()
    {
        _jumpButton.DownPressed -= OnJumpPressed;
        _sneackButton.DownPressed -= OnSneackPressed;
        _menuButton.Clicked -= OnMenuPressed;
        _touchPanel.Rotated -= OnRotationPressed;
    }

    private void OnMovement()
    {
        Vector2 movementDirection = _joystickMovement.IsInput(out Vector2 direction) ? direction : Vector2.zero;
        MovementPressed?.Invoke(movementDirection);
    }

    private void OnRotationPressed(Vector2 deltaPosition) =>
        RotationPressed?.Invoke(deltaPosition * DataParams.Inputs.TouchDragSensitivity);

    private void OnJumpPressed(ButtonChangeInformer _) =>
        JumpPressed?.Invoke();
    
    private void OnSneackPressed(ButtonChangeInformer _)
    {
        _isSneacking = !_isSneacking;

        if(_isSneacking)
            SneackPressed?.Invoke();
        else
            Rised?.Invoke();
    }

    private void OnMenuPressed() =>
        MenuPressed?.Invoke();
}