using UnityEngine;

public class SprayFromWallShower : MonoBehaviour
{
    [SerializeField] private WaterJet[] _jets;
    [SerializeField] private Spray _sprayPrefab;
    [SerializeField] private Puddle _puddlePrefab;

    private Pool<Spray> _sprayPool;
    private Pool<Puddle> _puddlePool;

    private void Awake()
    {
        _sprayPool = new(_sprayPrefab, transform);
        _puddlePool = new(_puddlePrefab, transform);
    }

    private void OnEnable()
    {
        foreach (WaterJet jet in _jets)
            jet.Collided += OnCollision;
    }

    private void OnDisable()
    {
        foreach (WaterJet jet in _jets)
            jet.Collided -= OnCollision;
    }

    private void OnCollision(Vector3 position, Quaternion rotation)
    {
        if (_sprayPool.TryGet(out Spray spray))
            spray.transform.SetPositionAndRotation(position, rotation);

        if (_puddlePool.TryGet(out Puddle puddle))
            puddle.transform.SetPositionAndRotation(position, rotation);
    }
}