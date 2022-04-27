using Common;
using Entitas;
using TimelineData;
using UnityEngine;

namespace ECS.Systems.TimeManagement.Player
{
    public class UndoPlayerTimelineSystem : IExecuteSystem
    {
        private readonly Contexts _contexts;

        public UndoPlayerTimelineSystem(Contexts contexts)
        {
            _contexts = contexts;
        }

        public void Execute()
        {
            if (_contexts.time.globalTimeSpeed.Value >= 0)
                return;

            var playerEntity = _contexts.game.playerEntity;
            var cameraEntity = _contexts.game.playerCameraEntity;
            
            var playerId = playerEntity.entityID.Value;
            var playerTransform = playerEntity.transform.Value;
            var playerTransformInfo = playerEntity.transformInfo;

            var time = _contexts.time.time.Value;
            var timelineStack = _contexts.time.timeLineStack.Value;

            if (!(timelineStack.Pop(playerId, time) is PlayerTimelineData transformData))
                return;
            
            playerTransform.position = transformData.playerPosition;
            playerTransform.rotation = transformData.playerRotation;
            cameraEntity.transform.Value.localRotation = Quaternion.Euler(transformData.cameraAngle, 0, 0);
            playerEntity.isDead = transformData.isDead;
            playerTransformInfo.Value = new TransformInfo(playerTransform.transform);
        }
    }
}