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
    private readonly List<Character> _characters;
    private Coroutine _coroutine;

    public event Action Restarted;

    public RoundRestarter(MonoBehaviour mono, CharacterPositionAssigner positionAssigner, List<Character> characters)
    {
        _mono = mono;
        _positionAssigner = positionAssigner;
        _characters = characters;
        _waitInSeconds = new WaitForSeconds(DelayBeforeRestartAfterRoundEnds);
    }

    public void Restart()
    {
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
            character.Resurrect();
            _positionAssigner.SetPosition(character);
        }

        Restarted?.Invoke();
    }
}