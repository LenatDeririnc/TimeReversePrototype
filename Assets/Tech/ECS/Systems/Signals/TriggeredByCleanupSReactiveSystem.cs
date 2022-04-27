using System.Collections.Generic;
using Entitas;

namespace ECS.Systems.Signals
{
    public class TriggeredByCleanupSReactiveSystem : ReactiveSystem<GameEntity>
    {
        private readonly Contexts _contexts;

        public TriggeredByCleanupSReactiveSystem(Contexts contexts) : base(contexts.game)
        {
            _contexts = contexts;
        }
    
        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.TriggeredBy.Added());
        }

        protected override bool Filter(GameEntity entity)
        {
            return true;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var e in entities)
            {
                e.RemoveTriggeredBy();
            }
        }
    }
}