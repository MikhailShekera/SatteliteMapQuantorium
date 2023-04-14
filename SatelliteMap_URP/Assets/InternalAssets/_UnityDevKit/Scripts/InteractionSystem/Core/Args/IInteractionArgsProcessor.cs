namespace UnityDevKit.InteractionSystem.Core.Args
{
    public interface IInteractionArgsProcessor<out TArgs>
    {
        public TArgs Process(InteractionArgs args);
    }
}