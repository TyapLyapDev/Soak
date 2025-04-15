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
    private readonly Rutine _rutine;
    private readonly Vector2 _durationLimits = new(2f, 5f);
    private LookingType _currentType;
    private float _timer;
    private float _duration;

    public BotRotationTypeSwitcher(MonoBehaviour mono)
    {
        _rutine = new(mono, UpdateInfo);

        SetNewRandomType();
        _rutine.Start();
    }

    public LookingType GetCurrentType => _currentType;

    private void UpdateInfo()
    {
        _timer += Time.deltaTime;

        if (_timer >= _duration)
        {
            _timer = 0f;
            _duration = Random.Range(_durationLimits.x, _durationLimits.y);
            SetNewRandomType();
        }
    }

    public void SetNewRandomType() =>
        _currentType = (LookingType)Random.Range(0, System.Enum.GetValues(typeof(LookingType)).Length);
}