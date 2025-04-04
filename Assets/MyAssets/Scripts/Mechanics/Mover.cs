using UnityEngine;

public class Mover
{
    private readonly CharacterController _characterController;
    private readonly Gravity _gravity;
    private readonly float _speed;

    public Mover(CharacterController characterController, float baseGravity, float speed)
    {
        _characterController = characterController;
        _gravity = new(characterController, baseGravity);
        _speed = speed;
    }

    public void Move(Vector2 direction)
    {
        direction *= _speed;
        Vector3 localDirection = new(direction.x, _gravity.GetUpdateVelocity(), direction.y);
        localDirection = _characterController.transform.TransformDirection(localDirection);
        _characterController.Move(localDirection * Time.deltaTime);
    }

    public void SetVerticalVelocity(float velocity) =>
        _gravity.SetVerticalVelocity(velocity);
}