using System;
using UnityEngine;

namespace UnityDevKit.Interactables.Children
{
    [DisallowMultipleComponent] [Obsolete("Class is obsolete", false)]
    public class InteractableChild : MonoBehaviour
    {
        [SerializeField] private InteractableBase interactableBase;

        protected virtual void Awake()
        {
            if (!interactableBase)
            {
                if (!transform.parent.TryGetComponent(out interactableBase))
                {
                    Debug.LogError("InteractableChild has no reference to InteractableBase");
                }
            }
        }

        public InteractableBase Interactable => interactableBase;
    }
}