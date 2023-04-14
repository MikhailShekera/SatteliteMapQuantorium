using System;
using UnityDevKit.Optimization;
using UnityDevKit.Player.Controllers;

namespace UnityDevKit.Player.Extensions
{
    public abstract class PlayerExtension : CachedMonoBehaviour
    {
        public PlayerController PlayerController { get; protected set; }

        private bool _isBlocked;

        protected bool IsBlocked => _isBlocked;
        
        protected override void Awake()
        {
            base.Awake();

            PlayerController = GetComponentInParent<PlayerController>();
            if (PlayerController == null)
            {
                throw new NullReferenceException("Player extension can't get reference to player controller");
            }
        }

        protected virtual void Start()
        {
            PlayerController.OnPlayerBlock.AddListener(Block);
            PlayerController.OnPlayerUnblock.AddListener(Unblock);
        }

        private void Update()
        {
            if (_isBlocked) return;
            PlayerLoop();
        }

        protected virtual void PlayerLoop()
        {
        }

        private void Block()
        {
            _isBlocked = true;
        }
        
        private void Unblock()
        {
            _isBlocked = false;
        }
    }
}