using MyBox;
using UnityDevKit.InteractionSystem.Core;
using UnityEngine;

namespace UnityDevKit.InteractionSystem.Source
{
    public class BaseInteractionSource : InteractionEntity
    {
        [Separator("Interaction source settings")]
        [SerializeField] protected GameObject source;
    }
}