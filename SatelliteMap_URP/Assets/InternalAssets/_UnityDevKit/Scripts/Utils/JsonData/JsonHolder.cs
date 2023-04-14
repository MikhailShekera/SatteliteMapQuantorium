using System.IO;
using UnityEngine;

namespace UnityDevkit.Utils.JsonData
{
    public abstract class JsonHolder<T> : ScriptableObject
    {
        public string fileName;
        public T data;
        public JsonHolder<T> defaultScriptableObject;

        protected readonly string JsonFileDirectory = Application.streamingAssetsPath + Path.AltDirectorySeparatorChar + "PlayerData";

        public abstract bool Validate(T validationData);
    }
}