using System;

public class Health
{
    private readonly float _baseHealth;
    private float _maxHealth => _baseHealth * Coef;
    private float _takenDamage;

    public float Current => _maxHealth - _takenDamage;
    public float Coef = 1f;
    public float LerpCurrent => Current / _maxHealth;
    public event Action OnHealthChanged;
    
    public Health(float baseHealth)
    {
        _baseHealth = baseHealth;
    }

    public void Damage(float damage)
    {
        _takenDamage += damage;
        if (_takenDamage > _maxHealth) { 
            _takenDamage = _maxHealth;
        }
        OnHealthChanged?.Invoke();
    }

    public void Restore(float value)
    {
        _takenDamage -= value;
        if (_takenDamage < 0) {
            _takenDamage = 0;
        }
        OnHealthChanged?.Invoke();
    }
    
}
