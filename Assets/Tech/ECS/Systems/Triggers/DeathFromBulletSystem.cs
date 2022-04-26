using System.Collections.Generic;
using Entitas;

namespace ECS.Systems.Triggers
{
    public class DeathFromBulletSystem : ReactiveSystem<GameEntity>
    {
        private readonly Contexts _contexts;

        public DeathFromBulletSystem(Contexts contexts) : base(contexts.game)
        {
            _contexts = contexts;
        }
    
        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.TriggeredBy.Added());
        }

        protected override bool Filter(GameEntity entity)
        {
            return !_contexts.time.isRollback & entity.triggeredBy.Entity.isBullet & entity.isDead == false;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var e in entities)
            {
                e.isDead = true;
            }
        }
    }
}