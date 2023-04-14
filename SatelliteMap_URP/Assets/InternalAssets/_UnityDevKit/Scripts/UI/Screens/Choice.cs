using System;
using UnityEngine;

namespace UnityDevKit.UI.Screens
{
    [Serializable]
    public struct Choice
    {
        public string Name;
        public bool IsCorrect;
        public GameObject Obj;
    }
}