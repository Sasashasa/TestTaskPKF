public class BarrierEffect : Effect
{
    public BarrierEffect(int duration) : base(duration)
    {
    }

    public override void ApplyEffect(Unit unit)
    {
        Duration--;
    }
}