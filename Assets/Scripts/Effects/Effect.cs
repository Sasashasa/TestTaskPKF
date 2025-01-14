public abstract class Effect
{
    public int Duration { get; protected set; }

    protected Effect(int duration)
    {
        Duration = duration;
    }

    public abstract void ApplyEffect(Unit unit);
}