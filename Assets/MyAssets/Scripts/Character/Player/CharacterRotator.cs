using UnityEngine;

public class CharacterRotator
{
    private readonly VerticalRotator _verticalRotator;
    private readonly HorizontalRotator _horizontalRotator;

    public CharacterRotator(Transform horizontal, Transform vertical)
    {
        _horizontalRotator = new(horizontal);
        _verticalRotator = new(vertical);
    }

    public void Rotate(Vector2 direction)
    {
        _horizontalRotator.Rotate(direction.x);
        _verticalRotator.Rotate(direction.y);
    }
}