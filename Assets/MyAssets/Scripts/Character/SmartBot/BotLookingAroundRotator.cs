using UnityEngine;

public class BotLookingAroundRotator
{
    private const float Deviation = 0.1f;

    private readonly Transform _horizontal;
    private readonly Transform _vertical;
    private readonly Vector2 _speedLimits = new(2f, 8f);
    private readonly Vector2 _horizontalAngleLimits = new(0, 180f);
    private readonly Vector2 _durationLimits = new(2f, 4f);

    private Quaternion _targetHorizontalRotation;
    private Quaternion _targetVerticalRotation;

    private float _currentSpeed;
    private float _lookTimer;
    private float _duration;

    public BotLookingAroundRotator(Transform horizontal, Transform vertical)
    {
        _horizontal = horizontal;
        _vertical = vertical;
    }

    public void StartRotation()
    {
        _lookTimer = 0f;
        _currentSpeed = Random.Range(_speedLimits.x, _speedLimits.y);
        _duration = Random.Range(_durationLimits.x, _durationLimits.y);

        float horizontalAngle = Random.Range(_horizontalAngleLimits.x, _horizontalAngleLimits.y);
        float verticalAngle = Random.Range(DataParams.Character.MinimumVerticalRotationAngle, DataParams.Character.MaximumVerticalRotationAngle);

        _targetHorizontalRotation = Quaternion.Euler(0, horizontalAngle, 0);
        _targetVerticalRotation = Quaternion.Euler(verticalAngle, 0, 0);
    }

    public void UpdateRotation()
    {
        bool isRotateHorizontal = IsRotateHorizontal();
        bool isRotateVertical = IsRotateVertical();

        if (isRotateHorizontal == false && isRotateVertical == false)
        {
            StartRotation();
            return;
        }

        _lookTimer += Time.deltaTime;

        if (_lookTimer >= _duration)
            StartRotation();
    }        

    private bool IsRotateHorizontal()
    {
        _horizontal.rotation = Quaternion.Slerp(_horizontal.rotation, _targetHorizontalRotation, _currentSpeed * Time.deltaTime);

        return Quaternion.Angle(_horizontal.rotation, _targetHorizontalRotation) > Deviation;
    }

    private bool IsRotateVertical()
    {
        _vertical.localRotation = Quaternion.Slerp(_vertical.localRotation, _targetVerticalRotation, _currentSpeed * Time.deltaTime);

        return Quaternion.Angle(_vertical.localRotation, _targetVerticalRotation) < Deviation;
    }
}