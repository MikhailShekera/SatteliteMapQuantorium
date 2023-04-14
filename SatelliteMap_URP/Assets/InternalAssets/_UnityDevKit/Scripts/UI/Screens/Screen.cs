using MyBox;
using UnityEngine;

namespace UnityDevKit.UI.Screens
{
    public abstract class Screen : MonoBehaviour, IScreen
    {
        [Separator("Screens references")]
        [SerializeField] private Screen next;
        [SerializeField] private Screen previous;
        
        [Separator("UI")] 
        [SerializeField] private GameObject uiPanel;

        protected bool IsOpen;
        
        protected IScreen GetNext() => next;
        protected IScreen GetPrevious() => previous;

        public virtual void Show()
        {
            IsOpen = true;
            uiPanel.SetActive(true);
        }

        public virtual void Hide()
        {
            IsOpen = false;
            uiPanel.SetActive(false);
        }
        
        public IScreen PowerAction(bool isPowered)
        {
            if (isPowered)
            {
                Show();
            }
            else
            {
                Hide();
            }

            return this;
        }
        
        public abstract IScreen UpAction();
        public abstract IScreen DownAction();
        public abstract IScreen ConfirmAction();
        public abstract IScreen BackAction();
        public abstract IScreen StartStopAction();
    }
}