using UnityEngine;
using UnityEngine.UI;

public class PanelTeamSelectionPlayerView : MonoBehaviour
{
    [SerializeField] private Image _image;

    [SerializeField] private Sprite _terroristTeamSprite;
    [SerializeField] private Sprite _counterTerroristTeamSprite;
    [SerializeField] private Sprite _againstEveryoneSprite;
    [SerializeField] private Sprite _randomTeamSprite;
    [SerializeField] private Sprite _observerTeamSprite;
    [SerializeField] private Sprite _none;

    [SerializeField] private ButtonTeamSelection _buttonTerroristTeam;
    [SerializeField] private ButtonTeamSelection _buttonCounterTerroristTeam;
    [SerializeField] private ButtonTeamSelection _buttonAgainstEveryone;
    [SerializeField] private ButtonTeamSelection _buttonRandomTeam;
    [SerializeField] private ButtonTeamSelection _buttonObserver;

    private void Awake()
    {
        _buttonTerroristTeam.SetText($"1 {DataParams.Texts.TeamTerroristsName.ToUpperInvariant()}");
        _buttonCounterTerroristTeam.SetText($"2 {DataParams.Texts.TeamCounterTerroristsName.ToUpperInvariant()}");
        _buttonAgainstEveryone.SetText($"4 {DataParams.Texts.TeamAgainstEveryoneName.ToUpperInvariant()}");
        _image.sprite = _none;
    }

    private void OnEnable()
    {
        _buttonTerroristTeam.Entered += OnButtonTerroristTeamEnter;
        _buttonCounterTerroristTeam.Entered += OnButtonCounterTerroristTeamEnter;
        _buttonAgainstEveryone.Entered += OnButtonAgainstEveryoneEnter;
        _buttonRandomTeam.Entered += OnButtonRandomTeamEnter;
        _buttonObserver.Entered += OnButtonObserverEnter;

        _buttonTerroristTeam.Exited += OnExit;
        _buttonCounterTerroristTeam.Exited += OnExit;
        _buttonAgainstEveryone.Exited += OnExit;
        _buttonRandomTeam.Exited += OnExit;
        _buttonObserver.Exited += OnExit;
    }

    private void OnDisable()
    {
        _buttonTerroristTeam.Entered -= OnButtonTerroristTeamEnter;
        _buttonCounterTerroristTeam.Entered -= OnButtonCounterTerroristTeamEnter;
        _buttonAgainstEveryone.Entered -= OnButtonAgainstEveryoneEnter;
        _buttonRandomTeam.Entered -= OnButtonRandomTeamEnter;
        _buttonObserver.Entered -= OnButtonObserverEnter;

        _buttonTerroristTeam.Exited -= OnExit;
        _buttonCounterTerroristTeam.Exited -= OnExit;
        _buttonAgainstEveryone.Exited -= OnExit;
        _buttonRandomTeam.Exited -= OnExit;
        _buttonObserver.Exited -= OnExit;
    }

    private void OnButtonTerroristTeamEnter() =>
        _image.sprite = _terroristTeamSprite;

    private void OnButtonCounterTerroristTeamEnter() =>
        _image.sprite = _counterTerroristTeamSprite;
    
    private void OnButtonAgainstEveryoneEnter() =>
        _image.sprite = _againstEveryoneSprite;

    private void OnButtonRandomTeamEnter() =>
        _image.sprite = _randomTeamSprite;

    private void OnButtonObserverEnter() =>
        _image.sprite = _observerTeamSprite;

    private void OnExit() =>
        _image.sprite = _none;
}