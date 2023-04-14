using UnityEngine;

namespace UnityDevKit.InteractionSystem.Source
{
    public sealed class SourceHolder : MonoBehaviour
    {
        [SerializeField] private Transform holder;

        public Transform Holder => holder;
    }
}