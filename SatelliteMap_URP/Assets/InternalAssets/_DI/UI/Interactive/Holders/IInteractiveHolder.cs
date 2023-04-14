using UnityEngine;

namespace DI.UI.Interactive.Holders
{
    public interface IInteractiveHolder
    {
        void AddCanvas(GameObject canvasObject);
        
        void RemoveCanvas(GameObject canvasObject);
    }
}