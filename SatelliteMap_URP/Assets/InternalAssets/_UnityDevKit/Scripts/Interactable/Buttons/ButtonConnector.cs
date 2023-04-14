using UnityEngine;
using UnityEngine.Events;

namespace UnityDevKit.Interactables.Buttons
{
    public abstract class ButtonConnector : MonoBehaviour
    {
        public abstract void OnButtonDownFrom();

        public abstract void OnButtonUpFrom();

        public abstract void OnButtonDownTo();

        public abstract void OnButtonUpTo();

        public abstract void AddListenerOnButtonDown(UnityAction listener);
        
        public abstract void AddListenerOnButtonUp(UnityAction listener);
    }
}