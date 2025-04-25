using System;
using System.Collections.Generic;
using UnityEngine;

public class BotEnemyAttacker
{
    private readonly BotTargetsFinder _botTargetsFinder;
    private readonly BotTargetSeer _botTargetSeer;
    private readonly BotVisibilityEnemyChecker _enemyChecker;
    private readonly CharacterDetector _detector;
    private readonly Rutine _rutine;
    private readonly Character _selfCharacter;
    private readonly Vector2 _timeShootingLimits = new(0.1f, 3f);
    private float _elapsedTime;
    private float _duration;

    private bool _isShooting;

    private Character _enemyTarget;

    public event Action<Character> EnemySerched;
    public event Action EnemyLost;
    public event Action ShootPressed;
    public event Action ShootUnpressed;

    public BotEnemyAttacker(Character selfCharacter, LayerMask layerMask, List<Character> characters)
    {
        _selfCharacter = selfCharacter;
        GazeDirection eyes = selfCharacter.GetComponentInChildren<GazeDirection>();

        _botTargetsFinder = new(eyes.transform, selfCharacter.Team, characters);
        _botTargetSeer = new(selfCharacter, eyes.transform, layerMask);
        _enemyChecker = new(selfCharacter, eyes.transform, layerMask);
        _detector = new(eyes.transform, selfCharacter.Colliders, layerMask);
        _rutine = new(selfCharacter.transform.GetComponent<MonoBehaviour>(), Update);
        _detector.Detected += OnDetected;
        _detector.Undetected += OnUndetected;
    }

    public void Start()
    {
        _rutine.Start();
        _detector.Start();
    }

    public void Stop()
    {
        _rutine.Stop();
        _detector.Stop();
    }

    private void Update()
    {
        if (_enemyTarget == null)
            SearchTarget();
        else
            CheckVisibilityCurrentEnemy();

        if (_enemyTarget != null && _isShooting == false)
        {
            _isShooting = true;
            _elapsedTime = 0;
            ShootPressed?.Invoke();
        }

        ChangeTime();
    }

    private void SearchTarget()
    {
        List<Character> targets = _botTargetsFinder.GetTargetsSight();

        if (_botTargetSeer.TrySeeEnemy(targets, out Character other))
            OnNewEnemyTarget(other);
    }

    private void CheckVisibilityCurrentEnemy()
    {
        if (_enemyChecker.TrySeeEnemy(_enemyTarget) || _enemyTarget.IsDead == false)
            return;

        _enemyTarget = null;
        EnemyLost?.Invoke();
    }

    private void ChangeTime()
    {
        _elapsedTime += Time.deltaTime;

        if (_elapsedTime > _duration)
        {
            _elapsedTime = 0;
            _duration = UnityEngine.Random.Range(_timeShootingLimits.x, _timeShootingLimits.y);

            if (_isShooting || _enemyTarget == null)
                StopAttack();
        }
    }

    private void OnDetected(Character other)
    {
        if (other.IsDead)
            return;

        bool isFriend = other.Team == _selfCharacter.Team && other.Team != TeamType.AgainstEveryone;

        if (isFriend == false && _enemyTarget != other)
            OnNewEnemyTarget(other);

        if (isFriend && _isShooting)
            StopAttack();

        if (isFriend == false && _isShooting == false)
            StartAttack();
    }

    private void OnNewEnemyTarget(Character other)
    {
        _enemyTarget = other;
        EnemySerched?.Invoke(_enemyTarget);
    }

    private void OnUndetected() =>
        _enemyTarget = null;

    private void StartAttack()
    {
        ShootPressed?.Invoke();
        _isShooting = true;
    }

    private void StopAttack()
    {
        _enemyTarget = null;
        _isShooting = false;
        ShootUnpressed?.Invoke();
    }
}