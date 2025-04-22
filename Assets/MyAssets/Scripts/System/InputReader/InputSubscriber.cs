using UnityEngine;

public class InputSubscriber
{
    private readonly KeyboardInputReader _keyboard;
    private readonly TouchInputReader _joystick;

    private readonly InputEventContainer _event;

    public InputSubscriber(KeyboardInputReader keyboard, TouchInputReader joystick, InputEventContainer eventConteiner)
    {
        _keyboard = keyboard;
        _joystick = joystick;
        _event = eventConteiner;
    }

    public void Subscribe()
    {
        if (Application.isMobilePlatform)
        {
            _joystick.MovementPressed += _event.MovementPressed;
            _joystick.RotationPressed += _event.RotationPressed;
            _joystick.JumpPressed += _event.JumpPressed;
            _joystick.MenuPressed += _event.MenuPressed;
            _joystick.SneackPressed += _event.SneackPressed;
            _joystick.Rised += _event.Rised;
        }
        else
        {
            _keyboard.MovementPressed += _event.MovementPressed;
            _keyboard.RotationPressed += _event.RotationPressed;
            _keyboard.JumpPressed += _event.JumpPressed;
            _keyboard.KeyMenuPressed += _event.MenuPressed;
            _keyboard.SneackPressed += _event.SneackPressed;
            _keyboard.Rised += _event.Rised;
            _keyboard.SlowingStepPressed += _event.SlowingStepPressed;
            _keyboard.RunningStepPressed += _event.RunningStepPressed;
            _keyboard.ShootingPressed += _event.ShootingPressed;
            _keyboard.ShootingUnpressed += _event.ShootingUnpressed;
            _keyboard.BotAddPressed += _event.BotAddPressed;
            _keyboard.BotRemovePressed += _event.BotRemovePressed;
            _keyboard.BotKillPressed += _event.BotKillPressed;
            _keyboard.StatsPressed += _event.StatsPressed;
            _keyboard.StatsUnpressed += _event.StatsUnpressed;
        }
    }

    public void Unsubscribe()
    {
        if (Application.isMobilePlatform)
        {
            _joystick.MovementPressed -= _event.MovementPressed;
            _joystick.RotationPressed -= _event.RotationPressed;
            _joystick.JumpPressed -= _event.JumpPressed;
            _joystick.MenuPressed -= _event.MenuPressed;
            _joystick.SneackPressed -= _event.SneackPressed;
            _joystick.Rised -= _event.Rised;
        }
        else
        {
            _keyboard.MovementPressed -= _event.MovementPressed;
            _keyboard.RotationPressed -= _event.RotationPressed;
            _keyboard.JumpPressed -= _event.JumpPressed;
            _keyboard.KeyMenuPressed -= _event.MenuPressed;
            _keyboard.SneackPressed -= _event.SneackPressed;
            _keyboard.Rised -= _event.Rised;
            _keyboard.SlowingStepPressed -= _event.SlowingStepPressed;
            _keyboard.RunningStepPressed -= _event.RunningStepPressed;
            _keyboard.ShootingPressed -= _event.ShootingPressed;
            _keyboard.ShootingUnpressed -= _event.ShootingUnpressed;
            _keyboard.BotAddPressed -= _event.BotAddPressed;
            _keyboard.BotRemovePressed -= _event.BotRemovePressed;
            _keyboard.BotKillPressed -= _event.BotKillPressed;
            _keyboard.StatsPressed -= _event.StatsPressed;
            _keyboard.StatsUnpressed -= _event.StatsUnpressed;
        }
    }
}