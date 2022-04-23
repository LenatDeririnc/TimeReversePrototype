using System;
using System.Collections.Generic;
using Entitas;

namespace ECS.Systems
{
    public class ColliderDataSignalReactiveSystem : ReactiveSystem<GameEntity>
    {
        private readonly Contexts _contexts;

        public ColliderDataSignalReactiveSystem(Contexts contexts) : base(contexts.game)
        {
            _contexts = contexts;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.AddColliderDataSignal.Added());
        }

        protected override bool Filter(GameEntity entity)
        {
            return true;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var e in entities)
            {
                _contexts.game.colliderData.Values[e.addColliderDataSignal.Collider] = e.addColliderDataSignal.GameEntity;
                e.RemoveAddColliderDataSignal();
            }
        }
    }
}