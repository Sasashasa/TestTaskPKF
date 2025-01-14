public abstract class Ability
{
    public int CurrentCooldown { get; protected set; }
    public bool IsReady => CurrentCooldown <= 0;
    
    protected readonly int Cooldown;
    protected readonly int Duration;

    protected Ability(int cooldown, int duration = 0)
    {
        Cooldown = cooldown;
        Duration = duration;
        CurrentCooldown = 0;
    }

    public void ResetCooldown()
    {
        CurrentCooldown = 0;
    }
    
    public void UpdateCooldown()
    {
        if (CurrentCooldown > 0)
            CurrentCooldown--;
    }
    
    public abstract void Use(Unit caster, Unit target);
}