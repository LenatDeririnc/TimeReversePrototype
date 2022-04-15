using Common;
using Entitas;
using UnityEngine;

namespace ECS.Systems.TimeManagement
{
    public class RewindSystem : IExecuteSystem
    {
        private readonly Contexts _contexts;

        public RewindSystem(Contexts contexts)
        {
            _contexts = contexts;
        }

        public void Execute()
        {
            if (!_contexts.time.isRollback)
                return;
            
            if (!_contexts.time.hasTimelineRewindPosition)
                return;
            
            var playerTransform = _contexts.game.playerEntity.transform.Value;
            var playerTransformInfo = _contexts.game.playerEntity.transformInfo;
            var camera = _contexts.game.playerCameraEntity;
            
            var tm = _contexts.time.timeManagerHandler.Value;
            
            var newPosition = _contexts.time.timelineRewindPosition.Value;
            var lastPosition = _contexts.time.timelineLastPosition.Value;

            playerTransform.position = Vector3.Lerp(lastPosition.playerPosition, newPosition.playerPosition, 1 - tm.TickRateDivideRatio);
            playerTransform.rotation = Quaternion.Lerp(lastPosition.playerRotation, newPosition.playerRotation, 1 - tm.TickRateDivideRatio);
            camera.transform.Value.localRotation = Quaternion.Lerp(
                    Quaternion.Euler(lastPosition.cameraAngle, 0, 0), 
                    Quaternion.Euler(newPosition.cameraAngle, 0, 0), 
                    1 - tm.TickRateDivideRatio);

            playerTransformInfo.Value = new TransformInfo(playerTransform.transform);
        }
    }
}