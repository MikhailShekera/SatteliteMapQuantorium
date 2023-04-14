#if UNITY_EDITOR
using System;
using UnityDevKit.Effects.Highlight.Loaders;
using UnityDevKit.Interactables.Extensions;
using UnityEditor;
using UnityEngine;

namespace UnityDevKit.Interactables.Editor
{
    [Obsolete("Class is obsolete", false)]
    public class InteractableEditWindow : EditorWindow
    {
        private GameObject _currentSelected;

        private SerializedObject _serializedObject;
        
        private const string WINDOW_NAME = "Interactable Edit";
        private const int SECTION_GAP_PIXELS = 15;
        private const int TINY_GAP_PIXELS = 5;

        [MenuItem("Tools/" + WINDOW_NAME)]
        public static void Init()
        {
            var window = GetWindow<InteractableEditWindow>();
            window.Show();
        }

        private void OnGUI()
        {
            titleContent = new GUIContent(WINDOW_NAME);
            _serializedObject = new SerializedObject(this);
            _currentSelected = Selection.activeGameObject;
            if (_currentSelected == null)
            {
                EditorGUILayout.HelpBox("Select interactable object.", MessageType.Warning);
                return;
            }

            var interactable = _currentSelected.GetComponent<InteractableBase>();
            if (interactable)
            {
                if (GUILayout.Button("Remove interactable logic"))
                {
                    RemoveInteractableLogic(interactable);
                }
                else
                {
                    EditInteractable(interactable);
                }
            }
            else
            {
                if (GUILayout.Button("Setup interactable logic"))
                {
                    SetupInteractable();
                }
            }
            _serializedObject.ApplyModifiedProperties();
        }

        private void EditInteractable(InteractableBase interactable)
        {
            // Get extensions
            var interactableEvents = interactable.GetComponent<InteractableEvents>();
            var interactableHighlight = interactable.GetComponent<InteractableHighlighter>();
            var interactableSound = interactable.GetComponent<InteractableSound>();
            var interactableAnimator = interactable.GetComponent<InteractableAnimator>();
            var interactableAnimatedButton = interactable.GetComponent<InteractableAnimatedButton>();
            
            // Add extensions
            var hasAllExtension = interactableEvents 
                                  && interactableHighlight
                                  && interactableSound
                                  && interactableAnimator
                                  && interactableAnimatedButton;
            
            GUILayout.Space(SECTION_GAP_PIXELS);
            EditorGUILayout.BeginVertical("box");
            GUILayout.Label("Add extensions", EditorStyles.boldLabel);
            if (!hasAllExtension)
            {
                if (!interactableEvents)
                {
                    if (GUILayout.Button("Add Unity Events"))
                    {
                        _currentSelected.AddComponent<InteractableEvents>();
                    }
                }

                if (!interactableHighlight)
                {
                    if (GUILayout.Button("Add Highlighter"))
                    {
                        interactableHighlight = _currentSelected.AddComponent<InteractableHighlighter>();
                        interactableHighlight.AddEffect();
                    }
                }
                if (!interactableSound)
                {
                    if (GUILayout.Button("Add Sound"))
                    {
                        _currentSelected.AddComponent<InteractableSound>();
                    }
                }
                if (!interactableAnimator)
                {
                    if (GUILayout.Button("Add Animator"))
                    {
                        _currentSelected.AddComponent<InteractableAnimator>();
                    }
                }
                if (!interactableAnimatedButton)
                {
                    if (GUILayout.Button("Add Animated Button"))
                    {
                        interactableAnimatedButton = _currentSelected.AddComponent<InteractableAnimatedButton>();
                        interactableAnimatedButton.AddAnimatedButton();
                    }
                }
            }
            else
            {
                EditorGUILayout.HelpBox("Nothing to add.", MessageType.Info);
            }
            EditorGUILayout.EndVertical();

            // Remove extensions
            var hasAnyExtension = interactableEvents 
                                  || interactableHighlight
                                  || interactableSound
                                  || interactableAnimator
                                  || interactableAnimatedButton;

            GUILayout.Space(SECTION_GAP_PIXELS);
            EditorGUILayout.BeginVertical("box");
            GUILayout.Label("Remove extensions", EditorStyles.boldLabel);
            if (hasAnyExtension)
            {
                if (interactableEvents)
                {
                    if (GUILayout.Button("Remove Unity Events"))
                    {
                        RemoveUnityEventsExtension(interactableEvents);
                    }
                }
                if (interactableHighlight)
                {
                    if (GUILayout.Button("Remove Highlighter"))
                    {
                        RemoveHighlighterExtension(interactableHighlight);
                    }
                }
                if (interactableSound)
                {
                    if (GUILayout.Button("Remove Sound"))
                    {
                        RemoveSoundExtension(interactableSound);
                    }
                }
                if (interactableAnimator)
                {
                    if (GUILayout.Button("Remove Animator"))
                    {
                        RemoveAnimatorExtension(interactableAnimator);
                    }
                }
                if (interactableAnimatedButton)
                {
                    if (GUILayout.Button("Remove Animated Button"))
                    {
                        RemoveAnimatedButtonExtension(interactableAnimatedButton);
                    }
                }
                
                // Remove all
                GUILayout.Space(SECTION_GAP_PIXELS);
                if (GUILayout.Button("Remove all extensions"))
                {
                    ClearInteractableExtensions();
                }
            }
            else
            {
                EditorGUILayout.HelpBox("Nothing to remove.", MessageType.Info);
            }
            EditorGUILayout.EndVertical();
        }

