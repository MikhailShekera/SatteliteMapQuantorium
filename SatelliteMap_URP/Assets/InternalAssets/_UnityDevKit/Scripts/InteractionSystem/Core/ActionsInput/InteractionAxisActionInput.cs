using UnityEngine;

namespace UnityDevKit.InteractionSystem.Core.ActionsInput
{
    public abstract class InteractionAxisActionInput : InteractionActionInput
    {
        public abstract Vector2 GetAxis();
    }
}