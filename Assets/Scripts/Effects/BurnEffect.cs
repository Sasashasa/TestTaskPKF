using UnityEngine;

public class BurnEffect : Effect
{
    private readonly int _damagePerTurn;

    public BurnEffect(int duration, int damage) : base(duration)
    {
        _damagePerTurn = damage;
    }

    public override void ApplyEffect(Unit unit)
    {
        Debug.Log($"{unit.UnitName} получил {_damagePerTurn} урона от горения. Осталось ходов: {Duration}");
        
        unit.TakeDamage(_damagePerTurn);
        Duration--;
    }
}