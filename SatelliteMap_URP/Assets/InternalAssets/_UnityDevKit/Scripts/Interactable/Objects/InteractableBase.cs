using System;
using MyBox;
using UnityEngine;
using UnityDevKit.Events;
using UnityDevKit.Utils.TimeHandlers;

namespace UnityDevKit.Interactables
{
    [RequireComponent(typeof(Collider))] [Obsolete("Class is obsolete", false)]
    public class InteractableBase : MonoBehaviour
    {
        [Header("Main settings")] [SerializeField]
        private bool setStartActiveState = false;

        [SerializeField] [ConditionalField(nameof(setStartActiveState))]
        private bool activateOnStart = true;

        public readonly EventHolder<GameObject> OnFocus = new EventHolder<GameObject>();
        public readonly EventHolder<GameObject> OnDeFocus = new EventHolder<GameObject>();
        public readonly EventHolder<GameObject> OnInteract = new EventHolder<GameObject>();
        public readonly EventHolder<GameObject> OnAfterInteract = new EventHolder<GameObject>();

        public readonly EventHolder<bool> OnActiveStateChange = new EventHolder<bool>();
        public readonly EventHolder<bool> OnStopStateChange = new EventHolder<bool>();

        protected GameObject InteractionSource;

        private bool _isActivated = DEFAULT_ACTIVE_STATE;
        private bool _isStopped;

        private const bool DEFAULT_ACTIVE_STATE = true;

        private void Start()
        {
            Init();
        }

        protected virtual void Init()
        {
            if (setStartActiveState)
            {
                Activate(activateOnStart);
            }

            EventsSubscriptions();
        }

        private void EventsSubscriptions()
        {
            TimeManager.Instance.OnTimeModeChanged.AddListener(CheckTimeScale);
        }

        private void EventsDescriptions()
        {
            TimeManager.Instance.OnTimeModeChanged.RemoveListener(CheckTimeScale);
        }

        public virtual void Activate(bool value)
        {
            _isActivated = value;
            OnActiveStateChange.Invoke(_isActivated);
        }

        private void CheckTimeScale(TimeManager.TimeMode timeMode)
        {
            _isStopped = timeMode == TimeManager.TimeMode.Pause;
            OnStopStateChange.Invoke(_isStopped);
        }

        // ----- FOCUS -----
        public virtual void Focus(GameObject source)
        {
            if (!IsReady()) return;
            InteractionSource = source;
            OnFocus.Invoke(InteractionSource);
        }
        
        public virtual void Focus()
        {
            if (!IsReady()) return;
            OnFocus.Invoke(InteractionSource);
        }

        public virtual void DeFocus()
        {
            if (!IsReady()) return;
            OnDeFocus.Invoke(InteractionSource);
            InteractionSource = null;
        }

        // ----- INTERACTING -----
        public virtual void Interact()
        {
            if (!IsReady()) return;
            OnInteract.Invoke(InteractionSource);
        }

        public virtual void AfterInteract()
        {
            if (!IsReady()) return;
            OnAfterInteract.Invoke(InteractionSource);
        }

        protected bool IsReady()
        {
            return _isActivated && !_isStopped;
        }
    }
}