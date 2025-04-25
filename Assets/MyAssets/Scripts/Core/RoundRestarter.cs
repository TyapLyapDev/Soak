using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundRestarter
{
    private const float DelayBeforeRestartAfterRoundEnds = 6f;

    private readonly MonoBehaviour _mono;
    private readonly CharacterPositionAssigner _positionAssigner;
    private readonly WaitForSeconds _waitInSeconds;
    private readonly IReadOnlyList<Character> _characters;
    private Coroutine _coroutine;

    private bool _isRoundFinished;

    public event Action Restarted;

    public RoundRestarter(MonoBehaviour mono, CharacterPositionAssigner positionAssigner, IReadOnlyList<Character> characters)
    {
        _mono = mono;
        _positionAssigner = positionAssigner;
        _characters = characters;
        _waitInSeconds = new WaitForSeconds(DelayBeforeRestartAfterRoundEnds);
    }

    public bool IsRoundFinished => _isRoundFinished;

    public void Restart()
    {
        _isRoundFinished = true;

        if (_coroutine != null)
            _mono.StopCoroutine(_coroutine);

        _coroutine = _mono.StartCoroutine(RestartOverTime());
    }

    public IEnumerator RestartOverTime()
    {
        yield return _waitInSeconds;

        StartNewRound();
    }

    private void StartNewRound()
    {
        _positionAssigner.ResetPoints();

        foreach (Character character in _characters)
        {
            if(character.Team == TeamType.Observer) 
                continue;

            character.Resurrect();
            _positionAssigner.SetPosition(character);
        }

        _isRoundFinished = false;

        Restarted?.Invoke();
    }
}