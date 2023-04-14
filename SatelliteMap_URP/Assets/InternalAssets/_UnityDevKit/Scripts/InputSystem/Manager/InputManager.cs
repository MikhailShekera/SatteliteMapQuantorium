using UnityDevKit.Events;
using UnityEngine.InputSystem;

namespace UnityDevKit.InputSystem.Manager
{
    public class InputManager
    {
        public readonly EventHolderBase ActionMapChange = new EventHolderBase();

        public PlayerMovementControls MovementControls { get; set; }

        private const string UNIVERSAL_MAP_NAME = "Universal";

        private InputActionMap lastEnabledMap;

        public void CreateControls()
        {
            MovementControls = new PlayerMovementControls();
            ToggleActionMap(MovementControls.Movement);
        }

        public void ToggleActionMap(InputActionMap actionMap)
        {
            if (actionMap.name == UNIVERSAL_MAP_NAME)
            {
                throw new UniversalMapToggleException("If you want to toggle Universal map, use other methods");
            }
            if (actionMap.enabled) return;
            

            MovementControls.Disable();

            TurnOnUniversalMap();
            actionMap.Enable();

            lastEnabledMap = actionMap;

            ActionMapChange.Invoke();
        }

        public void TurnOffAllMaps()
        {
            MovementControls.Disable();
        }

        public void EnableLastMap()
        {
            ToggleActionMap(lastEnabledMap);
        }

        public void TurnOffUniversalMap()
        {
            MovementControls.Universal.Disable();
        }

        public void TurnOnUniversalMap()
        {
            MovementControls.Universal.Enable();
        }
    }
}