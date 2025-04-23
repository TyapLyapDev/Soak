using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    [SerializeField] private CharacterStatsView _view;

    private Character _character;

    public Character Character => _character;

    private void OnDestroy()
    {
        if (_character != null)
            UnsubscribeFromEvents();
    }

    public void Initialize(Character character)
    {
        _character = character;
        SubscribeToEvents();
        UpdateAllStats();
    }

    public void SubscribeToEvents()
    {
        _character.NameChanged += UpdateName;
        _character.TeamChanged += UpdateColor;
        _character.HealthChanged += UpdateHealth;
        _character.Revived += SetDeathStatus;
        _character.Died += UpdateCountDeath;
        _character.CountKillChanged += UpdateCountKill;
    }

    private void UnsubscribeFromEvents()
    {
        _character.NameChanged -= UpdateName;
        _character.TeamChanged -= UpdateColor;
        _character.HealthChanged -= UpdateHealth;
        _character.Revived -= SetDeathStatus;
        _character.Died -= UpdateCountDeath;
        _character.CountKillChanged -= UpdateCountKill;
    }

    private void UpdateAllStats()
    {
        UpdateColor(_character);
        UpdateName(_character);
        UpdateHealth(_character);
        SetDeathStatus(_character);
        UpdateCountKill(_character);
        UpdateCountDeath(_character);

        _view.Select(_character is Player);
    }

    private void UpdateName(Character character) =>
        _view.UpdateName(character.Name);

    private void UpdateColor(Character character) =>
        _view.UpdateColor(character.Team);

    private void UpdateCountDeath(Character character)
    {
        SetDeathStatus(character);
        _view.UpdateCountDeath(character.CountDeath);
    }

    private void SetDeathStatus(Character character) =>
        _view.SetDeathStatus(character.IsDeath);

    private void UpdateHealth(Character character) =>
        _view.UpdateHealth(character.Health);

    private void UpdateCountKill(Character character) =>
        _view.UpdateCountKill(character.CountKill);
}