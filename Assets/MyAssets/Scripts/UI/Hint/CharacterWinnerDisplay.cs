using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class CharacterWinnerDisplay : MonoBehaviour
{
    [SerializeField] private CharacterManager _manager;

    private TextMeshProUGUI _text;

    private void Awake() =>
        _text = GetComponent<TextMeshProUGUI>();

    private void Start() =>
        OnRoundRestarted();

    private void OnEnable()
    {
        OnRoundRestarted();
        _manager.TeamWinChanged += OnTeamWinChanged;
        _manager.RoundRestarted += OnRoundRestarted;
    }

    private void OnDisable()
    {
        _manager.TeamWinChanged -= OnTeamWinChanged;
        _manager.RoundRestarted -= OnRoundRestarted;
    }

    private void OnTeamWinChanged(TeamType team, int _) =>
        _text.text = team == TeamType.CounterTerrorist ? DataParams.Texts.TextCounterTerroristsWin : DataParams.Texts.TextTerroristsWin;

    private void OnRoundRestarted() =>
        _text.text = string.Empty;
}