using System;
using UnityEngine;

public class KeyBoardInputReader : MonoBehaviour
{
    private const float SlowStepMultiplier = 0.6f;
    private const float SneakingMultiplier = 0.35f;
    private const float NormalizeMultiplier = 0.5f;
    private const float MouseSensitivity = 0.2f;

    [SerializeField] private float _rotationSensitivity;

    private const string Horizontal = "Horizontal";
    private const string Vertical = "Vertical";
    private const string MouseX = "Mouse X";
    private const string MouseY = "Mouse Y";
    private const KeyCode Jumping = KeyCode.Space;
    private const KeyCode SLowStep = KeyCode.LeftShift;
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

    public bool IsSlowStep() =>
        Input.GetKey(SLowStep);

    public bool IsSneaking() =>
        Input.GetKey(Sneaking);

    public Vector2 GetMovement()
    {
        Vector2 direction = new(Input.GetAxisRaw(Horizontal), Input.GetAxisRaw(Vertical));

        if (direction.x != 0 && direction.y != 0)
            direction *= NormalizeMultiplier;

        if (IsSneaking())
            direction *= SneakingMultiplier;
        else if (IsSlowStep())
            direction *= SlowStepMultiplier;

        return direction;
    }

    private void ReadKeyRotation()
    {
        Vector2 mouseDelta = new(Input.GetAxis(MouseX), Input.GetAxis(MouseY));
        mouseDelta *= _rotationSensitivity;

        if (mouseDelta != Vector2.zero)
            RotationPressed?.Invoke(mouseDelta * MouseSensitivity);
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