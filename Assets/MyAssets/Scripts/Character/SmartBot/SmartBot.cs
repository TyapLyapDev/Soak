using System.Linq;
using UnityEngine;

public class SmartBot : Character
{
    [SerializeField] private PointToHide[] _pointToHide;
    [SerializeField] private GazeDirection _gazeDirection;

    private readonly BotRotationTypeSwitcher _rotationTypeSwitcher = new();
    private readonly BotMovementTypeSwitcher _movementTypeSwitcher = new();
    private DirectionNormalizer _directionNormalizer;
    private BotTargetSwitcher _targetSwitcher;
    private BotRotatorToTarget _rotatorToTarget;
    private BotLookingAroundRotator _lookingAroundRotator;
    private BotForwardRotator _forwardRotator;

    protected override void Awake()
    {
        base.Awake();
        _directionNormalizer = new(transform);
        _targetSwitcher = new(transform, _pointToHide.Select(t => t.transform).ToArray());
        _rotatorToTarget = new(transform, _gazeDirection.transform);
        _lookingAroundRotator = new(transform, _gazeDirection.transform);
        _forwardRotator = new(transform, _gazeDirection.transform);
    }

    private void Update()
    {
        UpdateMovement();
        UpdateRotation();
    }

    private void OnEnable()
    {
        _targetSwitcher.TargetAchieved += OnTargetAchieved;
        _rotatorToTarget.NoTarget += OnLossTarget;
    }

    private void OnDisable()
    {
        _targetSwitcher.TargetAchieved -= OnTargetAchieved;
        _rotatorToTarget.NoTarget -= OnLossTarget;
    }

    private void OnTargetAchieved() { }

    private void OnLossTarget() =>
        _rotationTypeSwitcher.SetNewRandomType();

    private void UpdateMovement()
    {
        switch (_movementTypeSwitcher.GetCurrentType())
        {
            case MovementType.NoMovement:
                ProcessSwitch(false, 0);
                break;

            case MovementType.NoMovementAndSneaking:
                ProcessSwitch(true, 0);
                break;

            case MovementType.Walking:
                ProcessSwitch(false, 0.4f);
                break;

            case MovementType.Running:
                ProcessSwitch(false, 1);
                break;

            case MovementType.Sneacking:
                ProcessSwitch(true, 0.25f);
                break;
        }        
    }

    private void ProcessSwitch(bool isSneaking, float multiplierSpeed)
    {
        SwitchSneacking(isSneaking);

        Vector3 targetPosition = _targetSwitcher.GetCurrentTargetPosition();
        Vector2 input = _directionNormalizer.NormalizeToMoveOnPlane(targetPosition);

        _forwardRotator.UpdateLastMovementDirection(new(input.x, 0, input.y));
        OnMove(input * multiplierSpeed);
    }

    private void UpdateRotation()
    {
        switch (_rotationTypeSwitcher.GetCurrentType())
        {
            case LookingType.Target:
                _rotatorToTarget.UpdateRotation();
                break;

            case LookingType.Around:
                _lookingAroundRotator.UpdateRotation();
                break;

            case LookingType.Forward:
                _forwardRotator.UpdateRotation();
                break;
        }
    }    
}