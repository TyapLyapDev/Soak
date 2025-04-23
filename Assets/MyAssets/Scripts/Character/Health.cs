using System;

public class Health
{
    private const float MaximumValue = 100f;

    private float _currentValue;

    public event Action<float> ValueChanged;
    public event Action Died;

    public Health()
    {
        SetMaximumValue();
    }

    public float Value => _currentValue;

    public void SetMaximumValue()
    {
        _currentValue = MaximumValue;
        ValueChanged?.Invoke(_currentValue);
    }

    public void Kill()
    {
        _currentValue = 0;
        Died?.Invoke();
        ValueChanged?.Invoke(_currentValue);
    }

    public void TakeDamage(float damage)
    {
        if (damage < 0)
            throw new Exception("Значение урона должно быть положительным");

        if (_currentValue == 0)
            return;

        _currentValue -= damage;

        if (_currentValue < 0)
            _currentValue = 0;

        if (_currentValue == 0)
            Died?.Invoke();

        ValueChanged?.Invoke(_currentValue);
    }
}