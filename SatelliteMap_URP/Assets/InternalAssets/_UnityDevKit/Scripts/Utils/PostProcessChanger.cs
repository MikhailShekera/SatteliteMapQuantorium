using UnityEngine;
using UnityEngine.Rendering;

namespace UnityDevKit.Utils
{
    public class PostProcessChanger : MonoBehaviour
    {
        [SerializeField] private Volume volume;

        public void SetProfile(VolumeProfile profile)
        {
            volume.profile = profile;
        }
    }
}