using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class WaterFlowTextureShifter : MonoBehaviour
{
    private const float SpeedX = 0.03f;
    private const float SpeedY = 0.01f;

    private Material _material;

    private void Awake() =>
        _material = GetComponent<Renderer>().material;

    private void Update()
    {
        if (_material != null)
        {
            Vector2 offset = _material.mainTextureOffset;

            if (offset.x >= 1000)
                _material.mainTextureOffset = Vector2.zero;

            offset.x += SpeedX * Time.deltaTime;
            offset.y += SpeedY * Time.deltaTime;

            _material.mainTextureOffset = offset;
        }
    }
}