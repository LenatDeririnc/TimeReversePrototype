using Common;
using Entitas;
using UnityEngine;

namespace ECS.Systems.TimeManagement
{
    public class RewindStartPlayerTimelineSystem : IExecuteSystem, IInitializeSystem
    {
        private readonly TimeContext _timeContext;
        private readonly GameContext _gameContext;

        public RewindStartPlayerTimelineSystem(Contexts contexts)
        {
            _gameContext = contexts.game;
            _timeContext = contexts.time;
        }

        public void Initialize()
        {
            _timeContext.SetSmoothRewindSpeed(10);
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
            var divideRatio = (_timeContext.time.Value - newPosition.pushTime) / (lastPosition.pushTime - newPosition.pushTime);
            
            var deltaTime = Time.deltaTime * _timeContext.smoothRewindSpeed.value;

            playerTransform.position =
                Vector3.Lerp(playerTransform.position,  
                Vector3.Lerp(lastPosition.playerPosition, newPosition.playerPosition, 1 - divideRatio),
                deltaTime);
            
            playerTransform.rotation =
                Quaternion.Lerp(playerTransform.rotation, 
                Quaternion.Lerp(lastPosition.playerRotation, newPosition.playerRotation, 1 - divideRatio),
                deltaTime);
                
            camera.transform.Value.localRotation =
                Quaternion.Lerp(camera.transform.Value.localRotation,  
                    Quaternion.Lerp(
                    Quaternion.Euler(lastPosition.cameraAngle, 0, 0), 
                    Quaternion.Euler(newPosition.cameraAngle, 0, 0), 
                    1 - divideRatio), deltaTime);
            
            _gameContext.playerEntity.isDead = newPosition.isDead;
            playerTransformInfo.Value = new TransformInfo(playerTransform.transform);
        }
    }
}