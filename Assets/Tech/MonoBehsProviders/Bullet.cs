using ECS;
using ECS.Mono;
using UnityEngine;

namespace MonoBehsProviders
{
    public class Bullet : MonoGameObjectEntity
    {
        protected override void Awake()
        {
            base.Awake();
            Entity.isBullet = true;
        }

        public override void Destroy()
        {
            var timelineStack = Contexts.time.timeLineStack.Value;
            timelineStack.Clear(Entity.entityID.Value);
            base.Destroy();
        }

        private void OnTriggerEnter(Collider other)
        {
            var otherEntity = EcsManager.GameObjectEntityTools.GetEntityByCollider(other);
            if (otherEntity == null)
            {
                Entity.gameObject.Value.SetActive(false);
                return;
            }
            otherEntity.isDead = true;
        }
    }
}