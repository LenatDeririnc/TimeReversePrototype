using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace ECS.Systems.Characters.Player
{
    public class CameraPitchReactiveSystem : ReactiveSystem<GameEntity>
    {
        private readonly Contexts _contexts;

        public CameraPitchReactiveSystem(Contexts contexts) : base(contexts.game)
        {
            _contexts = contexts;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.CameraPitchAngle);
        }

        protected override bool Filter(GameEntity entity)
        {
            return true;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            _contexts.game.playerCameraEntity.transform.Value.rotation =
                Quaternion.Euler(_contexts.game.cameraPitchAngleEntity.cameraPitchAngle.Value, 0, 0);
        }
    }
}