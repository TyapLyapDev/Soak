using UnityEngine;

public class SprayFromWallShower : MonoBehaviour
{
    [SerializeField] private CharacterManager _characterManager;
    [SerializeField] private Spray _sprayPrefab;
    [SerializeField] private Puddle _puddlePrefab;

    private Pool<Spray> _sprayPool;
    private Pool<Puddle> _puddlePool;
    private CharacterRegistrator _registrator;

    private void Awake()
    {
        _sprayPool = new(_sprayPrefab, transform);
        _puddlePool = new(_puddlePrefab, transform);
    }

    private void Start()
    {
        _registrator = _characterManager.Registrator;

        _registrator.Registered += OnCharacterRegistered;
        _registrator.Deregistered += OnCharacterDeregistered;
    }

    private void OnCharacterRegistered(Character character) =>
        character.Jet.Collided += OnCollision;
    
    private void OnCharacterDeregistered(Character character) =>
        character.Jet.Collided -= OnCollision;

    private void OnCollision(Vector3 position, Quaternion rotation)
    {
        if (_sprayPool.TryGet(out Spray spray))
            spray.transform.SetPositionAndRotation(position, rotation);

        if (_puddlePool.TryGet(out Puddle puddle))
            puddle.transform.SetPositionAndRotation(position, rotation);
    }
}