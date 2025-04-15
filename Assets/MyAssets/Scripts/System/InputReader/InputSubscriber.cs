using System;
using UnityEngine;

public class InputSubscriber
{
    private readonly KeyboardInputReader _keyboard;
    private readonly TouchInputReader _joystick;

    public InputSubscriber(KeyboardInputReader keyboard, TouchInputReader joystick)
    {
        _keyboard = keyboard;
        _joystick = joystick;
    }

    public void Subscribe(
        Action<Vector2> movementPressed, 
        Action<Vector2> rotationPressed, 
        Action jumpPressed, 
        Action menuPressed, 
        Action sneackPressed, 
        Action rised, 
        Action slowingStep, 
        Action runningStep,
        Action shootingPressed,
        Action shootingUnpressed)
    {
        if (Application.isMobilePlatform)
        {
            _joystick.MovementPressed += movementPressed;
            _joystick.RotationPressed += rotationPressed;
            _joystick.JumpPressed += jumpPressed;
            _joystick.MenuPressed += menuPressed;
            _joystick.SneackPressed += sneackPressed;
            _joystick.Rised += rised;
        }
        else
        {
            _keyboard.MovementPressed += movementPressed;
            _keyboard.RotationPressed += rotationPressed;
            _keyboard.JumpPressed += jumpPressed;
            _keyboard.KeyMenuPressed += menuPressed;
            _keyboard.SneackPressed += sneackPressed;
            _keyboard.Rised += rised;
            _keyboard.SlowingStepPressed += slowingStep;
            _keyboard.RunningStepPressed += runningStep;
            _keyboard.ShootingPressed += shootingPressed;
            _keyboard.ShootingUnpressed += shootingUnpressed;
        }
    }

    public void Unsubscribe(
        Action<Vector2> movementPressed, 
        Action<Vector2> rotationPressed, 
        Action jumpPressed, 
        Action menuPressed, 
        Action sneackPressed, 
        Action rised, 
        Action slowingStep, 
        Action runningStep,
        Action shootingPressed,
        Action shootingUnpressed)
    {
        if (Application.isMobilePlatform)
        {
            _joystick.MovementPressed -= movementPressed;
            _joystick.RotationPressed -= rotationPressed;
            _joystick.JumpPressed -= jumpPressed;
            _joystick.MenuPressed -= menuPressed;
            _joystick.SneackPressed -= sneackPressed;
            _joystick.Rised -= rised;
        }
        else
        {
            _keyboard.MovementPressed -= movementPressed;
            _keyboard.RotationPressed -= rotationPressed;
            _keyboard.JumpPressed -= jumpPressed;
            _keyboard.KeyMenuPressed -= menuPressed;
            _keyboard.SneackPressed -= sneackPressed;
            _keyboard.Rised -= rised;
            _keyboard.SlowingStepPressed -= slowingStep;
            _keyboard.RunningStepPressed -= runningStep;
            _keyboard.ShootingPressed -= shootingPressed;
            _keyboard.ShootingUnpressed -= shootingUnpressed;
        }
    }
}