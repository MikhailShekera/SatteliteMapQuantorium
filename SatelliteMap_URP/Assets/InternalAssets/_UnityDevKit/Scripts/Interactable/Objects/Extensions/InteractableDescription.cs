using System;
using UnityDevKit.UI.Descriptions;
using UnityEngine;

namespace UnityDevKit.Interactables.Extensions
{
    [Obsolete("Class is obsolete", false)]
    public class InteractableDescription : InteractableExtension
    {
        [SerializeField] protected Description description;
        [SerializeField] private string text;
        
        protected override void OnFocusAction(GameObject source)
        {
            base.OnFocusAction(source);
            description.Show(text);
        }

        protected override void OnDeFocusAction(GameObject source)
        {
            base.OnDeFocusAction(source);
            description.Reset();
        }

        protected override void OnInteractAction(GameObject source)
        {
            base.OnInteractAction(source);
            description.SetupNew(text);
        }
    }
}