using UnityDevKit.InteractionSystem.Core;
using UnityEngine;

namespace UnityDevKit.InteractionSystem.Interactable
{
    [DisallowMultipleComponent] [RequireComponent(typeof(Collider))]
    public class BaseInteractable : InteractionEntity
    {
    }
}