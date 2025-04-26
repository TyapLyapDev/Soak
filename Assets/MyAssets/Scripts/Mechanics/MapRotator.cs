using UnityEngine;

public class MapRotator : MonoBehaviour
{
    [SerializeField] private Vector2 _speedLimits;
    [SerializeField] private Vector2 _durationLimits;

    private Vector3 _rotationAxis;
    private float _currentSpeed;
    private float _duration;
    private float _timer;

    private void Start() =>
        GenerateNewRotationParams();

    private void Update()
    {
        if (DataParams.SaveOptions.IsGravitationalAnomaliesChecked == false)
            return;

        Rotate();
        UpdateRotationCycle();
    }

    private void Rotate() =>
        transform.Rotate(_rotationAxis, _currentSpeed * Time.deltaTime, Space.World);

    private void UpdateRotationCycle()
    {
        _timer += Time.deltaTime;

        if (_timer < _duration)
            return;

        GenerateNewRotationParams();
        _timer = 0f;
    }

    private void GenerateNewRotationParams()
    {
        _rotationAxis = new Vector3(
            Random.Range(-1f, 1f),
            Random.Range(-1f, 1f),
            Random.Range(-1f, 1f)
        ).normalized;

        _currentSpeed = Random.Range(_speedLimits.x, _speedLimits.y);
        _duration = Random.Range(_durationLimits.x, _durationLimits.y);
    }
}