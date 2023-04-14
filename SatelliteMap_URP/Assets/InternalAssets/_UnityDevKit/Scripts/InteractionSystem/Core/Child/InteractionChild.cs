using UnityEngine;

namespace UnityDevKit.InteractionSystem.Core.Child
{
    [DisallowMultipleComponent]
    public abstract class InteractionChild<T> : MonoBehaviour
    where T : InteractionEntity
    {
        [SerializeField] private T interactionParent;

        protected virtual void Awake()
        {
            if (!interactionParent)
            {
                if (!transform.parent.TryGetComponent(out interactionParent))
                {
                    Debug.LogError("InteractableChild has no reference to InteractableBase");
                }
            }
        }

#if UNITY_EDITOR
        public void SetInteractionParent(T newInteractionParent)
        {
            interactionParent = newInteractionParent;
        }
#endif
        public T InteractionParent => interactionParent;
    }
}