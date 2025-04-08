using System;
using UI.Joystick;
using UnityEngine;

public class TouchInputReader : MonoBehaviour
{
    [SerializeField] private CustomJoystick _joystickMovement;
    [SerializeField] private TouchPanelInformer _touchPanel;
    [SerializeField] private ButtonChangeInformer _jumpButton;
    [SerializeField] private ButtonClickInformer _menuButton;

    public event Action<Vector2> RotationPressed;
    public event Action JumpPressed;
    public event Action MenuPressed;

    private void OnEnable()
    {
        _jumpButton.DownPressed += OnJumpPressed;
        _menuButton.Clicked += OnMenuPressed;
        _touchPanel.Rotated += OnRotationPressed;
    }

    private void OnDisable()
    {
        _jumpButton.DownPressed -= OnJumpPressed;
        _menuButton.Clicked -= OnMenuPressed;
        _touchPanel.Rotated -= OnRotationPressed;
    }

    public Vector2 GetMovement() =>
        _joystickMovement.IsInput(out Vector2 direction) ? direction : Vector2.zero;

    private void OnRotationPressed(Vector2 deltaPosition) =>
        RotationPressed?.Invoke(deltaPosition * DataParams.Inputs.TouchDragSensitivity);

    private void OnJumpPressed(ButtonChangeInformer _) =>
        JumpPressed?.Invoke();

    private void OnMenuPressed() =>
        MenuPressed?.Invoke();
}