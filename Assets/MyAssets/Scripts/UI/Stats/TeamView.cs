using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TeamView : MonoBehaviour
{
    private const string OnePlayer = "игрок";
    private const string SomePlayers = "игрока";
    private const string MorePlayers = "игроков";

    [SerializeField] private TextMeshProUGUI _team;
    [SerializeField] private TextMeshProUGUI _countWin;
    [SerializeField] private Image _border;

    public void SetColor(Color color)
    {
        _team.color = color;
        _countWin.color = color;
        _border.color = color;
    }

    public void SetTeamName(string name, int countPlayers)
    {
        string playerForm = GetPlayerForm(countPlayers);
        _team.text = $"{name}   -   {countPlayers} {playerForm}";
    }

    public void SetCountWin(int countWin) =>
        _countWin.text = countWin >= 0 ? countWin.ToString() : string.Empty;

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