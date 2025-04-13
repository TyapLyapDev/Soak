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
        public const float MouseDragSensitivity = 3f;
        public const float TouchDragSensitivity = 300f;
    }

    public static class Character
    {
        public const float MinimumVerticalRotationAngle = - 90f;
        public const float MaximumVerticalRotationAngle = 90f;
        public const float SlowingStepMultiplierSpeed = 0.35f;
        public const float SneakingStepMultiplierSpeed = 0.35f;
        public const float MovementSpeed = 7f;
        public const float JumpingForce = 4f;
        public const float Gravity = 10f;
    }

    public static class SaveOptions
    {
        public const string HorizontalRotation = nameof(HorizontalRotation);
        public const string VerticalRotation = nameof(VerticalRotation);
        public const string VolumeGame = nameof(VolumeGame);
        public const string VolumeMusic = nameof(VolumeMusic);
        public const string Lighting = nameof(Lighting);
        public const string AimColorR = nameof(AimColorR);
        public const string AimColorG = nameof(AimColorG);
        public const string AimColorB = nameof(AimColorB);
        public const string AimScale = nameof(AimScale);

        public const float ValueHorizontalRotation = 1.4f;
        public const float ValueVerticalRotation = 1.4f;
        public const float ValueVolumeGame = 90f;
        public const float ValueVolumeMusic = 70f;
        public const float ValueLighting = 40f;
        public const float ValueAimColorR = 0f;
        public const float ValueAimColorG = 1f;
        public const float ValueAimColorB = 0f;
        public const float ValueAimScale = 1f;
    }
}