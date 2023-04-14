namespace UnityDevKit.Effects
{
    public interface IMutableEffect : IEffect
    {
        void IncreaseEffectPower();
        void DecreaseEffectPower();
    }
}