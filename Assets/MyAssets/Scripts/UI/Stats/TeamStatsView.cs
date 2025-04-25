using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TeamStatsView : MonoBehaviour
{
    private const string OnePlayer = "игрок";
    private const string SomePlayers = "игрока";
    private const string MorePlayers = "игроков";

    [SerializeField] private TextMeshProUGUI _teamNameText;
    [SerializeField] private TextMeshProUGUI _winCountText;
    [SerializeField] private Image _border;

    public void UpdateTeamHeader(TeamType teamType, int countCharacters)
    {
        string team = Utils.GetTeamName(teamType);
        string count = countCharacters.ToString();
        string form = GetPlayerForm(countCharacters);

        _teamNameText.text = $"{team} - {count} {form}";
    }

    public void UpdateWinCount(int countWins) =>
        _winCountText.text = countWins < 0 ? string.Empty : countWins.ToString();

    public void UpdateColor(TeamType teamType)
    {
        Color color = TeamColors.Instance.Get(teamType);

        _teamNameText.color = color;
        _winCountText.color = color;
        _border.color = color;
    }

    private string GetPlayerForm(int count)
    {
        if (count % 10 == 1 && count % 100 != 11)
            return OnePlayer;

        else if (count % 10 >= 2 && count % 10 <= 4 && (count % 100 < 10 || count % 100 >= 20))
            return SomePlayers;

        else
            return MorePlayers;
    }
}