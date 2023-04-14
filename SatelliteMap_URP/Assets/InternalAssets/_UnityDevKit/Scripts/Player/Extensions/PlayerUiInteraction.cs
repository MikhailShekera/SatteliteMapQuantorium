using MyBox;
using UnityDevKit.UI;
using UnityDevKit.Triggers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace UnityDevKit.Player.Extensions
{
    public class PlayerUiInteraction : BindedPlayerExtension
    {
        [Separator("Main settings")]
        [SerializeField] private Image cursor;
        [SerializeField] private LayerMask raycastLayers;
        [SerializeField] private float checkDistance = 1.25f;
        [SerializeField] private BoolTriggerEvent isFreeLookTrigger;
        [SerializeField, MustBeAssigned] private InteractiveUiHandler interactiveUiHandler;

        private InputAction _clickInteraction;
        private InputAction _scrollInteraction;
        
        protected override void Start()
        {
            base.Start();
        }

        public override void AddBindings()
        {
            _clickInteraction = PlayerController.InputManager.MovementControls.Movement.Click;
            _scrollInteraction = PlayerController.InputManager.MovementControls.Movement.ScrollUI;

            _clickInteraction.canceled += GetUIInteraction;
            _scrollInteraction.performed += ScrollUI;
        }

        protected override void PlayerLoop()
        {
            base.PlayerLoop();

            const int focusFrameDelay = 5;
            
            if (!isFreeLookTrigger.GetValue()) return;
            if (Time.frameCount % focusFrameDelay == 0)
            {
                FocusInteractiveUi();
            }
        }

        public void GetUIInteraction(InputAction.CallbackContext context)
        {
            if (!isFreeLookTrigger.GetValue()) return;
                
            ClickInteractiveUi();
        }

        public void ScrollUI(InputAction.CallbackContext context)
        {
            if (!isFreeLookTrigger.GetValue()) return;
                
            ScrollInteractiveUi();
        }
        
        private void FocusInteractiveUi()
        {
            var pointerEventData = new PointerEventData(EventSystem.current)
            {
                position = Input.mousePosition
            };
            
            interactiveUiHandler.RaycastAllUi<Selectable>(
                pointerEventData, 
                clickHandler => clickHandler.Select(), 
                transform, 
                checkDistance);
        }
        
        private void ClickInteractiveUi()
        {
            var pointerEventData = new PointerEventData(EventSystem.current)
            {
                position = Input.mousePosition
            };
            interactiveUiHandler.RaycastAllUi<IPointerClickHandler>(
                pointerEventData, 
                clickHandler => clickHandler?.OnPointerClick(pointerEventData), 
                transform, 
                checkDistance);
        }
        
        private void ScrollInteractiveUi()
        {
            var pointerEventData = new PointerEventData(EventSystem.current)
            {
                position = Input.mousePosition,
                scrollDelta = GetMouseScrollNormalizedValue()
            };

            interactiveUiHandler.RaycastAllUi<ScrollRect>(
                pointerEventData, 
                scrollRect => scrollRect.OnScroll(pointerEventData), 
                transform, 
                checkDistance);
        }

        private Vector2 GetMouseScrollNormalizedValue()
        {
            var delta = _scrollInteraction.ReadValue<float>();

            return delta switch
            {
                > 0 => Vector2.up,
                < 0 => Vector2.down,
                _ => Vector2.zero
            };
        }
    }
}