using System;
using UnityEngine;

public class JoystickInputReader : MonoBehaviour
{
    [SerializeField] private UI.Joystick.Joystick _joystickMovement;
    [SerializeField] private TouchPanelInformer _touchPanel;
    [SerializeField] private ButtonChangeInformer _jumpButton;
    [SerializeField] private ButtonClickInformer _menuButton;
    [SerializeField] private float _rotationSensitivity;

    public event Action<Vector2> Rotated;
    public event Action JumpPressed;
    public event Action MenuPressed;

    private void OnEnable()
    {
        _jumpButton.DownPressed += InformAboutJumpPressed;
        _menuButton.Clicked += InformAboutMenuPressed;
        _touchPanel.Rotated += OnRotation;
    }

    private void OnDisable()
    {
        _jumpButton.DownPressed -= InformAboutJumpPressed;
        _menuButton.Clicked -= InformAboutMenuPressed;
        _touchPanel.Rotated -= OnRotation;
    }

    public Vector2 GetMovement() =>
        _joystickMovement.IsInput(out Vector2 direction) ? direction : Vector2.zero;

    private void OnRotation(Vector2 deltaPosition) =>
        Rotated?.Invoke(deltaPosition * _rotationSensitivity);

    private void InformAboutJumpPressed(ButtonChangeInformer _) =>
        JumpPressed?.Invoke();

    private void InformAboutMenuPressed() =>
        MenuPressed?.Invoke();
}