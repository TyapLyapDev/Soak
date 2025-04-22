using System;
using UnityEngine;

public class BotFreeMovement
{
    private readonly Rutine _rutine;
    private readonly ObstacleDetector _obstacleDetector;
    private readonly Vector2 _timeLimits = new(0.5f, 5f);

    private readonly Vector2[] _inputs =
    {
        new (0, 1),
        new (0, -1),
        new (-1, 0),
        new (1, 0),
        new (-0.72f, 0.72f),
        new (0.72f, 0.72f),
        new (-0.72f, -0.72f),
        new (0.72f, -0.72f),
    };

    private float _duration;
    private float _elapsedTime;
    private Vector2 _currentDirection;
    private Vector2 _checkedDirection;

    public event Action JumpOpened;

    public BotFreeMovement(MonoBehaviour mono)
    {
        _rutine = new(mono, Update);
        _obstacleDetector = new(mono.transform);
        SetNewParams();

        _rutine.Start();
        _obstacleDetector.JumpOpened += OnJumpOpened;
    }

    public Vector2 Input => _currentDirection;

    public void Stop() =>
        _rutine.Stop();

    public void Start() =>
        _rutine.Start();

    private void SetNewParams()
    {
        _checkedDirection = _inputs[UnityEngine.Random.Range(0, _inputs.Length)];

        if (_obstacleDetector.IsCanMovement(_checkedDirection))
        {
            _elapsedTime = 0;
            _duration = UnityEngine.Random.Range(_timeLimits.x, _timeLimits.y);
            _currentDirection = _checkedDirection;
            _checkedDirection = _inputs[UnityEngine.Random.Range(0, _inputs.Length)];

            return;
        }

        _currentDirection = Vector2.zero;
    }

    private void Update()
    {
        if (_obstacleDetector.IsCanMovement(_checkedDirection) == false)
            SetNewParams();
        else
            _currentDirection = _checkedDirection;

        _elapsedTime += Time.deltaTime;

        if (_elapsedTime > _duration)
            SetNewParams();
    }

    private void OnJumpOpened() =>
        JumpOpened?.Invoke();
}