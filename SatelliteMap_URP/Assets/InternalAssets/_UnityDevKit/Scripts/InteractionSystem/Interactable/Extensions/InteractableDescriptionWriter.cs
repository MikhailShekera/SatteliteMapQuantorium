using UnityDevKit.InteractionSystem.Core.Args;
using UnityDevKit.UI.Descriptions;
using UnityEngine;

namespace UnityDevKit.InteractionSystem.Interactable.Extensions
{
    public class InteractableDescriptionWriter : InteractableExtension
    {
        [SerializeField] protected Description description;
        [SerializeField] private string text;
        
        protected override void OnFocusAction(InteractionArgs args)
        {
            base.OnFocusAction(args);
            description.Show(text);
        }

        protected override void OnDeFocusAction(InteractionArgs args)
        {
            base.OnDeFocusAction(args);
            description.Reset();
        }

        protected override void OnInteractAction(InteractionArgs args)
        {
            base.OnInteractAction(args);
            description.SetupNew(text);
        }
    }
}