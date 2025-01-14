using UnityEngine;

public class RegenAbility : Ability
{
    private readonly int _healPerTurn;

    public RegenAbility() : base(5, 3)
    {
        _healPerTurn = 2;
    }

    public override void Use(Unit caster, Unit target)
    {
        caster.AddEffect(new RegenEffect(Duration, _healPerTurn));
        
        CurrentCooldown = Cooldown;
        
        Debug.Log($"{caster.UnitName} активировал регенерацию, восстанавливая {_healPerTurn} здоровья за ход на {Duration} ходов.");
    }
}