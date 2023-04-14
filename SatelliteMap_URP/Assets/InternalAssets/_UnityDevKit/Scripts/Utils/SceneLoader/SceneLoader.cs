using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityDevKit.XR;

namespace UnityDevKit.Utils.SceneLoader
{
    public class SceneLoader : MonoBehaviour, ISceneLoader, IExtensibleSceneLoader
    {
        private readonly ExtensionsController _beforeLoadExtensions = new ExtensionsController();
        private readonly ExtensionsController _sequencedLoadExtensions = new ExtensionsController();
        private readonly ExtensionsController _parallelLoadExtensions = new ExtensionsController();
        
        private readonly ExtensionsController _sequencedQuitExtensions = new ExtensionsController();
        private readonly ExtensionsController _parallelQuitExtensions = new ExtensionsController();
        
        private const float TARGET_SCENE_LOAD_PROGRESS = 0.9f;

        private sealed class ExtensionsController
        {
            public List<ICoroutineExtension> Extensions { get; } = new List<ICoroutineExtension>();

            public void Add(ICoroutineExtension extension)
            {
                Extensions.Add(extension);
            }
            
            public void Remove(ICoroutineExtension extension)
            {
                Extensions.Remove(extension);
            }
            
            public IEnumerator ExecuteCoroutine()
            {
                foreach (var extension in Extensions)
                {
                    yield return extension.Execute();
                }
            }

            public void Clear()
            {
                Extensions.Clear();
            }
        }

        public void AddLoadingExtension(
            ICoroutineExtension extension,
            SceneLoadMethod sceneLoadMethod,
            SceneLoadingExtensionType extensionType)
        {
            switch (sceneLoadMethod)
            {
                case SceneLoadMethod.SceneLoad:
                    AddLoadingExtension(extension, extensionType);
                    break;
                case SceneLoadMethod.Quit:
                    AddQuitExtension(extension, extensionType);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(sceneLoadMethod), sceneLoadMethod, null);
            }
        }

        private void AddLoadingExtension(ICoroutineExtension extension, SceneLoadingExtensionType extensionType)
        {
            switch (extensionType)
            {
                case SceneLoadingExtensionType.BeforeLoading:
                    _beforeLoadExtensions.Add(extension);
                    break;
                case SceneLoadingExtensionType.SequencedWithLoading:
                    _sequencedLoadExtensions.Add(extension);
                    break;
                case SceneLoadingExtensionType.ParallelWithLoading:
                    _parallelLoadExtensions.Add(extension);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(extensionType), extensionType, null);
            }
        }

        private void AddQuitExtension(ICoroutineExtension extension, SceneLoadingExtensionType extensionType)
        {
            switch (extensionType)
            {
                case SceneLoadingExtensionType.BeforeLoading or SceneLoadingExtensionType.SequencedWithLoading:
                    _sequencedQuitExtensions.Add(extension);
                    break;
                case SceneLoadingExtensionType.ParallelWithLoading:
                    _parallelQuitExtensions.Add(extension);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(extensionType), extensionType, null);
            }
        }

        public void LoadScene(string sceneName, XrMode xrMode)
        {
            XrChanger.Change(xrMode);
            LoadScene(sceneName);
        }
        
        public void LoadScene(string sceneName)
        {
            ExecuteAllExtension(_beforeLoadExtensions);
            StartCoroutine(SwitchSceneAsync(sceneName));
            ExecuteAllExtension(_parallelLoadExtensions);
        }

        public void RestartScene()
        {
            LoadScene(SceneManager.GetActiveScene().name);
        }
        
        public void Quit()
        {
            //Time.timeScale = 1f; // TODO
            ExecuteAllExtension(_parallelQuitExtensions);
            StartCoroutine(QuitProcess());
        }

        private IEnumerator SwitchSceneAsync(string sceneName)
        {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
            asyncLoad.allowSceneActivation = false;

            yield return _sequencedLoadExtensions.ExecuteCoroutine();

            while (asyncLoad.progress < TARGET_SCENE_LOAD_PROGRESS)
            {
                yield return null;
            }

            CLearAllExtensions(); // TODO
            asyncLoad.allowSceneActivation = true;
        }

        private IEnumerator QuitProcess()
        {
            yield return _sequencedQuitExtensions.ExecuteCoroutine();
            Application.Quit();
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }

        private void ExecuteAllExtension(ExtensionsController extensionsController)
        {
            foreach (var extension in extensionsController.Extensions)
            {
                StartCoroutine(extension.Execute());
            }
        }

        private void CLearAllExtensions()
        {
            _beforeLoadExtensions.Clear();
            _sequencedLoadExtensions.Clear();
            _parallelLoadExtensions.Clear();
        }
    }
}