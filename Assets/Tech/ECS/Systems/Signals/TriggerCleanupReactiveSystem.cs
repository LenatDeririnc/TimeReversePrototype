using System.Collections.Generic;
using Entitas;

namespace ECS.Systems.Signals
{
    public class TriggerCleanupReactiveSystem : ReactiveSystem<GameEntity>
    {
        private readonly Contexts _contexts;

        public TriggerCleanupReactiveSystem(Contexts contexts) : base(contexts.game)
        {
            _contexts = contexts;
        }
    
        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.TriggerSignal.Added());
        }

        protected override bool Filter(GameEntity entity)
        {
            return true;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var e in entities)
            {
                e.RemoveTriggerSignal();
            }
        }
    }
}