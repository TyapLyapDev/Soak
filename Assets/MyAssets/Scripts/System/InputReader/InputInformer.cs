using System;
using UnityEngine;

public class InputInformer : MonoBehaviour
{
    [SerializeField] private MenuShower _menuShower;
    [SerializeField] private KeyboardInputReader _keyboard;
    [SerializeField] private TouchInputReader _joystick;
    [SerializeField] private SliderHorizontalRotationSensitivity _mouseSensitivityHorizontal;
    [SerializeField] private SliderVerticalRotationSensitivity _mouseSensitivityVertical;

    private InputSubscriber _subscriber;

    public event Action<Vector2> MovementPressed;
    public event Action<Vector2> RotationPressed;
    public event Action JumpPressed;
    public event Action MenuPressed;
    public event Action SneackPressed;
    public event Action Rised;
    public event Action SlowingStepPressed;
    public event Action RunningStepPressed;
    public event Action ShootingPressed;
    public event Action ShootingUnpressed;

    private void Awake() =>
        _subscriber = new(_keyboard, _joystick);

    private void OnEnable()
    {
        _subscriber.Subscribe(
            OnMovementPressed,
            OnRotationPressed,
            OnJumpPressed,
            OnMenuPressed,
            OnSneackPressed,
            OnRised,
            OnSlowingStep,
            OnRunningStep,
            OnShootingPressed,
            OnShootingUnpressed);
    }

    private void OnDisable()
    {
        _subscriber.Unsubscribe(
            OnMovementPressed,
            OnRotationPressed,
            OnJumpPressed,
            OnMenuPressed,
            OnSneackPressed,
            OnRised,
            OnSlowingStep,
            OnRunningStep,
            OnShootingPressed,
            OnShootingUnpressed);
    }

    private void OnMovementPressed(Vector2 direction) =>
        MovementPressed?.Invoke(direction);

    private void OnRotationPressed(Vector2 direction)
    {
        if (_menuShower.IsShowing)
            return;

        direction *= new Vector2(_mouseSensitivityHorizontal.Value, _mouseSensitivityVertical.Value);
        RotationPressed?.Invoke(direction);
    }

    private void OnJumpPressed() =>
        JumpPressed?.Invoke();

    private void OnSneackPressed() =>
        SneackPressed?.Invoke();

    private void OnRised() =>
        Rised?.Invoke();

    private void OnSlowingStep() =>
        SlowingStepPressed?.Invoke();

    private void OnRunningStep() =>
        RunningStepPressed?.Invoke();

    private void OnMenuPressed() =>
        MenuPressed?.Invoke();

    private void OnShootingPressed()
    {
        if (_menuShower.IsShowing)
            return;

        ShootingPressed?.Invoke();
    }

    private void OnShootingUnpressed()
    {
        if (_menuShower.IsShowing)
            return;

        ShootingUnpressed?.Invoke();
    }
}