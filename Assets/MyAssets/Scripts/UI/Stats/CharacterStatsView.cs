using TMPro;
using UnityEngine;

public class CharacterStatsView : MonoBehaviour
{
    private const string Death = "Промок";
    private const string Alive = "";

    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI _life;
    [SerializeField] private TextMeshProUGUI _health;
    [SerializeField] private TextMeshProUGUI _countKill;
    [SerializeField] private TextMeshProUGUI _countDeath;
    [SerializeField] private GameObject _background;

    public void UpdateName(string name) =>
        _name.text = name;

    public void Select(bool isSelected) =>
        _background.SetActive(isSelected);

    public void UpdateColor(TeamType teamType)
    {
        Color color = TeamColors.Instance.Get(teamType);

        _name.color = color;
        _life.color = color;
        _health.color = color;
        _countKill.color = color;
        _countDeath.color = color;
    }

    public void UpdateCountDeath(int countDeath) =>
        _countDeath.text = countDeath.ToString();       

    public void SetDeathStatus(bool isDeath) =>
        _life.text = isDeath ? Death : Alive;

    public void UpdateHealth(float value) =>
        _health.text = value > 0 ? value.ToString("F0") : string.Empty;

    public void UpdateCountKill(int countKill) =>
        _countKill.text = countKill.ToString();
}