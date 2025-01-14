using UnityEngine;

public class RegenEffect : Effect
{
    private readonly int _healPerTurn;

    public RegenEffect(int duration, int healAmount) : base(duration)
    {
        _healPerTurn = healAmount;
    }

    public override void ApplyEffect(Unit unit)
    {
        Debug.Log($"{unit.UnitName} восстановил {_healPerTurn} здоровья благодаря регенерации. Осталось ходов: {Duration}");
        
        unit.Heal(_healPerTurn);
        Duration--;
    }
}