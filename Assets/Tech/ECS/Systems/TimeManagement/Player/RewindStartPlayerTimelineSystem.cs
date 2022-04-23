using Common;
using Entitas;
using UnityEngine;

namespace ECS.Systems.TimeManagement.Player
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
            
            if (!_timeContext.isTimelineRewindPosition)
                return;

            var playerTransform = _gameContext.playerEntity.transform.Value;
            var playerTransformInfo = _gameContext.playerEntity.transformInfo;
            var camera = _gameContext.playerCameraEntity;

            var newData = _timeContext.timelineRewindPositionEntity.playerTimelineData.Value;
            var lastData = _timeContext.timelineLastPositionEntity.playerTimelineData.Value;
            var divideRatio = (_timeContext.time.Value - newData.pushTime) / (lastData.pushTime - newData.pushTime);
            
            var deltaTime = Time.deltaTime * _timeContext.smoothRewindSpeed.value;

            playerTransform.position =
                Vector3.Lerp(playerTransform.position,  
                Vector3.Lerp(lastData.playerPosition, newData.playerPosition, 1 - divideRatio),
                deltaTime);
            
            playerTransform.rotation =
                Quaternion.Lerp(playerTransform.rotation, 
                Quaternion.Lerp(lastData.playerRotation, newData.playerRotation, 1 - divideRatio),
                deltaTime);
                
            camera.transform.Value.localRotation =
                Quaternion.Lerp(camera.transform.Value.localRotation,  
                    Quaternion.Lerp(
                    Quaternion.Euler(lastData.cameraAngle, 0, 0), 
                    Quaternion.Euler(newData.cameraAngle, 0, 0), 
                    1 - divideRatio), deltaTime);
            
            _gameContext.playerEntity.isDead = newData.isDead;
            playerTransformInfo.Value = new TransformInfo(playerTransform.transform);
        }
    }
}