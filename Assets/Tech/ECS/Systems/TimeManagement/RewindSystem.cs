using Common;
using ECS.Extensions;
using Entitas;
using UnityEngine;

namespace ECS.Systems.TimeManagement
{
    public class RewindSystem : IExecuteSystem
    {
        private readonly TimeContext _timeContext;
        private readonly GameContext _gameContext;

        public RewindSystem(Contexts contexts)
        {
            _gameContext = contexts.game;
            _timeContext = contexts.time;
        }

        public void Execute()
        {
            if (!_timeContext.isRollback)
                return;
            
            if (!_timeContext.hasTimelineRewindPosition)
                return;

            var playerTransform = _gameContext.playerEntity.transform.Value;
            var playerTransformInfo = _gameContext.playerEntity.transformInfo;
            var camera = _gameContext.playerCameraEntity;

            var newPosition = _timeContext.timelineRewindPosition.Value;
            var lastPosition = _timeContext.timelineLastPosition.Value;
            var divideRatio = _timeContext.TickRateDivideRatio();

            playerTransform.position = Vector3.Lerp(lastPosition.playerPosition, newPosition.playerPosition, 1 - divideRatio);
            playerTransform.rotation = Quaternion.Lerp(lastPosition.playerRotation, newPosition.playerRotation, 1 - divideRatio);
            camera.transform.Value.localRotation = Quaternion.Lerp(
                    Quaternion.Euler(lastPosition.cameraAngle, 0, 0), 
                    Quaternion.Euler(newPosition.cameraAngle, 0, 0), 
                    1 - divideRatio);

            playerTransformInfo.Value = new TransformInfo(playerTransform.transform);
        }
    }
}