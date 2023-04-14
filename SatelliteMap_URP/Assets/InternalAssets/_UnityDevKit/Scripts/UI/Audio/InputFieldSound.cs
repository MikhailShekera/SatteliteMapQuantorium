using TMPro;
using UnityEngine;

namespace UnityDevKit.UI.Audio
{
    [RequireComponent(typeof(TMP_InputField))]
    public class InputFieldSound : MonoBehaviour
    {
        [SerializeField] private AudioSource onValueChangeSound;
        
        private void Start()
        {
            var inputField = GetComponent<TMP_InputField>();
            inputField.onValueChanged.AddListener(_ => onValueChangeSound.PlayOneShot(onValueChangeSound.clip));
        }
    }
}