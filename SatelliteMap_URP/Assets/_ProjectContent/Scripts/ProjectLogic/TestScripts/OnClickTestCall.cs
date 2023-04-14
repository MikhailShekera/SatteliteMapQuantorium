using UnityEngine;
using UnityEngine.UI;

namespace SatelliteMap.Tests
{
    public class OnClickTestCall : MonoBehaviour
    {
        [SerializeField] private Button button;
        
        private int _currentIndex;
        private Sprite _currentSprite;

        private void Start()
        {
            button.onClick.AddListener(Clicked);
        }

        public void Clicked()
        {
            Debug.Log("Clicked " + _currentIndex);
        }

        public void SetIndex(int index)
        {
            _currentIndex = index;
        }

        public void OpenImage()
        {
            Debug.Log("Open Image: " + _currentIndex);
        }
    }
}