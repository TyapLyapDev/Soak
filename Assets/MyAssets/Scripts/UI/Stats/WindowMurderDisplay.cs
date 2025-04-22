using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WindowMurderDisplay : MonoBehaviour
{
    private const float DurationInSeconds = 5f;

    [SerializeField] private CharacterManager _manager;
    [SerializeField] private MurderLine _prefab;
    [SerializeField] private Sprite _suicideSprite;
    [SerializeField] private Sprite _weaponSprite;

    private Pool<MurderLine> _pool;
    private RectTransform _rectContent;
    private WaitForSeconds _wait;

    private void Awake()
    {
        _rectContent = transform.GetComponent<RectTransform>();
        _pool = new(_prefab, transform);
        _wait = new(DurationInSeconds);        
    }

    private void Start() =>
        ClearContent();

    private void OnEnable() =>
        _manager.Murdered += OnMurdered;

    private void OnDisable() =>
        _manager.Murdered -= OnMurdered;

    private void ClearContent()
    {
        foreach (Transform child in transform)
            Destroy(child.gameObject);
    }

    private void OnMurdered(Character killer, Character sacrifice)
    {
        if (_pool.TryGet(out MurderLine line) == false)
            return;

        Color colorKiller;
        string nameKiller;
        Sprite sprite;

        if (killer == null)
        {
            sprite = _suicideSprite;            
            colorKiller = Color.white;
            nameKiller = string.Empty;
        }
        else
        {
            sprite = _weaponSprite;
            colorKiller = TeamColors.Instance.Get(killer.Team);
            nameKiller = killer.Name;
        }

        Color colorSacrifice = TeamColors.Instance.Get(sacrifice.Team);
        string nameSacrifice = sacrifice.Name;

        line.SetSprite(sprite);
        line.SetColor(colorKiller, colorSacrifice);
        line.SetNames(nameKiller, nameSacrifice);

        RectTransform rectLine = line.GetComponent<RectTransform>();
        LayoutRebuilder.ForceRebuildLayoutImmediate(rectLine);
        LayoutRebuilder.ForceRebuildLayoutImmediate(_rectContent);

        StartCoroutine(RemoveLineAfterDelay(line));
    }

    private IEnumerator RemoveLineAfterDelay(MurderLine line)
    {
        yield return _wait;

        line.ReturnInPool();
        LayoutRebuilder.ForceRebuildLayoutImmediate(_rectContent);
    }
}