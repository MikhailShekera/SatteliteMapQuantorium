using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UnityDevKit.UI
{
    public class InteractiveUiHandler : MonoBehaviour
    {
        [SerializeField] private List<GraphicRaycaster> raycasters;

        public List<GraphicRaycaster> Raycasters => raycasters;

        public void AddCanvas(GraphicRaycaster rayCaster)
        {
            if (raycasters.Contains(rayCaster)) return;
            raycasters.Add(rayCaster);
        }

        public void AddCanvas(GameObject rayCasterObject)
        {
            var rayCaster = rayCasterObject.GetComponentInChildren<GraphicRaycaster>(true);
            if (rayCaster == null) return;
            AddCanvas(rayCaster);
        }

        public void RemoveCanvas(GraphicRaycaster rayCaster)
        {
            if (!raycasters.Contains(rayCaster)) return;
            raycasters.Remove(rayCaster);
        }

        public void RemoveCanvas(GameObject rayCasterObject)
        {
            var rayCaster = rayCasterObject.GetComponentInChildren<GraphicRaycaster>(true);
            if (rayCaster == null) return;
            RemoveCanvas(rayCaster);
        }

        public void RaycastAllUi(
            PointerEventData pointerEventData,
            Action<RaycastResult> action,
            Transform source,
            float distance)
        {
            foreach (var raycaster in raycasters)
            {
                RaycastUi(pointerEventData, action, source, distance, raycaster);
            }
        }

        public void RaycastAllUi<T>(
            PointerEventData pointerEventData,
            Action<T> action,
            Transform source,
            float distance)
        {
            foreach (var raycaster in raycasters)
            {
                RaycastUi(pointerEventData, action, source, distance, raycaster);
            }
        }

        public void RaycastUi(
            PointerEventData pointerEventData,
            Action<RaycastResult> action,
            Transform source,
            float distance,
            GraphicRaycaster raycaster)
        {
            var results = new List<RaycastResult>();
            raycaster.Raycast(pointerEventData, results);
            foreach (var result in results
                         .Where(result =>
                             Vector3.Distance(source.position, result.gameObject.transform.position) < distance))
            {
                action(result);
            }
        }

        public void RaycastUi<T>(
            PointerEventData pointerEventData,
            Action<T> action,
            Transform source,
            float distance,
            GraphicRaycaster raycaster)
        {
            var results = new List<RaycastResult>();
            raycaster.Raycast(pointerEventData, results);
            foreach (var typedResult in results
                         .Where(result =>
                             Vector3.Distance(source.position, result.gameObject.transform.position) < distance)
                         .Select(result => result.gameObject.GetComponent<T>())
                         .Where(result => result != null))
            {
                action(typedResult);
            }
        }
    }
}