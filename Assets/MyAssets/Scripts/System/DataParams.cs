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
        public const float SneackingHeightMultiplier = 0.5f;
        public const float MovementSpeed = 7f;
        public const float JumpingForce = 4f;
        public const float Gravity = 10f;
        public const float MaximumRayDistance = 100f;        
    }

    public static class SaveOptions
    {
        public const string PlayerName = nameof(PlayerName);
        public const string CountBot = nameof(CountBot);
        public const string GravigravitationalAnomalies = nameof(GravigravitationalAnomalies);
        public const string SpatialAnomalies = nameof(SpatialAnomalies);
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
        public const float ValueVolumeGame = 45f;
        public const float ValueVolumeMusic = 30f;
        public const float ValueLighting = 40f;
        public const float ValueAimColorR = 0f;
        public const float ValueAimColorG = 1f;
        public const float ValueAimColorB = 0f;
        public const float ValueAimScale = 1f;

        public static bool IsGravigravitationalAnomaliesChecked = false;
    }

    public static class Texts
    {
        public const string PlayerName = ">>-Стрелок-->";
        public const string CountBot = "9";
        public const int GravigravitationalAnomalies = 0;
        public const int SpatialAnomalies = 0;
        public const string TeamNoName = "Без команды";
        public const string TeamTerroristsName = "Хулиганы";
        public const string TeamCounterTerroristsName = "Защитники";
        public const string TeamObserverName = "Наблюдатели";
        public const string TeamAgainstEveryoneName = "Против всех";
        public const string TextTerroristsWin = "Хулиганы победили!";
        public const string TextCounterTerroristsWin = "Защитники победили!";
        public const string TextCharacterWin = "{0} побеждает всех!";

        public const string HintFriendTeam = "Кореш : ";
        public const string HintEnemyTeam = "Вражина : ";

        public static readonly string[] Names =
        {
            "Скуфидон",
            "Душнидзе",
            "Дотоша",
            "Частоплюй",
            "Засисьник",
            "Мудзилла",
            "Дерьмозавр",
            "Лезбовский",
            "Пепе Ронни",
            "КоJI6acKa",
            "АпЧитер",
            "Гоу-Няшка",
            "Кукумбер",
            "Пивосос",
            "Укурок",
            "Штопанный рыцарь",
            "Ихтиандр",
            "Доктор Ху",
            "Говнодавчик",
            "Полупсих",
            "PSIH",
            "Местный Вася",
            "Вездессущий",
            "Андройд Айосович",
            "Кон Чен Ый",
            "Килька",
            "Жмопс",
            "Весёлый Мясник",
            "Курвабобр",
            "Кузькин Отец",
            "Писюн",
            "Кусок Добра",
            "Джеймс Болт",
            "Криндерман",
            "Шваброид",
            "Дерьминатор",
            "Каллобок",
            "Шаурмагеддон",
            "ТикТокенштейн",
            "Хацкер с лопатой",
            "Абкакалипсис",
            "Милфмен",
            "Аццкий Бздюльтерьер",
            "Лютый токсик",
            "Мы вбежопасности",
            "Альтунец",
            "Пендальф",
            "Жрать Вейдер",
            "Джек-Попрошитель",
            "Чайничек",
            "Пипидастр",
            "Обдолбыш",
            "Эпичный пшик",
            "Дупло_и_бал",
            "Чмонстр",
        };
    }
}