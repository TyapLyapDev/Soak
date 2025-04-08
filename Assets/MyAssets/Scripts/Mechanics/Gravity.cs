using UnityEngine;

public class Gravity
{
    private readonly CharacterController _characterController;

    private float _maximumVerticalVelocity;
    private float _currentVerticalVelocity;

    public Gravity(CharacterController characterController)
    {
        _characterController = characterController;
    }

    public float GetUpdateVelocity()
    {
        UpdateVelosity();        

        return _currentVerticalVelocity;
    }

    private void UpdateVelosity()
    {
        bool isGrounded = _characterController.isGrounded && _currentVerticalVelocity != _maximumVerticalVelocity;
        _currentVerticalVelocity -= isGrounded ? Mathf.Epsilon : DataParams.Character.Gravity * Time.deltaTime;
    }

    public void SetVerticalVelocity(float value)
    {
        _currentVerticalVelocity = value;
        _maximumVerticalVelocity = value;
    }
}