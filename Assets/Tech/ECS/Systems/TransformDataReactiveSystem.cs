using System.Collections.Generic;
using Entitas;

namespace ECS.Systems
{
    public class TransformDataReactiveSystem : ReactiveSystem<GameEntity>
    {
        public TransformDataReactiveSystem(Contexts contexts) : base(contexts.game)
        {
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.TransformInfo);
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.hasTransform;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var e in entities)
            {
                e.transform.Value.position = e.transformInfo.Value.position;
                e.transform.Value.rotation = e.transformInfo.Value.rotation;
            }
        }
    }
}