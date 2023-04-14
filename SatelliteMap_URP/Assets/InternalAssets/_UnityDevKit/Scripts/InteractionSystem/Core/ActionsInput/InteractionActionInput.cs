using UnityEngine;

namespace UnityDevKit.InteractionSystem.Core.ActionsInput
{
    public abstract class InteractionActionInput : MonoBehaviour
    {
        public abstract bool Handle();
    }
}