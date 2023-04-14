using UnityDevKit.Events;
using UnityEngine;

namespace UnityDevKit.Variants
{
    public class Variant : MonoBehaviour
    {
        public EventHolderBase OnTurnOn { get; } = new EventHolderBase();
        public EventHolderBase OnTurnOff { get; } = new EventHolderBase();
        
        public virtual void TurnOn()
        {
            OnTurnOn.Invoke();
        }

        public virtual void TurnOff()
        {
            OnTurnOff.Invoke();
        }
    }
}