using System;
using System.Collections;
using UnityEngine;

public class PlayerModelConfigurations
{
    private const float DelayAfterDeadInSeconds = 3f;

    private readonly MapRotator _map;
    private readonly CameraFollower _cameraFollower;
    private readonly RequestCameraTarget _requestCameraTarget;
    private readonly SoulModel _cameraDisplayBody;
    private readonly PhysicalModel _physicalBody;
    private readonly CharacterGeo _physicalMesh;
    private readonly Weapon _weaponInPhysicalBody;
    private readonly PlayerSoul _soul = new();
    private readonly Player _player;

    private readonly MonoBehaviour _monoBehaviour;
    private readonly WaitForSeconds _waitAfterDead;

    private readonly Transform _camera;

    public event Action DeadShowed;

    public PlayerModelConfigurations(Player player)
    {
        _map = player.GetComponentInParent<MapRotator>();

        _waitAfterDead = new(DelayAfterDeadInSeconds);
        _monoBehaviour = player.GetComponent<MonoBehaviour>();
        _player = player;

        _camera = Camera.main.transform;

        _physicalBody = player.GetComponentInChildren<PhysicalModel>(true);

        if (_physicalBody == null)
            throw new NullReferenceException($"Не найден компонент PhysicalModel в иерархии {player.name}");

        _cameraDisplayBody = player.GetComponentInChildren<SoulModel>(true);

        if (_cameraDisplayBody == null)
            throw new NullReferenceException($"Не найден компонент SoulModel в иерархии {player.name}");

        if (_camera.TryGetComponent(out _cameraFollower) == false)
            throw new NullReferenceException($"Не найден компонент CameraFollower на объекте {_camera.name}");

        _requestCameraTarget = player.GetComponentInChildren<RequestCameraTarget>(true);

        if (_requestCameraTarget == null)
            throw new NullReferenceException($"Не найден компонент RequestCameraTarget в иерархии {_physicalBody.transform.name}");

        _weaponInPhysicalBody = _physicalBody.transform.GetComponentInChildren<Weapon>(true);

        if (_weaponInPhysicalBody == null)
            throw new NullReferenceException($"Не найден компонент Weapon в иерархии {_physicalBody.transform.name}");

        _physicalMesh = _physicalBody.transform.GetComponentInChildren<CharacterGeo>(true);

        if (_physicalMesh == null)
            throw new NullReferenceException($"Не найден компонент CharacterGeo в иерархии {_physicalBody.transform.name}");
    }

    public void ProcessDied(Character killer)
    {
        _cameraFollower.ReparentInPhysisModel();
        _cameraDisplayBody.gameObject.SetActive(false);
        _physicalMesh.gameObject.SetActive(true);
        _weaponInPhysicalBody.gameObject.SetActive(true);

        _monoBehaviour.StartCoroutine(EnableMovementSoulAfterTime(killer));
    }

    public void ProcessResurrect()
    {
        _camera.parent = _player.transform;

        _camera.SetPositionAndRotation(
            _requestCameraTarget.transform.position,
            _requestCameraTarget.transform.rotation);

        _cameraFollower.enabled = true;
        _cameraDisplayBody.gameObject.SetActive(true);
        _physicalMesh.gameObject.SetActive(false);
        _weaponInPhysicalBody.gameObject.SetActive(false);
        _soul.DisableMovementContol();
    }

    public void DisableControl()
    {
        _cameraFollower.enabled = false;
        _cameraDisplayBody.gameObject.SetActive(false);
        _physicalMesh.gameObject.SetActive(false);
        _weaponInPhysicalBody.gameObject.SetActive(false);
        _soul.DisableMovementContol();
    }

    public void LeaveOnlySoul()
    {
        _physicalBody.gameObject.SetActive(false);
        _cameraDisplayBody.gameObject.SetActive(false);
        _physicalMesh.gameObject.SetActive(false);
        _weaponInPhysicalBody.gameObject.SetActive(false);
        _soul.EnableMovementContol();
    }

    private IEnumerator EnableMovementSoulAfterTime(Character killer)
    {
        yield return _waitAfterDead;

        if (_player.IsDead == false)
            yield break;

        _soul.EnableMovementContol();
        _camera.parent = _map.transform;

        if (killer != null)
        {
            float backwardOffset = 1f;
            float verticalOffset = 0.5f;

            Vector3 backDirection = -killer.transform.forward;

            Vector3 cameraPosition = killer.Center.position
                + backDirection * backwardOffset
                + killer.transform.up * verticalOffset;

            Vector3 lookDirection = killer.Center.position - cameraPosition;

            _camera.SetPositionAndRotation(cameraPosition, Quaternion.LookRotation(lookDirection.normalized));
        }
        else
        {
            Vector3 currentRotation = _camera.rotation.eulerAngles;
            currentRotation.x = 0f;
            currentRotation.z = 0f;
            _camera.rotation = Quaternion.Euler(currentRotation);

            Vector3 currentPosition = _camera.position;
            currentPosition.y += 1f;

            _camera.SetLocalPositionAndRotation(currentPosition, Quaternion.Euler(currentRotation));
        }

        DeadShowed?.Invoke();
    }
}