using UnityEngine;

public class BotForwardRotator
{
    private readonly Vector2 _speedLimits = new(2f, 8f);

    private readonly Transform _horizontal;
    private readonly Transform _vertical;

    private Vector3 _lastMovementDirection;
    private float _currentSpeed;

    public BotForwardRotator(Transform horizontalTransform, Transform verticalTransform)
    {
        _horizontal = horizontalTransform;
        _vertical = verticalTransform;
    }

    public void StartRotation() =>
        _currentSpeed = Random.Range(_speedLimits.x, _speedLimits.y);

    public void UpdateLastMovementDirection(Vector3 direction) =>
        _lastMovementDirection = direction;

    public void UpdateRotation()
    {
        RotateHorizontal();
        ResetVerticalRotation();
    }

    private void RotateHorizontal()
    {
        if (_lastMovementDirection == Vector3.zero) 
            return;

        Quaternion targetRotation = Quaternion.LookRotation(_lastMovementDirection);
        _horizontal.rotation = Quaternion.Slerp(_horizontal.rotation, targetRotation, _currentSpeed * Time.deltaTime);
    }

    private void ResetVerticalRotation() =>
        _vertical.localRotation = Quaternion.Slerp(_vertical.localRotation, Quaternion.identity, _currentSpeed * Time.deltaTime);
}