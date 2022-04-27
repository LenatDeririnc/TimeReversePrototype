using ECS.Mono;
using UnityEngine;

namespace MonoBehsProviders
{
    public class DynamicRigidbody : MonoGameObjectEntity
    {
        public Rigidbody Rigidbody;

        protected override void Awake()
        {
            base.Awake();
            Entity.ReplaceDynamicRigidbody(Rigidbody);
        }
    }
}