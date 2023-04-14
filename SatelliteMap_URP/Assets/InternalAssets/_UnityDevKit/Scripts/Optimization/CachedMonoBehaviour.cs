using UnityEngine;

namespace UnityDevKit.Optimization
{
    public class CachedMonoBehaviour : MonoBehaviour
    {
        public Transform TransformData { get; private set; }

        public Vector3 SelfPosition() => TransformData.position;

        public Quaternion SelfRotation() => TransformData.rotation;

        protected virtual void Awake()
        {
            TransformData = transform;
        }
    }
}