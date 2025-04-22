using UnityEngine;

public class SoulMover : MonoBehaviour
{
    [SerializeField] private InputInformer _informer;

    private void OnEnable() =>
        _informer.MovementPressed += OnMovemenetPressed;

    private void OnDisable() =>
        _informer.MovementPressed -= OnMovemenetPressed;

    private void OnMovemenetPressed(Vector2 inputs)
    {
        Vector3 forward = transform.forward;
        Vector3 right = transform.right;

        forward.Normalize();
        right.Normalize();

        Vector3 movement = DataParams.Character.MovementSpeed * Time.deltaTime * (forward * inputs.y + right * inputs.x);
        transform.position += movement;
    }
}