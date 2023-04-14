using MyBox;
using UnityDevKit.Events;
using UnityEngine;
using UnityEngine.Events;

namespace UnityDevKit.Interactables.Buttons
{
    public class ButtonEventsFlowConnector : ButtonConnector
    {
        [Foldout("Events from platform logic", true)]
        [SerializeField] private EventFlow onButtonDownFromEvent;
        [SerializeField] private EventFlow onButtonUpFromEvent;
        
        [Foldout("Events to platform logic", true)]
        [SerializeField] private EventFlow onButtonDownToEvent;
        [SerializeField] private EventFlow onButtonUpToEvent;
        
        #region From platform logic
        public override void OnButtonDownFrom()
        {
            onButtonDownFromEvent.Invoke();
        }
        
        public override void OnButtonUpFrom()
        {
            onButtonUpFromEvent.Invoke();
        }
        #endregion
        
        #region To platform logic
        public override void OnButtonDownTo()
        {
            onButtonDownToEvent.Invoke();
        }
        
        public override void OnButtonUpTo()
        {
            onButtonUpToEvent.Invoke();
        }

        public override void AddListenerOnButtonDown(UnityAction listener)
        {
            onButtonDownFromEvent.AddListener(listener);
        }

        public override void AddListenerOnButtonUp(UnityAction listener)
        {
            onButtonUpFromEvent.AddListener(listener);
        }

        #endregion
    }
}