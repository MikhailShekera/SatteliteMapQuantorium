using System.Collections.Generic;
using System.Linq;
using MyBox;
using UnityDevKit.InputSystem;
using UnityDevKit.Player.Controllers;
using UnityDevKit.Player.Extensions;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace UnityDevKit.Player.UI
{
    public class PlayerUiPointerInputModule : PointerInputModule, IInputControlsBinder
    {
        [SerializeField, PositiveValueOnly] private float maxDistance = 10f;

        [Tooltip("Canvases for UI pointer interaction.")]
        public List<Canvas> uiCanvases = new List<Canvas>();

        [Tooltip("Angle the pointer has to move before a drag starts, " +
                 "too low and click events on a scroll rect will not execute")]
        public float angleDragThreshold = 1f;

        private List<PlayerUiPointer> _pointers;

        private InputAction _mouseClickAction;

        protected override void Awake()
        {
            _pointers = FindObjectsOfType<PlayerUiPointer>().ToList();
        }

        protected override void Start()
        {
            base.Start();

            _mouseClickAction = GetComponentInParent<PlayerController>().InputManager.MovementControls.Movement.Click;

            for (var j = 0; j < _pointers.Count; j++)
            {
                AddBindings(_pointers[j]);
            }
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            UpdatePointersData();
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            UpdatePointersData();
        }
        
        private void UpdatePointersData()
        {
            foreach (var pointer in _pointers)
            {
                pointer.PointerEventData = new PointerEventData(eventSystem);
            }
        }

        public void AddBindings(PlayerUiPointer pointer)
        {
            _mouseClickAction.started += (InputAction.CallbackContext context) => HandleClick(context ,pointer);
            _mouseClickAction.performed += (InputAction.CallbackContext context) => HandleClick(context, pointer);
            _mouseClickAction.canceled += (InputAction.CallbackContext context) => HandleClick(context, pointer);
        }

        public void AddCanvas(Canvas canvas)
        {
            if (uiCanvases.Contains(canvas))
                return;
            uiCanvases.Add(canvas);
        }

        public void RemoveCanvas(Canvas canvas)
        {
            if (!uiCanvases.Contains(canvas))
                return;
            uiCanvases.Remove(canvas);
        }

        private void HandleClick(InputAction.CallbackContext context, PlayerUiPointer pointer)
        {
            if (pointer.PointerEventData.pointerCurrentRaycast.gameObject == null)
                return;

            if(context.started)
            {
                ProcessPress(pointer);
            }
            else if(context.canceled)
            {
                ProcessRelease(pointer);
            }
        }

        public override void Process()
        {
            for (var j = 0; j < _pointers.Count; j++)
            {
                var pointer = _pointers[j];
                if (!pointer || !pointer.isActiveAndEnabled) continue;

                pointer.CurrentUIElement = null;

                for (var i = 0; i < uiCanvases.Count; i++)
                {
                    var canvas = uiCanvases[i];
                    if (!canvas) continue;
                    canvas.worldCamera = pointer.ViewCamera;
                }

                var isBlocked = pointer.Process();
                if (isBlocked)
                {
                    continue;
                }

                SendUpdateEventToSelectedObject();

                eventSystem.RaycastAll(pointer.PointerEventData, m_RaycastResultCache);
                pointer.PointerEventData.pointerCurrentRaycast = FindFirstRaycast(m_RaycastResultCache);

                if (pointer.PointerEventData.pointerCurrentRaycast.distance > maxDistance)
                {
                    continue;
                }

                m_RaycastResultCache.Clear();
                pointer.CurrentUIElement = pointer.PointerEventData.pointerCurrentRaycast.gameObject;

                var screenPosition =
                    (Vector2) pointer.ViewCamera.WorldToScreenPoint(
                        pointer.PointerEventData.pointerCurrentRaycast.worldPosition);
                var delta = screenPosition - pointer.PointerEventData.position;
                pointer.PointerEventData.position = screenPosition;
                pointer.PointerEventData.delta = delta;

                HandlePointerExitAndEnter(pointer.PointerEventData, pointer.CurrentUIElement);

                if (_mouseClickAction.phase == InputActionPhase.Performed)
                {
                    ProcessDrag(pointer.PointerEventData);
                }

                ProcessMove(pointer.PointerEventData);
                ProcessScroll(pointer.PointerEventData);
            }

            for (var i = 0; i < uiCanvases.Count; i++)
            {
                var canvas = uiCanvases[i];
                if (!canvas)
                    continue;
                canvas.worldCamera = null;
            }
        }

        private static void ProcessScroll(PointerEventData eventData)
        {
            if (!Mathf.Approximately(eventData.scrollDelta.sqrMagnitude, 0.0f))
            {
                var scrollHandler =
                    ExecuteEvents.GetEventHandler<IScrollHandler>(eventData.pointerCurrentRaycast.gameObject);
                ExecuteEvents.ExecuteHierarchy(scrollHandler, eventData, ExecuteEvents.scrollHandler);
            }
        }

        private bool SendUpdateEventToSelectedObject()
        {
            if (eventSystem.currentSelectedGameObject == null)
                return false;

            var data = GetBaseEventData();
            ExecuteEvents.Execute(eventSystem.currentSelectedGameObject, data, ExecuteEvents.updateSelectedHandler);
            return data.used;
        }

        protected override void ProcessDrag(PointerEventData eventData)
        {
            if (eventData.pointerDrag == null)
                return;

            if (!eventData.dragging)
            {
                var startDrag = ShouldStartDrag(eventData);

                if (startDrag)
                {
                    ExecuteEvents.Execute(eventData.pointerDrag, eventData, ExecuteEvents.beginDragHandler);
                    eventData.dragging = true;
                }
                else
                {
                    return;
                }
            }

            if (eventData.pointerPress != eventData.pointerDrag)
            {
                ExecuteEvents.Execute(eventData.pointerPress, eventData, ExecuteEvents.pointerUpHandler);
                eventData.eligibleForClick = false;
                eventData.pointerPress = null;
                eventData.rawPointerPress = null;
            }

            ExecuteEvents.Execute(eventData.pointerDrag, eventData, ExecuteEvents.dragHandler);
        }

        private bool ShouldStartDrag(PointerEventData eventData)
        {
            if (!eventData.useDragThreshold)
                return true;

            var cameraPos = eventData.pressEventCamera.transform.position;
            var pressDir = (eventData.pointerPressRaycast.worldPosition - cameraPos).normalized;
            var currentDir = (eventData.pointerCurrentRaycast.worldPosition - cameraPos).normalized;
            return Vector3.Dot(pressDir, currentDir) < Mathf.Cos(Mathf.Deg2Rad * angleDragThreshold);
        }

        private void ProcessPress(PlayerUiPointer pointer)
        {
            var eventData = pointer.PointerEventData;
            eventData.eligibleForClick = true;
            eventData.delta = Vector2.zero;
            eventData.dragging = false;
            eventData.useDragThreshold = true;
            eventData.pressPosition = eventData.position;
            eventData.pointerPressRaycast = eventData.pointerCurrentRaycast;

            DeselectIfSelectionChanged(pointer.CurrentUIElement, eventData);

            var pressed =
                ExecuteEvents.ExecuteHierarchy(pointer.CurrentUIElement, eventData, ExecuteEvents.pointerDownHandler);
            if (pressed == null)
            {
                pressed = ExecuteEvents.GetEventHandler<IPointerClickHandler>(pointer.CurrentUIElement);
            }

            var time = Time.unscaledTime;

            if (pressed == eventData.lastPress)
            {
                var diffTime = time - eventData.clickTime;
                if (diffTime < 0.3f)
                    ++eventData.clickCount;
                else
                    eventData.clickCount = 1;

                eventData.clickTime = time;
            }
            else
            {
                eventData.clickCount = 1;
            }

            eventData.pointerPress = pressed;
            eventData.rawPointerPress = pointer.CurrentUIElement;
            eventData.clickTime = Time.unscaledTime;
            eventData.pointerDrag = ExecuteEvents.GetEventHandler<IDragHandler>(pointer.CurrentUIElement);
            if (eventData.pointerDrag != null)
                ExecuteEvents.Execute(eventData.pointerDrag, eventData, ExecuteEvents.initializePotentialDrag);
        }

        private void ProcessRelease(PlayerUiPointer pointer)
        {
            var eventData = pointer.PointerEventData;

            ExecuteEvents.Execute(eventData.pointerPress, eventData, ExecuteEvents.pointerUpHandler);

            var handler = ExecuteEvents.GetEventHandler<IPointerClickHandler>(pointer.CurrentUIElement);
            if (eventData.pointerPress == handler && eventData.eligibleForClick)
            {
                ExecuteEvents.Execute(eventData.pointerPress, eventData, ExecuteEvents.pointerClickHandler);
            }
            else if (eventData.pointerDrag != null && eventData.dragging)
            {
                ExecuteEvents.ExecuteHierarchy(pointer.CurrentUIElement, eventData, ExecuteEvents.dropHandler);
            }

            if (eventData.pointerDrag != null && eventData.dragging)
            {
                ExecuteEvents.Execute(eventData.pointerDrag, eventData, ExecuteEvents.endDragHandler);
            }

            eventData.eligibleForClick = false;
            eventData.dragging = false;
            eventData.pointerDrag = null;
            eventData.pressPosition = Vector2.zero;
            eventData.pointerPress = null;
            eventData.rawPointerPress = null;

            if (pointer.CurrentUIElement != eventData.pointerEnter)
            {
                HandlePointerExitAndEnter(eventData, null);
                HandlePointerExitAndEnter(eventData, pointer.CurrentUIElement);
            }
        }

        public void AddBindings()
        {
            throw new System.NotImplementedException();
        }
    }
}