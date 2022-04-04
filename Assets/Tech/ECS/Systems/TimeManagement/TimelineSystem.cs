using System.Collections.Generic;
using Common;
using Entitas;

namespace ECS.Systems.TimeManagement
{
    public class TimelineSystem : IInitializeSystem
    {
        private readonly Contexts _contexts;

        public TimelineSystem(Contexts contexts)
        {
            _contexts = contexts;
        }
        
        public void Initialize()
        {
            _contexts.time.SetTimelineData(new Stack<TimelineData>());

            var player = _contexts.game.playerEntity;
            var camera = _contexts.game.playerCameraEntity;
            
            var timelineData = new TimelineData()
            {
                playerPosition = player.transform.Value.position,
                playerRotation = player.transform.Value.rotation,
                cameraAngle = camera.cameraPitchAngle.Value,
            };
            
            _contexts.time.SetTimelineLastPosition(timelineData);

            var timeManager = _contexts.time.timeManagerHandlerEntity.timeManagerHandler;
            timeManager.Value.OnTickAdd += AddPlayerInfo;
            timeManager.Value.OnTickRemove += ExecuteLastPlayerInfo;
        }

        private void AddPlayerInfo()
        {
            var playerTransform = _contexts.game.playerEntity.transform.Value;
            var cameraPitch = _contexts.game.playerCameraEntity.cameraPitchAngle.Value;

            var saveData = new TimelineData()
            {
                playerPosition = playerTransform.position,
                playerRotation = playerTransform.rotation,
                cameraAngle = cameraPitch, 
            };
            
            _contexts.time.ReplaceTimelineLastPosition(saveData);
            _contexts.time.timelineDataEntity.timelineData.Value.Push(saveData);
        }

        private void ExecuteLastPlayerInfo()
        {
            var timelineData = _contexts.time.timelineDataEntity.timelineData.Value;
            
            if (timelineData.Count <= 0)
                return;
            
            var player = _contexts.game.playerEntity;
            var camera = _contexts.game.playerCameraEntity;
            
            var newTimelineData = new TimelineData()
            {
                playerPosition = player.transform.Value.position,
                playerRotation = player.transform.Value.rotation,
                cameraAngle = camera.cameraPitchAngle.Value,
            };
            
            _contexts.time.ReplaceTimelineLastPosition(newTimelineData);
            TimelineData transformData = timelineData.Pop();
            _contexts.time.ReplaceTimelineRewindPosition(transformData);
        }
    }
}