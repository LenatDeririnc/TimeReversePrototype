using Common;
using Entitas;
using UnityEngine;

namespace ECS.Systems.TimeManagement
{
    public class RollbackMovementSystem : IInitializeSystem, IExecuteSystem
    {
        private readonly Contexts _contexts;

        public RollbackMovementSystem(Contexts contexts)
        {
            _contexts = contexts;
        }

        public void Initialize()
        {
            
        }

        public void Execute()
        {
            if (!_contexts.time.isRollback)
                return;
            
            var timeManager = _contexts.time.timeManagerHandlerEntity.timeManagerHandler.Value;
            var player = _contexts.game.playerEntity;

            var lastPosition = _contexts.game.playerEntity.transformInfo.Value;
            var newPosition = _contexts.time.timelineData.Value.Peek();

            player.transform.Value.position = Vector3.Lerp(lastPosition.position, newPosition.position, 1-timeManager.TickRateDivideRatio);
            player.transform.Value.rotation = Quaternion.Lerp(lastPosition.rotation, newPosition.rotation, 1-timeManager.TickRateDivideRatio);
            player.transformInfo.Value = new TransformInfo(player.transform.Value);
        }
    }
}