using UnityEngine;

public class PoolWithoutWater : MonoBehaviour
{
    [SerializeField] private Transform[] _transforms;
    [SerializeField] private float _hideAngle = 60f;

    private void Update()
    {
        if (DataParams.SaveOptions.IsGravitationalAnomaliesChecked == false) 
            return;

        float angleDifference = Vector3.Angle(transform.up, Vector3.up);

        if (angleDifference >= _hideAngle)
            DestryTransforms();
    }

    private void DestryTransforms()
    {
        foreach (Transform transformToDestroy in _transforms)
            if (transformToDestroy != null)
                Destroy(transformToDestroy.gameObject);

        Destroy(this);
    }
}