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
    private readonly InputEventContainer _event = new();

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
    public event Action BotAddPressed;
    public event Action BotRemovePressed;
    public event Action BotKillPressed;
    public event Action StatsPressed;
    public event Action StatsUnpressed;

    private void Awake()
    {
        Init();
        _subscriber = new(_keyboard, _joystick, _event);
    }

    private void Init()
    {
        _event.MovementPressed += OnMovementPressed;
        _event.RotationPressed += OnRotationPressed;
        _event.JumpPressed += OnJumpPressed;
        _event.MenuPressed += OnMenuPressed;
        _event.SneackPressed += OnSneackPressed;
        _event.Rised += OnRised;
        _event.SlowingStepPressed += OnSlowingStepPressed;
        _event.RunningStepPressed += OnRunningStepPressed;
        _event.ShootingPressed += OnShootingPressed;
        _event.ShootingUnpressed += OnShootingUnpressed;
        _event.BotAddPressed += OnBotAddPressed;
        _event.BotRemovePressed += OnBotRemovePressed;
        _event.BotKillPressed += OnBotKillPressed;
        _event.StatsPressed += OnStatsPressed;
        _event.StatsUnpressed += OnStatsUnpressed;
    }

    private void OnEnable() =>
        _subscriber.Subscribe();

    private void OnDisable() =>
        _subscriber.Unsubscribe();

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

    private void OnSlowingStepPressed() =>
        SlowingStepPressed?.Invoke();

    private void OnRunningStepPressed() =>
        RunningStepPressed?.Invoke();

    private void OnMenuPressed() =>
        MenuPressed?.Invoke();

    private void OnShootingPressed()
    {
        if (_menuShower.IsShowing)
            return;

        ShootingPressed?.Invoke();
    }

    private void OnShootingUnpressed() =>
        ShootingUnpressed?.Invoke();

    private void OnBotAddPressed() =>
        BotAddPressed?.Invoke();

    private void OnBotRemovePressed() =>
        BotRemovePressed?.Invoke();

    private void OnBotKillPressed() =>
        BotKillPressed?.Invoke();

    private void OnStatsPressed()
    {
        if (_menuShower.IsShowing)
            return;

        StatsPressed?.Invoke();
    }

    private void OnStatsUnpressed() =>
        StatsUnpressed?.Invoke();
}

public class InputEventContainer
{
    public Action<Vector2> MovementPressed;
    public Action<Vector2> RotationPressed;
    public Action JumpPressed;
    public Action MenuPressed;
    public Action SneackPressed;
    public Action Rised;
    public Action SlowingStepPressed;
    public Action RunningStepPressed;
    public Action ShootingPressed;
    public Action ShootingUnpressed;
    public Action BotAddPressed;
    public Action BotRemovePressed;
    public Action BotKillPressed;
    public Action StatsPressed;
    public Action StatsUnpressed;
}