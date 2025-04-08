using UnityEngine;

public class DeltaMovementCalculator
{
    private readonly Transform _transform;
    private Vector3 _previousPosition;
    private Vector3 _lastDelta;

    public DeltaMovementCalculator(Transform transform)
    {
        _transform = transform;
        _previousPosition = _transform.position;
    }

    public Vector2 GetNormalizedDelta(float movementSpeed)
    {
        UpdateDelta();
        float distancePerFrame = movementSpeed * Time.deltaTime;

        return distancePerFrame == 0 ? Vector2.zero : _lastDelta /distancePerFrame;
    }

    private void UpdateDelta()
    {
        Vector3 worldDelta = _transform.position - _previousPosition;
        _lastDelta = _transform.InverseTransformDirection(worldDelta);
        _previousPosition = _transform.position;
    }    
}