using UnityEngine;

namespace SatelliteMap.VR.Interactable.Grab
{
    public class VrGrabbable : MonoBehaviour
    {
        [SerializeField] private Transform root;

        private Transform _rootParent;
        private Transform _currentGrabber;
        
        private void Awake()
        {
            _rootParent = root.parent;
        }

        public void Grab(Transform grabber)
        {
            root.SetParent(grabber);
            _currentGrabber = grabber;
        }

        public void Throw(Transform grabber)
        {
            if (_currentGrabber == grabber)
            {
                root.SetParent(_rootParent);
            }
        }
    }
}