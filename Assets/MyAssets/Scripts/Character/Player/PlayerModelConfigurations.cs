using System;
using UnityEngine;

public class PlayerModelConfigurations
{
    private readonly CameraFollower _cameraFollower;
    private readonly RequestCameraTarget _requestCameraTarget;
    private readonly SoulModel _cameraDisplayBody;
    private readonly PhysicalModel _physicalBody;
    private readonly CharacterGeo _physicalMesh;
    private readonly Weapon _weaponInPhysicalBody;
    private readonly PlayerSoul _soul = new();

    private readonly Transform _camera;

    public PlayerModelConfigurations(Transform player)
    {
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

        ProcessResurrect();
    }

    public void ProcessDied()
    {
        _cameraFollower.enabled = false;
        _cameraDisplayBody.gameObject.SetActive(false);
        _physicalMesh.gameObject.SetActive(true);
        _weaponInPhysicalBody.gameObject.SetActive(true);
        _soul.EnableMovementContol();
    }

    public void ProcessResurrect()
    {
        _camera.SetPositionAndRotation(
            _requestCameraTarget.transform.position,
            _requestCameraTarget.transform.rotation);

        _cameraFollower.enabled = true;
        _cameraDisplayBody.gameObject.SetActive(true);
        _physicalMesh.gameObject.SetActive(false);
        _weaponInPhysicalBody.gameObject.SetActive(false);
        _soul.DisableMovementContol();
    }
}