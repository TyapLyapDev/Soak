using UnityEngine;

public class SprayFromWallShower : MonoBehaviour
{
    [SerializeField] private Spray _sprayPrefab;
    [SerializeField] private Puddle _puddlePrefab;

    private Pool<Spray> _sprayPool;
    private Pool<Puddle> _puddlePool;

    private void Awake()
    {
        _sprayPool = new(_sprayPrefab, transform);
        _puddlePool = new(_puddlePrefab, transform);
    }

    public void Subscribe(WaterJet jet) =>
        jet.Collided += OnCollision;

    public void Unsubscribe(WaterJet jet) =>
        jet.Collided -= OnCollision;

    private void OnCollision(Vector3 position, Quaternion rotation)
    {
        if (_sprayPool.TryGet(out Spray spray))
            spray.transform.SetPositionAndRotation(position, rotation);

        if (_puddlePool.TryGet(out Puddle puddle))
            puddle.transform.SetPositionAndRotation(position, rotation);
    }
}