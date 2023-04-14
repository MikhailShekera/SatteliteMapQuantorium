using UnityDevKit.Events;
using UnityEngine;

namespace UnityDevKit.Interactable.PlayerView
{
    public class ViewObject : MonoBehaviour
    {
        [SerializeField] private EventFlow onFocusEvent;
        [SerializeField] private EventFlow onDeFocusEvent;

        public void Focus()
        {
            onFocusEvent.Invoke();
        }

        public void DeFocus()
        {
            onDeFocusEvent.Invoke();
        }
    }
}