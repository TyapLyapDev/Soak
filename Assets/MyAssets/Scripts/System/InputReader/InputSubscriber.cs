using System;
using UnityEngine;

public class InputSubscriber
{
    private KeyboardInputReader _keyboard;
    private TouchInputReader _joystick;

    public InputSubscriber(KeyboardInputReader keyboard, TouchInputReader joystick)
    {
        _keyboard = keyboard;
        _joystick = joystick;
    }

    public void Subscribe(Action<Vector2> RotationPressed, Action JumpPressed, Action MenuPressed)
    {
        if (Application.isMobilePlatform)
            SubscribeToMobile(RotationPressed, JumpPressed, MenuPressed);
        else
            SubscribeToPC(RotationPressed, JumpPressed, MenuPressed);
    }

    public void Unsubscribe(Action<Vector2> RotationPressed, Action JumpPressed, Action MenuPressed)
    {
        if (Application.isMobilePlatform)
            UnsubscribeFromMobile(RotationPressed, JumpPressed, MenuPressed);
        else
            UnsubscribeFromPC(RotationPressed, JumpPressed, MenuPressed);
    }

    public void SubscribeToMobile(Action<Vector2> RotationPressed, Action JumpPressed, Action MenuPressed)
    {
        _joystick.RotationPressed += RotationPressed;
        _joystick.JumpPressed += JumpPressed;
        _joystick.MenuPressed += MenuPressed;
    }

    public void SubscribeToPC(Action<Vector2> RotationPressed, Action JumpPressed, Action MenuPressed)
    {
        _keyboard.RotationPressed += RotationPressed;
        _keyboard.JumpPressed += JumpPressed;
        _keyboard.KeyMenuPressed += MenuPressed;
    }

    public void UnsubscribeFromMobile(Action<Vector2> RotationPressed, Action JumpPressed, Action MenuPressed)
    {
        _joystick.RotationPressed -= RotationPressed;
        _joystick.JumpPressed -= JumpPressed;
        _joystick.MenuPressed -= MenuPressed;
    }

    public void UnsubscribeFromPC(Action<Vector2> RotationPressed, Action JumpPressed, Action MenuPressed)
    {
        _keyboard.RotationPressed -= RotationPressed;
        _keyboard.JumpPressed -= JumpPressed;
        _keyboard.KeyMenuPressed -= MenuPressed;
    }
}