using UnityEngine;

public class CleanseAbility : Ability
{
    public CleanseAbility() : base(5)
    {
    }

    public override void Use(Unit caster, Unit target)
    {
        CurrentCooldown = Cooldown;
        
        for (int i = caster.ActiveEffects.Count - 1; i >= 0; i--)
        {
            if (caster.ActiveEffects[i] is BurnEffect)
            {
                caster.ActiveEffects.RemoveAt(i);
                
                Debug.Log($"{caster.UnitName} снял эффект горения.");
            }
        }
    }
}