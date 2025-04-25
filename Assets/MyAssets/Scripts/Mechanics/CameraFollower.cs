using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    private const float Speed = 10f;

    [SerializeField] private CameraHeightTarget _cameraTarget;
    [SerializeField] private GazeDirection _gazDirection;

    private Transform _target;

    private void Awake() =>
        _target = _cameraTarget.transform;

    private void LateUpdate()
    {
        RotateRig();
        MaintainHeight();
    }

    public void ReparentInPhysisModel()
    {
        transform.parent = _target;
        transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
        enabled = false;
    }

    private void RotateRig() =>
        _gazDirection.transform.rotation = transform.rotation;

    private void MaintainHeight()
    {
        Vector3 tempPosition = transform.position;
        tempPosition.y = Mathf.Lerp(tempPosition.y, _target.position.y, Speed * Time.deltaTime);
        transform.position = tempPosition;
    }
}