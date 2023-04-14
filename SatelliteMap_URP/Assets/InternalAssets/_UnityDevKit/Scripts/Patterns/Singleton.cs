using UnityEngine;

namespace UnityDevKit.Patterns
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T instance;

        public virtual void Awake()
        {
            if (!instance)
            {
                var selfObject = gameObject;
                instance = selfObject.GetComponent<T>();
                DontDestroyOnLoad(selfObject);
            }
            else
            {
                Debug.LogError("[Singleton] Too many instances of '" + typeof(T) + "' has been created! (" +
                               FindObjectsOfType(typeof(T)).Length + " instances)");
                Destroy(gameObject);
            }
        }

        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = (T) FindObjectOfType(typeof(T));
                    var instancesLength = FindObjectsOfType(typeof(T)).Length;
                    if (instancesLength > 1)
                        Debug.LogError("[Singleton] multiple instances of '" + typeof(T) + "' found! (" +
                                       instancesLength + " instances)");

                    if (instance == null)
                    {
                        var singleton = new GameObject();
                        instance = singleton.AddComponent<T>();
                        singleton.name = "(singleton) " + typeof(T).ToString();
                        DontDestroyOnLoad(singleton);
                        Debug.Log("[Singleton] An instance of '" + typeof(T) + "' was created: " + singleton);
                    }
                    else Debug.Log("[Singleton] Using instance of '" + typeof(T) + "': " + instance.gameObject.name);
                }

                return instance;
            }
        }
    }
}