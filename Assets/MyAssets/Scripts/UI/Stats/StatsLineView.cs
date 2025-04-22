using TMPro;
using UnityEngine;

public class StatsLineView : MonoBehaviour
{
    private const string Death = "Промок";
    private const string Alive = "";

    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI _life;
    [SerializeField] private TextMeshProUGUI _health;
    [SerializeField] private TextMeshProUGUI _countKill;
    [SerializeField] private TextMeshProUGUI _countDeath;

    public void SetColor(Color color)
    {
        _name.color = color;
        _life.color = color;
        _health.color = color;
        _countKill.color = color;
        _countDeath.color = color;
    }

    public void SetStats(string name, bool isDied, float health, int countKill, int countDeath)
    {
        _name.text = name;
        _life.text = isDied ? Death : Alive;
        _countKill.text = countKill.ToString();
        _countDeath.text = countDeath.ToString();
        SetHealth(health);
    }

    public void SetHealth(float value) =>
        _health.text = value > 0 ? value.ToString("F0") : string.Empty;
}