using System;
using UnityEngine;

public class InputInformer : MonoBehaviour
{
    [SerializeField] private MenuShower _menuShower;
    [SerializeField] private KeyboardInputReader _keyboard;
    [SerializeField] private TouchInputReader _joystick;
    [SerializeField] private SliderChangeInformer _mouseSensitivityHorizontal;
    [SerializeField] private SliderChangeInformer _mouseSensitivityVertical;

    private InputSubscriber _subscriber;

    public event Action<Vector2> RotationPressed;
    public event Action JumpPressed;
    public event Action MenuPressed;

    private void Awake() =>
        _subscriber = new(_keyboard, _joystick);

    private void OnEnable() =>
        _subscriber.Subscribe(OnRotationPressed, OnJumpPressed, OnMenuPressed);

    private void OnDisable() =>
        _subscriber.Unsubscribe(OnRotationPressed, OnJumpPressed, OnMenuPressed);

    public Vector2 GetMovement() =>
        Application.isMobilePlatform ? _joystick.GetMovement() : _keyboard.GetMovement();

    private void OnJumpPressed() =>
        JumpPressed?.Invoke();

    private void OnRotationPressed(Vector2 direction)
    {
        if (_menuShower.IsShowing == false)
            RotationPressed?.Invoke(direction * new Vector2(_mouseSensitivityHorizontal.Value, _mouseSensitivityVertical.Value));
    }

    private void OnMenuPressed() =>
        MenuPressed?.Invoke();
}