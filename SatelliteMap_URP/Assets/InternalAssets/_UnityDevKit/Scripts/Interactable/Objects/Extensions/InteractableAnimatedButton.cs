using System;
using MyBox;
using UnityDevKit.Interactables.Buttons;
using UnityEditor;
using UnityEngine;

namespace UnityDevKit.Interactables.Extensions
{
    [Obsolete("Class is obsolete", false)]
    public class InteractableAnimatedButton : InteractableExtension
    {
        public AnimatedButton animatedButton;
        [SerializeField] private ButtonAction buttonAction = ButtonAction.ToggleWithReturn;
        
        [Serializable]
        public enum ButtonAction
        {
            SetDown,
            SetUp,
            Toggle,
            ToggleWithReturn
        }
        
        private void Awake()
        {
            if (animatedButton == null)
            {
                animatedButton = GetComponent<AnimatedButton>();
            }
        }

        protected override void OnInteractAction(GameObject source)
        {
            base.OnInteractAction(source);
            switch (buttonAction)
            {
                case ButtonAction.SetDown:
                    animatedButton.SetDown();
                    break;
                case ButtonAction.SetUp:
                    animatedButton.SetUp();
                    break;
                case ButtonAction.Toggle:
                    animatedButton.Toggle();
                    break;
                case ButtonAction.ToggleWithReturn:
                    animatedButton.ToggleWithReturn();
                    break;
            }
        }
        
#if UNITY_EDITOR
        [ButtonMethod]
        public void AddAnimatedButton()
        {
            if (animatedButton)
            {
                return;
            }
            
            var currentGameObject = gameObject;
            animatedButton = currentGameObject.GetComponent<AnimatedButton>();
            if (!animatedButton)
            {
                animatedButton = currentGameObject.AddComponent<AnimatedButton>();
            }
            
            EditorUtility.SetDirty(currentGameObject);
        }
#endif
    }
}