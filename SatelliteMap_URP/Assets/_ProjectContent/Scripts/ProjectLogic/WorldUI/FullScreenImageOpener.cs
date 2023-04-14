using MyBox;
using System.Runtime.Remoting.Metadata;
using SatelliteMap.Data;
using TMPro;
using UnityDevKit.Events;
using UnityEngine;
using UnityEngine.UI;

namespace SatelliteMap.WorldUI
{
    public class FullScreenImageOpener : MonoBehaviour
    {
        [Separator("Processed Image Bodies")]
        [SerializeField] private GameObject singleImageBody;
        [SerializeField] private GameObject doubleImageBody;

        [Separator("Image Containers")]
        [SerializeField] private Image bigImage;

        [SerializeField] private Image bigImageDouble;
        [SerializeField] private Image bigImageDoubleProcessed;

        [Separator("Text Info")]
        [SerializeField] private TMP_Text singleZoneName;
        [SerializeField] private TMP_Text pictureName;

        public readonly EventHolderBase OnWindowOpened = new EventHolderBase();

        private void Start()
        {
            bigImage.preserveAspect = true;
            bigImageDouble.preserveAspect = true;
            bigImageDoubleProcessed.preserveAspect = true;
        }

        public void OpenFullScreenImage(ImagesAndName imagesAndName, string zoneName)
        {
            var hasProcessedImage = imagesAndName.processedImage != null;

            if (!hasProcessedImage)
            {
                bigImage.sprite = imagesAndName.image;
            }
            else
            {
                bigImageDouble.sprite = imagesAndName.image;
                bigImageDoubleProcessed.sprite = imagesAndName.processedImage;
            }

            singleImageBody.SetActive(!hasProcessedImage);
            doubleImageBody.SetActive(hasProcessedImage);

            pictureName.text = imagesAndName.spriteName;
            singleZoneName.text = zoneName;

            OnWindowOpened.Invoke();
        }
    }
}
