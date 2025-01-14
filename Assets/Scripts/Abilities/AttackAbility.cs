using UnityEngine;

public class AttackAbility : Ability
{
    private readonly int _damage;

    public AttackAbility() : base(0)
    {
        _damage = 8;
    }

    public override void Use(Unit caster, Unit target)
    {
        Debug.Log($"{caster.UnitName} атаковал {target.UnitName}, нанеся {_damage} урона.");
        
        target.TakeDamage(_damage);
    }
}