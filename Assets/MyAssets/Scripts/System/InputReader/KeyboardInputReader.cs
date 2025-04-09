using System;
using UnityEngine;

public class KeyboardInputReader : MonoBehaviour
{    
    private const float DiagonalStepNormalizationMultiplier = 0.5f;
    private const string Horizontal = "Horizontal";
    private const string Vertical = "Vertical";
    private const string MouseX = "Mouse X";
    private const string MouseY = "Mouse Y";

    private const KeyCode Jumping = KeyCode.Space;
    private const KeyCode SLowingStep = KeyCode.LeftShift;
    private const KeyCode Sneaking = KeyCode.LeftControl;
    private const KeyCode Escape = KeyCode.Escape;

    public event Action<Vector2> RotationPressed;
    public event Action JumpPressed;
    public event Action KeyMenuPressed;

    private void Update()
    {
        ReadKeyJump();
        ReadKeyRotation();
        ReadKeyMenu();
    }

    public bool IsSlowingStep() =>
        Input.GetKey(SLowingStep);

    public bool IsSneaking() =>
        Input.GetKey(Sneaking);

    public Vector2 GetMovement()
    {
        Vector2 direction = new(Input.GetAxisRaw(Horizontal), Input.GetAxisRaw(Vertical));

        if (direction.x != 0 && direction.y != 0)
            direction *= DiagonalStepNormalizationMultiplier;

        if (IsSneaking())
            direction *= DataParams.Character.SneakingStepMultiplierSpeed;
        else if (IsSlowingStep())
            direction *= DataParams.Character.SlowingStepMultiplierSpeed;

        return direction;
    }

    private void ReadKeyRotation()
    {
        Vector2 mouseDelta = new(Input.GetAxis(MouseX), Input.GetAxis(MouseY));

        if (mouseDelta != Vector2.zero)
            RotationPressed?.Invoke(mouseDelta * DataParams.Inputs.MouseDragSensitivity);
    }

    private void ReadKeyJump()
    {
        if (Input.GetKeyDown(Jumping))
            JumpPressed?.Invoke();
    }
    private void ReadKeyMenu()
    {
        if (Input.GetKeyDown(Escape))
            KeyMenuPressed?.Invoke();
    }
}