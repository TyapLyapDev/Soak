using UnityEngine;

public class Gravity
{
    private CharacterController _characterController;
    private float _baseGravity;
    private float _velocity;
    private float _maximumVelocityY;

    public Gravity(CharacterController characterController , float baseGravity)
    {
        _characterController = characterController;
        _baseGravity = baseGravity;
    }

    public float GetUpdateVelocity()
    {
        if (_characterController.isGrounded && _velocity != _maximumVelocityY)
            _velocity = -0.001f;
        else
            _velocity -= _baseGravity * Time.deltaTime;

        return _velocity;
    }

    public void SetVerticalVelocity(float value)
    {
        _velocity = value;
        _maximumVelocityY = value;
    }
}