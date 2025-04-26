using System.Collections;
using UnityEngine;

public class SpatialAnomaliesSource : MonoBehaviour
{
    [Header("Настройки аномалии")]
    [SerializeField] private Vector2 _speedRange;
    [SerializeField] private Vector2 _scaleDurationRange;
    [SerializeField] private Vector2 _pauseDurationRange;

    private Vector3 _originalScale;
    private Vector3 _currentAxis;
    private Coroutine _animationCoroutine;

    private void Start() =>
        _originalScale = transform.localScale;

    public void StartAnimation()
    {
        if (_animationCoroutine != null)
            StopCoroutine(_animationCoroutine);

        _animationCoroutine = StartCoroutine(AnimateScale());
    }

    private IEnumerator AnimateScale()
    {
        while (true)
        {
            _currentAxis = GetRandomAxis();

            float speed = Random.Range(_speedRange.x, _speedRange.y);
            float scaleDuration = Random.Range(_scaleDurationRange.x, _scaleDurationRange.y);

            yield return ScaleOverTime(_currentAxis, speed, scaleDuration);

            yield return new WaitForSeconds(Random.Range(_pauseDurationRange.x, _pauseDurationRange.y));

            yield return ResetScale(_currentAxis, speed);
        }
    }

    private Vector3 GetRandomAxis()
    {
        int axis = Random.Range(0, 3);
        return axis switch
        {
            0 => Vector3.right,
            1 => Vector3.up,
            _ => Vector3.forward
        };
    }

    private IEnumerator ScaleOverTime(Vector3 axis, float speed, float duration)
    {
        float timer = 0f;
        Vector3 startScale = transform.localScale;
        Vector3 targetScale = startScale + axis * Random.Range(0.5f, 2f);

        while (timer < duration)
        {
            timer += Time.deltaTime * speed;
            transform.localScale = Vector3.Lerp(startScale, targetScale, timer / duration);
            yield return null;
        }
    }

    private IEnumerator ResetScale(Vector3 axis, float speed)
    {
        float timer = 0f;
        Vector3 startScale = transform.localScale;
        Vector3 targetScale = new(
            axis.x == 0 ? startScale.x : _originalScale.x,
            axis.y == 0 ? startScale.y : _originalScale.y,
            axis.z == 0 ? startScale.z : _originalScale.z
        );

        while (transform.localScale != targetScale)
        {
            timer += Time.deltaTime * speed;
            transform.localScale = Vector3.Lerp(startScale, targetScale, timer);
            yield return null;
        }
    }

    private void OnDisable()
    {
        if (_animationCoroutine != null)
            StopCoroutine(_animationCoroutine);
    }
}