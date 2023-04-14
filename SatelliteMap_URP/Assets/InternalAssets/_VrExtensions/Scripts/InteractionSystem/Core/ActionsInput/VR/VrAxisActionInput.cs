using HurricaneVR.Framework.Core.Grabbers;
using UnityDevKit.InteractionSystem.Core.ActionsInput;
using UnityEngine;

namespace VrExtensions.InteractionSystem.Core.ActionsInput.VR
{
    public abstract class VrAxisActionInput : InteractionAxisActionInput
    {
        [SerializeField] protected HVRHandGrabber handGrabber;
    }
}