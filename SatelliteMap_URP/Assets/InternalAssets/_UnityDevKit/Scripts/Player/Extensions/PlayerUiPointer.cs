using UnityEngine;
using UnityEngine.EventSystems;

namespace UnityDevKit.Player.Extensions
{
    public class PlayerUiPointer : PlayerExtension
    {
        [SerializeField] private Camera viewCamera;

        public Camera ViewCamera => viewCamera;
        public PointerEventData PointerEventData { get; internal set; }
        public GameObject CurrentUIElement { get; internal set; }
        
        public bool Process()
        {
            PointerEventData.Reset();
            PointerEventData.position = Input.mousePosition;
            PointerEventData.scrollDelta = Vector2.zero;
            return IsBlocked;
        }
    }
}