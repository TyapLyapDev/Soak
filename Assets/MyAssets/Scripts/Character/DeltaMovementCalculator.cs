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

    private Vector3 UpdateDelta()
    {
        Vector3 worldDelta = _transform.position - _previousPosition;
        _lastDelta = _transform.InverseTransformDirection(worldDelta);
        _previousPosition = _transform.position;

        return _lastDelta;
    }

    public Vector2 GetNormalizedDelta(float movementSpeed)
    {
        UpdateDelta();
        Vector3 delta = _lastDelta / (movementSpeed * Time.deltaTime + Mathf.Epsilon);

        return new(delta.x, delta.z);
    }
}