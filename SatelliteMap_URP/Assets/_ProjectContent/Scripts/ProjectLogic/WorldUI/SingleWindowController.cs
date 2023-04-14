using UnityEngine;
using MyBox;
using TMPro;
using UnityDevKit.Events;
using UnityEngine.UI;
using System.Collections;
using SatelliteMap.Data;

namespace SatelliteMap.WorldUI
{
    public class SingleWindowController : MonoBehaviour
    {
        [Separator("Content Window")] 
        [SerializeField] private Transform contentWindow;
        [SerializeField] private GameObject imagesList;
        [SerializeField] private GameObject fullScreenImage;
        [SerializeField] private Transform canvas;

        [Separator("Image-Button Prefab")] 
        [SerializeField] public GameObject imageButton;

        [Separator("Header")] 
        [SerializeField] private TMP_Text zoneName;
        [SerializeField] private TMP_Text numberOfImages;

        [Separator("Full Screen Image Controller")] 
        [SerializeField] private FullScreenImageOpener fullScreenImageOpener;

        [Separator("Grid Layout")] 
        [SerializeField] private GridLayoutGroup gridLayoutGroup;

        [Separator("ImageSpawnDelay")] 
        [SerializeField] private float imageSpawnDelay = 0.025f;

        public Transform ContentWindow => contentWindow;
        public GameObject Canvas => canvas.gameObject;
        public TMP_Text ZoneName => zoneName;
        public GameObject FullScreenImage => fullScreenImage;
        public TMP_Text NumberOfImages => numberOfImages;

        public EventHolderBase OnWindowClose { get; } = new EventHolderBase();

        private AudioSource _soundSource;

        private void Awake()
        {
            _soundSource = GetComponentInParent<AudioSource>();
        }

        private void Start()
        {
            fullScreenImageOpener.OnWindowOpened.AddListener(ToggleScreensInSingleWindow);
        }

        public void ToggleScreensInSingleWindow()
        {
            imagesList.SetActive(!imagesList.activeSelf);
            fullScreenImage.SetActive(!fullScreenImage.activeSelf);
            _soundSource.Play();
        }

        public void CloseWindow()
        {
            canvas.gameObject.SetActive(false);
            OnWindowClose.Invoke();
            _soundSource.Play();
        }

        public void SetUpImagesAndButtons(ZonePhotos data)
        {
            zoneName.text = data.zoneName;
            numberOfImages.text = data.images.Length.ToString();

            StartCoroutine(GradualInstantiate(data));
        }

        private IEnumerator GradualInstantiate(ZonePhotos data)
        {
            for (var i = 0; i < data.images.Length; i++)
            {
                var image = data.images[i].image;
                var spriteName = data.images[i].spriteName;

                ImagesAndName singlePhoto = data.images[i];
                var newChild = Instantiate(imageButton, contentWindow);

                var currentDataSaver = newChild.GetComponentInChildren<SingleImageDataSaver>();
                currentDataSaver.SetSingleImageData(image, spriteName, gridLayoutGroup.cellSize);

                var imageClickEvent = currentDataSaver.OnImageClick();
                imageClickEvent.AddListener(delegate
                {
                    fullScreenImageOpener.OpenFullScreenImage(singlePhoto, zoneName.text);
                });
                imageClickEvent.AddListener(_soundSource.Play);

                yield return new WaitForSeconds(imageSpawnDelay);
            }
        }

        public void TurnOnImagesList()
        {
            imagesList.SetActive(true);
        }
    }
}