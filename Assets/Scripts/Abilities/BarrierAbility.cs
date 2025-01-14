using UnityEngine;

public class BarrierAbility : Ability
{
    private readonly int _barrierPoints;

    public BarrierAbility() : base(4, 2)
    {
        _barrierPoints = 5;
    }

    public override void Use(Unit caster, Unit target)
    {
        caster.AddBarrier(_barrierPoints);
        
        caster.AddEffect(new BarrierEffect(Duration));
        
        CurrentCooldown = Cooldown;
        
        Debug.Log($"{caster.UnitName} создал барьер на {Duration} ходов, поглощающий суммарно {_barrierPoints} урона.");
    }
}