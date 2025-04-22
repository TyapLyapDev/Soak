using UnityEngine;
using System;

public class CharacterView : MonoBehaviour
{
    private CharacterAnimatorWrapper _characterAnimatorWrapper;
    private EventAnimation _eventAnimation;

    public event Action Stepped;

    private void Awake()
    {
        _characterAnimatorWrapper = GetComponentInChildren<CharacterAnimatorWrapper>(true);

        if (_characterAnimatorWrapper == null)
            throw new NullReferenceException($"Не найден компонент CharacterAnimatorWrapper в иерархии {transform.name}");

        _eventAnimation = GetComponentInChildren<EventAnimation>(true);

        if (_eventAnimation == null)
            throw new NullReferenceException($"Не найден компонент EventAnimation в иерархии {transform.name}");

        _eventAnimation.Stepped += OnStepped;
    }

    public void UpdateMovementAnimation(Vector2 movementAnimation) =>
        _characterAnimatorWrapper.UpdateMovement(movementAnimation);

    public void PlaySneacking() =>
        _characterAnimatorWrapper.PlaySneacking();

    public void PlayRising() =>
        _characterAnimatorWrapper.PlayRising();

    public void EnableAnimator() =>
        _characterAnimatorWrapper.EnableAnimator();

    public void DisableAnimator() =>
        _characterAnimatorWrapper.DisableAnimator();

    private void OnStepped() =>
        Stepped?.Invoke();
}