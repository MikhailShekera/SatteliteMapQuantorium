using System;
using MyBox;
using UnityDevKit.XR;
using UnityEngine;
using Zenject;

namespace UnityDevKit.Player.Builders
{
    public sealed class PlayerBuilder : MonoBehaviour
    {
        [Separator("Settings")] 
        //[SerializeField] private Transform startPosition;
        [SerializeField] private Transform holder;

        [Separator("Additional settings")] 
        [SerializeField] private bool useManualXrSetup;
        [SerializeField, ConditionalField(nameof(useManualXrSetup))] private XrMode startXrMode = XrMode.Desktop;
        
        [Separator("Prefabs")]
        [SerializeField] private GameObject desktopPlayerPrefab;
        [SerializeField] private GameObject vrPlayerPrefab;

        private DiContainer _diContainer;
        
        [Inject]
        public void Construct(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }
        
        private void Awake()
        {
            if (useManualXrSetup)
            {
                XrChanger.Change(startXrMode);
            }
            
            var currentXrMode = XrChanger.XrMode;
            Build(currentXrMode);
        }
        
        private void Build(XrMode xrMode)
        {
            var currentModePrefab = SelectPlayerPrefab(xrMode);
            var playerObject = _diContainer.InstantiatePrefab(currentModePrefab, holder);
            Debug.Log($"Player `{playerObject.name}` (mode: {xrMode.ToString()}) was instantiated");
        }

        private GameObject SelectPlayerPrefab(XrMode xrMode) =>
            xrMode switch
            {
                XrMode.Desktop => desktopPlayerPrefab,
                XrMode.Vr => vrPlayerPrefab,
                _ => throw new ArgumentOutOfRangeException(nameof(xrMode), xrMode, null)
            };
    }
}