using UnityEngine;

namespace SingletonSystem
{
    public abstract class StaticInstance<T> : MonoBehaviour where T : MonoBehaviour
    {
        public static T Instance { get; private set; }
        protected virtual void Awake() => Instance = this as T;

        protected void ClearInstance()
        {
            Instance = null;
            Destroy(this);
        }
    }
}