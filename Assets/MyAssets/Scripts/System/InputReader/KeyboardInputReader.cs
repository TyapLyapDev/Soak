using System;
using UnityEngine;

public class KeyboardInputReader : MonoBehaviour
{
    private const string Horizontal = "Horizontal";
    private const string Vertical = "Vertical";
    private const string MouseX = "Mouse X";
    private const string MouseY = "Mouse Y";

    private const KeyCode Jumping = KeyCode.Space;
    private const KeyCode SLowingStep = KeyCode.LeftShift;
    private const KeyCode Sneaking = KeyCode.LeftControl;
    private const KeyCode Escape = KeyCode.Escape;

    public event Action<Vector2> MovementPressed;
    public event Action<Vector2> RotationPressed;
    public event Action JumpPressed;
    public event Action SneackPressed;
    public event Action Rised;
    public event Action SlowingStepPressed;
    public event Action RunningStepPressed;
    public event Action KeyMenuPressed;

    private void Update()
    {
        ReadKeyMovement();
        ReadKeyJump();
        ReadKeyRotation();
        ReadKeyMenu();
        ReadKeySneaking();
        ReadKeySlowingStep();
    }

    private void ReadKeyMovement()
    {
        Vector2 direction = new(Input.GetAxisRaw(Horizontal), Input.GetAxisRaw(Vertical));
        direction = direction.normalized;

        MovementPressed?.Invoke(direction);
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

    private void ReadKeySneaking()
    {
        if (Input.GetKeyDown(Sneaking))
            SneackPressed?.Invoke();
        else if (Input.GetKeyUp(Sneaking))
            Rised?.Invoke();
    }

    private void ReadKeySlowingStep()
    {
        if (Input.GetKeyDown(SLowingStep))
            SlowingStepPressed?.Invoke();
        else if (Input.GetKeyUp(SLowingStep))
            RunningStepPressed?.Invoke();
    }
}