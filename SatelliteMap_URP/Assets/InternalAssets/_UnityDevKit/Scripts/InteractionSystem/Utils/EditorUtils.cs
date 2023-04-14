#if UNITY_EDITOR
using UnityDevKit.InteractionSystem.Interactable;
using UnityDevKit.InteractionSystem.Interactable.Child;
using UnityEditor;
using UnityEngine;

namespace UnityDevKit.InteractionSystem.Utils
{
    public static class EditorUtils
    {
        private const string MENU_PATH = "GameObject/Interaction System";
        private const string CREATE_MENU_PATH = MENU_PATH + "/Create";
        private const string RESOURCES_PATH = "InteractionSystem/Prototypes";
        
        [MenuItem(CREATE_MENU_PATH + "/Interactable Empty Object", false, 30)]
        public static void CreateInteractableEmptyObject(MenuCommand menuCommand)
        {
            CreateInteractableObject("InteractableEmptyObject");
        }

        [MenuItem(CREATE_MENU_PATH + "/Interactable Example Cube", false, 30)]
        public static void CreateInteractableCubeExample(MenuCommand menuCommand)
        {
            CreateInteractableObject("InteractableCubeExample");
        }
        
        [MenuItem(MENU_PATH + "/Add Child Extension", false, 30)]
        public static void AddChildExtension(MenuCommand menuCommand)
        {
            var childGameObject = new GameObject("ChildExtension");
            var selected = Selection.activeGameObject;
            if (selected)
            {
                childGameObject.transform.SetParent(selected.transform);
            }

            var interactableChild = childGameObject.AddComponent<InteractableChild>();
            var interactable = selected.GetComponentInParent<BaseInteractable>();
            if (interactable)
            {
                interactableChild.SetInteractionParent(interactable);
                Debug.Log("Successfully add and setup interactable child");
            }
            else
            {
                Debug.Log("Successfully add interactable child, but you have to setup parent interactable");
            }
        }

        private static void CreateInteractableObject(string prototypeName)
        {
            var selected = Selection.activeGameObject;

            var interactableObjectPrefab = Resources.Load($"{RESOURCES_PATH}/{prototypeName}");
            var interactableObject =
                (GameObject) Object.Instantiate(interactableObjectPrefab, Vector3.zero, Quaternion.identity);
            if (selected)
            {
                interactableObject.transform.SetParent(selected.transform);
            }

            Selection.activeGameObject = interactableObject;

            Debug.Log("Interactable object has been created.");
        }
    }
}
#endif