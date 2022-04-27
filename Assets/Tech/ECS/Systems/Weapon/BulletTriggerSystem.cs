using System.Collections.Generic;
using Entitas;

namespace ECS.Systems.Weapon
{
    public class BulletTriggerSystem : ReactiveSystem<GameEntity>
    {
        public BulletTriggerSystem(Contexts contexts) : base(contexts.game)
        {
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector( GameMatcher.TriggerSignal.Added());
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.isBullet;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var entity in entities)
            {
                var otherEntity = EcsManager.GameObjectEntityTools.GetEntityByCollider(entity.triggerSignal.Collider);
                if (otherEntity == null)
                {
                    entity.gameObject.Value.SetActive(false);
                    return;
                }
                otherEntity.isDead = true;
            }
        }
    }
}