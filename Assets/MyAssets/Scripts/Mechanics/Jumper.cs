public class Jumper
{
    private readonly Mover _mover;

    public Jumper(Mover mover)
    {
        _mover = mover;
    }

    public void Jump() =>
        _mover.SetVerticalVelocity(DataParams.Character.JumpingForce);
}