public class Jumper
{
    private readonly Mover _mover;    
    private readonly float _jumpForce;

    public Jumper(Mover mover, float jumpForce)
    {
        _mover = mover;
        _jumpForce = jumpForce;
    }

    public void Jump() =>
        _mover.SetVerticalVelocity(_jumpForce);
}