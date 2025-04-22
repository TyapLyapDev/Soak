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
        Start();
    }

    public LookingType GetCurrentType => _currentType;

    public void Start() =>
        _rutine.Start();

    public void Stop() =>
        _rutine.Stop();

    public void SetCurrentType(LookingType type) =>
        _currentType = type;

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