using MyBox;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SatelliteMap.WorldUI
{
    public class SingleImageDataSaver : MonoBehaviour
    {
        [Separator("Main fields")]
        [SerializeField] private Image image;
        [SerializeField] private Button button;
        [SerializeField] private TMP_Text imageName;
        
        public void SetSingleImageData(Sprite currentImage, string currentImageName, Vector2 cellSize)
        {
            image.sprite = currentImage;
            image.preserveAspect = true;
            imageName.text = currentImageName;
        }

        public Button.ButtonClickedEvent OnImageClick()
        {
            return button.onClick;
        }
    }
}
