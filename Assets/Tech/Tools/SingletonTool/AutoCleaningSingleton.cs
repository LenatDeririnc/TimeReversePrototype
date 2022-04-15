using UnityEngine;

namespace Tools.SingletonTool
{
    public abstract class AutoCleaningSingleton<T> : Singleton<T> where T : MonoBehaviour
    {
        protected virtual void OnApplicationQuit()
        {
            ClearInstance();
        }
    }
}