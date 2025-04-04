using System;
using UnityEngine;

public class InputInformer : MonoBehaviour
{
    [SerializeField] private MenuShower _menuShower;
    [SerializeField] private KeyBoardInputReader _keyBoardInputReader;
    [SerializeField] private JoystickInputReader _joystickInputReader;

    public event Action<Vector2> RotationPressed;
    public event Action JumpPressed;
    public event Action MenuPressed;

    private void OnEnable()
    {
        if (Application.isMobilePlatform)
            SubscribeMobileInput();
        else
            SubscribePCInput();
    }

    private void OnDisable()
    {
        if (Application.isMobilePlatform)
            UnsubscribeMobileInput();
        else
            UnsubscribePCInput();
    }

    private void SubscribeMobileInput()
    {
        _joystickInputReader.Rotated += InformAboutRotationPressed;
        _joystickInputReader.JumpPressed += InformAboutJumpPressed;
    }

    private void SubscribePCInput()
    {
        _keyBoardInputReader.RotationPressed += InformAboutRotationPressed;
        _keyBoardInputReader.JumpPressed += InformAboutJumpPressed;
        _keyBoardInputReader.KeyMenuPressed += InformAboutMenuPressed;
    }

    private void UnsubscribeMobileInput()
    {
        _joystickInputReader.Rotated -= InformAboutRotationPressed;
        _joystickInputReader.JumpPressed -= InformAboutJumpPressed;        
    }

    private void UnsubscribePCInput()
    {
        _keyBoardInputReader.RotationPressed -= InformAboutRotationPressed;
        _keyBoardInputReader.JumpPressed -= InformAboutJumpPressed;
        _keyBoardInputReader.KeyMenuPressed -= InformAboutMenuPressed;
    }

    public Vector2 GetMovement()
    {
        if (Application.isMobilePlatform)
            return _joystickInputReader.GetMovement();
        else
            return _keyBoardInputReader.GetMovement();
    }

    private void InformAboutJumpPressed() =>
        JumpPressed?.Invoke();

    private void InformAboutRotationPressed(Vector2 direction)
    {
        if (_menuShower.IsShowing == false)
            RotationPressed?.Invoke(direction);
    }

    private void InformAboutMenuPressed() =>
        MenuPressed?.Invoke();
}