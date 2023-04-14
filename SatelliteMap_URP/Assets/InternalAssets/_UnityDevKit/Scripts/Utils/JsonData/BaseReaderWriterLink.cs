using MyBox;
using UnityEngine;

namespace UnityDevkit.Utils.JsonData
{
    public abstract class BaseReaderWriterLink<TData> : MonoBehaviour
    {
        [Separator("Scriptable Object")]
        [SerializeField] private JsonHolder<TData> scriptableObject;

        [Separator("Load Flag")]
        [SerializeField] private bool loadOnAwake = true;

        public JsonHolder<TData> ScriptableObject => scriptableObject;


        private void Awake()
        {
            if (loadOnAwake)
            {
                JsonReaderWriter<TData>.LoadData(scriptableObject);
            }
        }

        public async void SaveData()
        {
            await JsonReaderWriter<TData>.SaveDataAsync(scriptableObject);
        }

        public void RewriteData(TData data)
        {
            scriptableObject.data = data;
        }
    }
}