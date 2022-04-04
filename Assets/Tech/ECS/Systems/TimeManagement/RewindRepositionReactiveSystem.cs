using Common;
using Entitas;
using UnityEngine;

namespace ECS.Systems
{
    public class RewindRepositionReactiveSystem : IExecuteSystem
    {
        private readonly Contexts _contexts;

        public RewindRepositionReactiveSystem(Contexts contexts)
        {
            _contexts = contexts;
        }

        public void Execute()
        {
            if (!_contexts.time.isRollback)
                return;
            
            if (!_contexts.time.hasRewindPosition)
                return;
            
            var playerTransform = _contexts.game.playerEntity.transform.Value;
            var playerTransformInfo = _contexts.game.playerEntity.transformInfo;
            var tm = _contexts.time.timeManagerHandler.Value;
            
            var newPosition = _contexts.time.rewindPosition.Value;
            var lastPosition = _contexts.time.timelineLastPosition.Value;

            var data = new TransformInfo()
            {
                position = Vector3.Lerp(lastPosition.position, newPosition.position, 1 - tm.TickRateDivideRatio),
                rotation = Quaternion.Lerp(lastPosition.rotation, newPosition.rotation, 1 - tm.TickRateDivideRatio),
            };
            
            playerTransform.position = data.position;
            playerTransform.rotation = data.rotation;

            playerTransformInfo.Value = new TransformInfo(playerTransform.transform);
        }
    }
}