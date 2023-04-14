using MyBox;
using UnityDevKit.Optimization;
using UnityEngine;
using UnityEngine.Events;

namespace UnityDevKit.Zones.Zones
{
    [RequireComponent(typeof(Collider))]
    public class Zone : CachedMonoBehaviour
    {
        [Separator("Main settings")]
        [SerializeField] private GameObject rootObject;
        [SerializeField] [PositiveValueOnly] private int index;
        
        [Separator("Events")]
        [SerializeField] private UnityEvent onZoneEnter;
        [SerializeField] private UnityEvent onZoneExit;
        
        public GameObject RootObject => rootObject;
        public bool IsActiveRoot => RootObject.activeSelf;
        public int Index => index;
        public UnityEvent OnZoneEnter => onZoneEnter;
        public UnityEvent OnZoneExit => onZoneExit;

        protected override void Awake()
        {
            base.Awake();
            if (rootObject == null)
            {
                rootObject = TransformData.parent.gameObject;
            }

            if (rootObject == null)
            {
                throw new ZoneException();
            }
        }

        public void EnterZone()
        {
            HandleOnZoneEnter();
            onZoneEnter.Invoke();
        }
        
        public void ExitZone()
        {
            HandleOnZoneExit();
            onZoneExit.Invoke();
        }
        
        protected virtual void HandleOnZoneEnter()
        {
        }

        protected virtual void HandleOnZoneExit()
        {
        }
    }
}