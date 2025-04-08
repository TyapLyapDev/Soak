using UnityEngine;

public enum LookingType
{
    NoRotation,
    Target,
    Around,
    Forward,
}

public class BotRotationTypeSwitcher
{
    private readonly Vector2 _durationLimits = new(2f, 5f);
    private LookingType _currentType = LookingType.Target;
    private float _timer;
    private float _duration;

    public LookingType GetCurrentType()
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
        _currentType = (LookingType)Random.Range(0, System.Enum.GetValues(typeof(LookingType)).Length);
        Debug.Log($"LookingType = {_currentType}");
    }
}
