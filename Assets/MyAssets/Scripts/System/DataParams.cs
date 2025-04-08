public static class DataParams
{
    public static class SceneNames
    {
        public const string Menu = nameof(Menu);
        public const string Game = nameof(Game);
    }

    public static class Animator
    {
        public const string IsSneaking = nameof(IsSneaking);
        public const string RightMoving = nameof(RightMoving);
        public const string ForwardMoving = nameof(ForwardMoving);
        public const string Jump = nameof(Jump);
    }

    public static class Inputs
    {
        public const float MouseDragSensitivity = 0.8f;
        public const float TouchDragSensitivity = 8f;
    }

    public static class Character
    {
        public const float MinimumVerticalRotationAngle = - 90f;
        public const float MaximumVerticalRotationAngle = 90f;
        public const float SlowingStepMultiplierSpeed = 0.5f;
        public const float SneakingStepMultiplierSpeed = 0.35f;
        public const float MovementSpeed = 7f;
        public const float JumpingForce = 4f;
        public const float Gravity = 10f;
    }
}