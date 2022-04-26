using System.Collections.Generic;
using Entitas;

namespace ECS.Systems
{
    public class SetFlagSystem : ReactiveSystem<GameEntity>
    {
        private readonly Contexts _contexts;

        public SetFlagSystem(Contexts contexts) : base(contexts.game)
        {
            _contexts = contexts;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.SetFlagSignal.Added());
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.setFlagSignal.Delegate != null;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var flagSignal in entities)
            {
                flagSignal.setFlagSignal.Delegate.Invoke(flagSignal.setFlagSignal.Value);
                flagSignal.RemoveSetFlagSignal();
            }
        }
    }
}