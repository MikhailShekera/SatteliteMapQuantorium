using UnityDevKit.Events;
using UnityEngine;
using UnityDevKit.InputSystem.Manager;

namespace UnityDevKit.Player.Controllers
{
    [DisallowMultipleComponent]
    public abstract class PlayerController: MonoBehaviour
    {
        public readonly EventHolderBase OnPlayerBlock = new EventHolderBase();
        public readonly EventHolderBase OnPlayerUnblock = new EventHolderBase();

        public InputManager InputManager { get; protected set; } = new InputManager();

        private void Awake()
        {
            InputManager.CreateControls();
        }

        public virtual void BlockPlayer()
        {
            OnPlayerBlock.Invoke();
        }
        
        public virtual void UnblockPlayer()
        {
            OnPlayerUnblock.Invoke();
        }
    }
}