using UnityEngine;

namespace SatelliteMap.WorldUI
{
    public class WorkModeController : MonoBehaviour
    {
        [SerializeField] private WindowControllerBase workMode;

        public WindowControllerBase WorkMode => workMode;

        private void Awake()
        {
            ToggleWorkMode();
        }

        private void ToggleWorkMode()
        {
            foreach (Transform child in transform)
            {
                if (child.GetComponent<WindowControllerBase>() == workMode)
                {
                    workMode.gameObject.SetActive(true);
                }
                else
                {
                    child.gameObject.SetActive(false);
                }
            }
        }
    }
}