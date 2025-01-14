using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public string UnitName => _unitName;
    public int MaxHealth => _maxHealth;
    public int CurrentHealth { get; private set; }
    public List<Effect> ActiveEffects { get; } = new();

    [SerializeField] private string _unitName;
    [SerializeField] private int _maxHealth = 100;

    private int _barrierPoints;

    public void ResetCurrentHealth()
    {
        CurrentHealth = MaxHealth;
    }
    
    public void TakeDamage(int damage)
    {
        if (_barrierPoints > 0)
        {
            int absorbedDamage = Mathf.Min(damage, _barrierPoints);
            
            damage -= absorbedDamage;
            _barrierPoints -= absorbedDamage;
            
            Debug.Log($"{_unitName} блокировал {absorbedDamage} урона барьером. Осталось {_barrierPoints} защиты.");
        }

        if (damage > 0)
        {
            CurrentHealth = Mathf.Max(0, CurrentHealth - damage);
            
            Debug.Log($"{_unitName} получил {damage} урона. Текущее здоровье: {CurrentHealth}");
        }
        
        if (CurrentHealth == 0)
        {
            Die();
        }
    }
    
    public void Heal(int amount)
    {
        CurrentHealth = Mathf.Min(MaxHealth, CurrentHealth + amount);
    }
    
    public void AddBarrier(int amount)
    {
        _barrierPoints += amount;
    }

    public void RemoveBarrier()
    {
        _barrierPoints = 0;
    }
    
    public void UpdateEffects()
    {
        for (int i = ActiveEffects.Count - 1; i >= 0; i--)
        {
            ActiveEffects[i].ApplyEffect(this);

            if (ActiveEffects[i].Duration <= 0)
            {
                ActiveEffects.RemoveAt(i);
            }
        }
    }
    
    public void AddEffect(Effect effect)
    {
        ActiveEffects.Add(effect);
    }

    private void Die()
    {
        Debug.Log($"{_unitName} погиб!");
    }
}