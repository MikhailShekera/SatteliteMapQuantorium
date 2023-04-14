using System.Collections;
using MyBox;
using UnityEngine;

namespace UnityDevKit.Utils.SceneLoader
{
    public class SceneLoaderObjectToggle : AutoSceneLoadingExtension
    {
        [Separator("Object to toggle")]
        [SerializeField] private GameObject menuUI;
        
        public override IEnumerator Execute()
        {
            menuUI.SetActive(false);
            yield return null;
        }
    }
}