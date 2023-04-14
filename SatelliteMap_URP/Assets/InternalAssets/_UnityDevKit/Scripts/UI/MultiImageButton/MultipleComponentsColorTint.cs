using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UnityDevKit.UI.MultiImageButton
{
    public class MultipleComponentsColorTint : MonoBehaviour
    {
        [SerializeField] private Graphic[] targetGraphics;

        public IEnumerable<Graphic> GetTargetGraphics => targetGraphics;
    }
}