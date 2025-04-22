using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(AudioSource))]
public abstract class Character : MonoBehaviour
{
    private CharacterComponentSearcher _initializer;
    private CharacterAudio _audio;
    private CharacterPhysics _physics;
    private CharacterView _view;
    private Health _health;
    private Transform _centerModel;
    private HashSet<Collider> _colliders;
    private WaterJet _jet;

    private TeamTypes _team;
    private string _name;
    private int _countKill;
    private int _countDeath;

    private bool _isDeath;

    public event Action<Character, float> HealthChanged;
    public event Action<Character> Died;

    public string Name => _name;

    public TeamTypes Team => _team;

    public int CountKill => _countKill;

    public int CountDeath => _countDeath;

    public float Health => _health.Value;

    public bool IsDeath => _isDeath;

    public Character Killer => _physics.Killer;

    public Transform Center => _centerModel.transform;

    public WaterJet Jet => _jet;

    public HashSet<Collider> Colliders => _colliders;

    protected virtual void Awake()
    {
        _initializer = new(this);
        _centerModel = _initializer.GetCenterModel();      
        _view = _initializer.GetView();
        _colliders = _initializer.GetColliders();
        _jet = _initializer.GetWaterJet();        

        _audio = new(transform);
        _health = new();
        _physics = new(transform);
        _physics.Init();
    }

    protected virtual void OnEnable()
    {
        _physics.Subscribe();
        _physics.DamageTaked += _health.TakeDamage;
        _view.Stepped += OnStepped;
        _health.ValueChanged += OnHealthChanged;
        _health.Died += OnDied;
    }

    protected virtual void OnDisable()
    {
        _physics.Unsubscribe();
        _physics.DamageTaked -= _health.TakeDamage;
        _view.Stepped -= OnStepped;
        _health.ValueChanged -= OnHealthChanged;
        _health.Died -= OnDied;
    }

    public virtual void Init(string name, TeamTypes team)
    {
        _name = name;
        _team = team;
    }

    public virtual void SetListCharacters(List<Character> characters)
    {
        if (_centerModel == false)
            _centerModel = GetComponentInChildren<CenterModel>(true).transform;
    }

    public void IncreaseCountKill() =>
        _countKill++;

    public void DecreaseCountKill() =>
        _countKill--;

    public void IncreaseCountDeath() =>
        _countDeath++;

    public void Kill() =>
        _health.Kill();

    public virtual void Resurrect()
    {
        _health.SetMaximumValue();
        _isDeath = false;
        _physics.Enable();
        _view.EnableAnimator();
        _physics.ResetKiller();
    }

    protected virtual void OnDied()
    {
        if (_isDeath)
            return;

        _isDeath = true;
        _view.DisableAnimator();
        _physics.Disable();
        _countDeath++;

        Died?.Invoke(this);
    }

    protected void Move(Vector2 direction)
    {
        _physics.Move(direction);
        _view.UpdateMovementAnimation(_physics.DeltaDistance);
    }

    protected void Jump() =>
        _physics.Jump();

    protected void Sneack()
    {
        _physics.Sneack();
        _view.PlaySneacking();
    }

    protected void Rise()
    {
        _physics.Rise();
        _view.PlayRising();
    }

    protected void SetSlowingStep() =>
        _physics.SetSlowingStep();

    protected void SetNormalStep() =>
        _physics.SetNormalStep();

    private void OnStepped()
    {
        if (_physics.CurrentSpeed >= 1f)
            _audio.PlayStep();
    }

    private void OnHealthChanged(float value) =>
        HealthChanged?.Invoke(this, value);
}