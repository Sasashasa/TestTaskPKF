using UnityEngine;

public class FireballAbility : Ability
{
    private readonly int _directDamage;
    private readonly int _burnDamage;

    public FireballAbility() : base(6, 5)
    {
        _directDamage = 5;
        _burnDamage = 1;
    }

    public override void Use(Unit caster, Unit target)
    {
        Debug.Log($"{caster.UnitName} применил Огненный Шар, нанеся {_directDamage} урона и наложив горение на {Duration} ходов.");
        
        target.TakeDamage(_directDamage);
        
        target.AddEffect(new BurnEffect(Duration, _burnDamage));
        
        CurrentCooldown = Cooldown;
    }
}