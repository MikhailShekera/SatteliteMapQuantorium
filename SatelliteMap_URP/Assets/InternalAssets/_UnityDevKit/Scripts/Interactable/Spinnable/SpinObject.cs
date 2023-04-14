using UnityEngine;

namespace UnityDevKit.Interactables.Spinnable
{
    public class SpinObject : MonoBehaviour
    {
        [SerializeField] private GameObject objectToSpin;
        [SerializeField] private float rotationSpeed = 2f;

        private Vector3 _rotationPivotPoint;

        private void Awake()
        {
            _rotationPivotPoint = objectToSpin.transform.position;
        }

        private void FixedUpdate()
        {
            objectToSpin.transform.RotateAround(_rotationPivotPoint, Vector3.up, rotationSpeed);
        }
    }
}