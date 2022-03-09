using DamageSystem;
using UnityEngine;

namespace ObjectSystems
{
    public class DamagableTrigger : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            var destroyable = other.GetComponent<IDestroyable>();
            destroyable?.Destroy();
        }
    }
}