using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Addressables
{
    public class AddressablesAssetLoader
    {
        private GameObject _cachedObject;

        public virtual async Task<GameObject> LoadGameObject(string assetId)
        {
            var handle = UnityEngine.AddressableAssets.Addressables.InstantiateAsync(assetId);
            _cachedObject = await handle.Task;
            
            return _cachedObject;
        }

        public virtual async Task<T> LoadComponent<T>(string assetId)
        {
            var handle = UnityEngine.AddressableAssets.Addressables.InstantiateAsync(assetId);
            _cachedObject = await handle.Task;
            if (_cachedObject.TryGetComponent(out T component) == false)
                throw new NullReferenceException($"Object type {typeof(T)} is null " +
                                                 $"on attempt to load it from addressables");
            return component;
        }

        public virtual void Unload()
        {
            if (_cachedObject == null)
                return;
                
            _cachedObject.SetActive(false);
            UnityEngine.AddressableAssets.Addressables.ReleaseInstance(_cachedObject);
            _cachedObject = null;
        }
    }
}