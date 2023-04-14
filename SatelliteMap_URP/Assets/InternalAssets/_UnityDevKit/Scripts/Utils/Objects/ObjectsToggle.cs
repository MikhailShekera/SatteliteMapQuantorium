using System.Linq;
using UnityEngine;

namespace UnityDevKit.Utils.Objects
{
    public class ObjectsToggle : MonoBehaviour
    {
        [SerializeField] private GameObject[] objects;

        private bool _currentIsActive;

        private void Start()
        {
            _currentIsActive = objects.All(o => o.activeSelf);
        }

        public void TurnOn()
        {
            ToggleToValue(true);
        }

        public void TurnOff()
        {
            ToggleToValue(false);
        }

        public void Toggle()
        {
            ToggleToValue(!_currentIsActive);
        }

        private void ToggleToValue(bool isActive)
        {
            foreach (var obj in objects)
            {
                obj.SetActive(isActive);
            }

            _currentIsActive = isActive;
        }
    }
}