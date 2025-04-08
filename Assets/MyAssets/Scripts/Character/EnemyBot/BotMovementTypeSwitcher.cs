using UnityEngine;

public enum MovementType
{
    NoMovement,
    NoMovementAndSneaking,
    Walking,
    Running,
    Sneacking,
}

public class BotMovementTypeSwitcher
{
    private readonly Vector2 _durationLimits = new(2f, 5f);
    private MovementType _currentType = MovementType.Walking;
    private float _timer;
    private float _duration;

    public MovementType GetCurrentType()
    {
        _timer += Time.deltaTime;

        if (_timer >= _duration)
        {
            _timer = 0f;
            _duration = Random.Range(_durationLimits.x, _durationLimits.y);
            SetNewRandomType();
        }

        return _currentType;
    }

    public void SetNewRandomType()
    {
        _currentType = (MovementType)Random.Range(0, System.Enum.GetValues(typeof(MovementType)).Length);
        Debug.Log($"MovementType = {_currentType}");
    }
}