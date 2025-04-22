using UnityEngine;

public class TeamColors : MonoBehaviour
{
    public static TeamColors Instance { get; private set; }

    [SerializeField] private Color _none;
    [SerializeField] private Color _counterTerrorists;
    [SerializeField] private Color _terrorists;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public Color Get(TeamTypes team)
    {
        return team switch
        {
            TeamTypes.CounterTerrorist => _counterTerrorists,
            TeamTypes.Terrorist => _terrorists,
            _ => _none,
        };
    }
}