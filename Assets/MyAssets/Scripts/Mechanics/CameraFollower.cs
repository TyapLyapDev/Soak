using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    private const float Speed = 10f;

    [SerializeField] private CameraTarget _cameraTarget;

    private Transform _target;

    private void Awake() =>
        _target = _cameraTarget.transform;

    private void LateUpdate()
    {
        Vector3 tempPosition = transform.position;
        tempPosition.y = Mathf.Lerp(tempPosition.y, _target.position.y, Speed * Time.deltaTime);
        transform.position = tempPosition;
    }
}