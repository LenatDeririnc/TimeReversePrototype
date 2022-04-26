using System.Collections.Generic;
using Entitas;

namespace ECS.Systems
{
    public class DestroyEntitySystem : ReactiveSystem<GameEntity>
    {
        public DestroyEntitySystem(Contexts contexts) : base(contexts.game)
        {
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.DestroyEntitySignal.Added());
        }

        protected override bool Filter(GameEntity entity)
        {
            return true;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var e in entities)
            {
                e.Destroy();
            }
        }
    }
}