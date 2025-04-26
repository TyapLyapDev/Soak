using System;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class CharacterHintDisplay : MonoBehaviour
{
    [SerializeField] private Player _player;

    private TextMeshProUGUI _hint;
    private CharacterDetector _characterDetector;
    private Character _lastCharacter;
    private float _lastHealth;

    private void Awake() =>
        _hint = GetComponent<TextMeshProUGUI>();        

    private void Start()
    {
        _characterDetector = _player.CharacterDetector;
        _hint.text = string.Empty;

        _characterDetector.Detected += OnDetected;
        _characterDetector.Undetected += OnUndetected;
    }

    private void OnDestroy()
    {
        if (_characterDetector == null)
            return;

        _characterDetector.Detected -= OnDetected;
        _characterDetector.Undetected -= OnUndetected;
    }

    private void OnDetected(Character character)
    {
        if (_lastCharacter == character && Mathf.Approximately(_lastHealth, character.Health))
            return;

        _lastCharacter = character;
        _lastHealth = character.Health;
        Show();
    }

    private void OnUndetected()
    {
        if (_lastCharacter != null)
        {
            _lastCharacter = null;
            _hint.text = string.Empty;
        }
    }

    private void Show()
    {
        string attirudeTeam = GetAttitude();
        string healt = $"{_lastCharacter.Health:F0}";

        _hint.text = $"{attirudeTeam}{_lastCharacter.Name} Здоровье : {healt}%";
        _hint.color = TeamColors.Instance.Get(_lastCharacter.Team);
    }

    private string GetAttitude()
    {
        if (_lastCharacter == null)
            throw new ArgumentNullException($"Исключение нулевой ссылки _lastCharacter");

        return _lastCharacter.Team == _player.Team && _lastCharacter.Team != TeamType.AgainstEveryone ? 
            DataParams.Texts.HintFriendTeam :
            DataParams.Texts.HintEnemyTeam;
    }
}