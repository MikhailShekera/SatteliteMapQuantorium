using UnityEngine;

namespace UnityDevKit.UI.TextField
{
    public class DisplayFloatValueWrapper : MonoBehaviour
    {
        [SerializeField] private DisplayFloatValue displayFloatValue;

        public void SendToTextField(float value)
        {
            displayFloatValue.SendToTextField(value);
        }
    }
}