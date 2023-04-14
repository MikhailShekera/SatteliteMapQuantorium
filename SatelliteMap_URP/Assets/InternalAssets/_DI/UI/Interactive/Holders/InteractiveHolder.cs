using UnityEngine;

namespace DI.UI.Interactive.Holders
{
    public abstract class InteractiveHolder : MonoBehaviour, IInteractiveHolder
    {
        public abstract void AddCanvas(GameObject canvasObject);
        public abstract void RemoveCanvas(GameObject canvasObject);
    }
}