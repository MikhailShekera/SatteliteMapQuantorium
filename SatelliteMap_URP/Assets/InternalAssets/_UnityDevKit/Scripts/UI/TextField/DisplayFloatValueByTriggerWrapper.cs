using UnityEngine;

namespace UnityDevKit.UI.TextField
{
    public class DisplayFloatValueByTriggerWrapper : MonoBehaviour
    {
        [SerializeField] private DisplayFloatValueByTrigger displayFloatValueByTrigger;

        private void Start()
        {
            displayFloatValueByTrigger.Init();
        }
        
        
    }
}