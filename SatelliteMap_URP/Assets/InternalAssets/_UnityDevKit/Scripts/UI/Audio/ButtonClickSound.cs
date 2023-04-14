using UnityEngine;
using UnityEngine.UI;

namespace UnityDevKit.UI.Audio
{
    [RequireComponent(typeof(Button))]
    public class ButtonClickSound : MonoBehaviour
    {
        [SerializeField] private AudioSource onClickSound;
        
        private void Start()
        {
            var btn = GetComponent<Button>();
            btn.onClick.AddListener(() => onClickSound.PlayOneShot(onClickSound.clip));
        }
    }
}