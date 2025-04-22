using UnityEngine;

public class SoulRotator : MonoBehaviour
{
    [SerializeField] InputInformer _informer;

    private float _currentYAngle = 0f;

    private void OnEnable() =>
        _informer.RotationPressed += OnRotationPressed;

    private void OnDisable() =>
        _informer.RotationPressed -= OnRotationPressed;

    private void OnRotationPressed(Vector2 inputs)
    {
        float rotationX = inputs.x;
        float rotationY = inputs.y;

        transform.Rotate(Vector3.up, rotationX, Space.World);

        _currentYAngle -= rotationY;
        _currentYAngle = Mathf.Clamp(
            _currentYAngle, 
            DataParams.Character.MinimumVerticalRotationAngle, 
            DataParams.Character.MaximumVerticalRotationAngle);

        transform.localEulerAngles = new Vector3(_currentYAngle, transform.localEulerAngles.y, 0);
    }
}