        private void SetupInteractable()
        {
            var collider = _currentSelected.GetComponent<Collider>();
            if (collider == null)
            {
                _currentSelected.AddComponent<BoxCollider>();
            }

            _currentSelected.AddComponent<InteractableBase>();
        }

        private void RemoveInteractableLogic(InteractableBase interactable)
        {
            ClearInteractableExtensions();
            DestroyImmediate(interactable);
        }

        private static void RemoveUnityEventsExtension(InteractableEvents interactableEvents)
        {
            DestroyImmediate(interactableEvents);
        }
        
        private static void RemoveHighlighterExtension(InteractableHighlighter interactableHighlight)
        {
            var effect = interactableHighlight.Effect;
            if (effect)
            {
                DestroyImmediate(effect);
            }

            var propertiesLoader = interactableHighlight.gameObject.GetComponent<InteractablePropertiesLoader>();
            if (propertiesLoader)
            {
                DestroyImmediate(propertiesLoader);
            }
            DestroyImmediate(interactableHighlight);
        }
        
        private static void RemoveSoundExtension(InteractableSound interactableSound)
        {
            DestroyImmediate(interactableSound);
        }
        
        private static void RemoveAnimatorExtension(InteractableAnimator interactableAnimator)
        {
            var animator = interactableAnimator.gameObject.GetComponent<Animator>();
            DestroyImmediate(interactableAnimator);
            if (animator)
            {
                DestroyImmediate(animator);
            }
        }
        
        private static void RemoveAnimatedButtonExtension(InteractableAnimatedButton interactableAnimatedButton)
        {
            var animatedButton = interactableAnimatedButton.animatedButton;
            if (animatedButton)
            {
                DestroyImmediate(animatedButton);
            }
            DestroyImmediate(interactableAnimatedButton);
        }

        private void ClearInteractableExtensions()
        {
            foreach (var extension in _currentSelected.GetComponents<InteractableExtension>())
            {
                switch (extension)
                {
                        case InteractableEvents e:
                            RemoveUnityEventsExtension(e);
                            break;
                        case InteractableHighlighter e:
                            RemoveHighlighterExtension(e);
                            break;
                        case InteractableSound e:
                            RemoveSoundExtension(e);
                            break;
                        case InteractableAnimator e:
                            RemoveAnimatorExtension(e);
                            break;
                        case InteractableAnimatedButton e:
                            RemoveAnimatedButtonExtension(e);
                            break;
                        default:
                            DestroyImmediate(extension);
                            break;
                }
            }
        }
    }
}
#endif