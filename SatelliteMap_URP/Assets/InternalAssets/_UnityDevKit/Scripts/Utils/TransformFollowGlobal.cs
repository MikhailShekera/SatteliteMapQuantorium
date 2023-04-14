using UnityEngine;

namespace UnityDevKit.Utils
{
    public class TransformFollowGlobal : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private Transform follow;

        [SerializeField] private bool controlPosition;
        [SerializeField] private bool controlRotation;
        [SerializeField] private bool controlScale;

        private Vector3 previousPosition;
        private Vector3 previousRotation;
        private Vector3 previousScale;

        private void Start()
        {
            previousPosition = target.position;
            previousRotation = target.eulerAngles;
            previousScale = target.localScale;
        }

        private void Update()
        {
            if (controlPosition)
            {
                var position = target.position;
                follow.position += position - previousPosition;
                previousPosition = position;
            }

            if (controlRotation)
            {
                var eulerAngles = target.eulerAngles;
                follow.eulerAngles += eulerAngles - previousRotation;
                previousRotation = eulerAngles;
            }

            if (controlScale)
            {
                var localScale = target.localScale;
                follow.localScale += localScale - previousScale;
                previousScale = localScale;
            }
        }
    }
}