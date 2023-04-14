using UnityEngine;
using UnityEngine.Events;

namespace UnityDevKit.Events
{
    public class EventFlow : MonoBehaviour
    {
        [SerializeField] private UnityEvent onEvent;

        public void Invoke()
        {
            onEvent.Invoke();
        }

        public void AddListener(UnityAction listener)
        {
            onEvent.AddListener(listener);
        }
    }
}