using UnityEngine;

namespace VrExtensions.InteractionSystem.Core.ActionsInput.VR
{
    public class VrJoystickActionInput : VrAxisActionInput
    {
        public override bool Handle() => GetAxis() != Vector2.zero;
        
        public override Vector2 GetAxis() => handGrabber.Controller.JoystickAxis;
    }
}