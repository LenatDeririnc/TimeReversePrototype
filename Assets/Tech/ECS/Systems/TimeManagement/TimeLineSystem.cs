using System.Collections.Generic;
using Common;
using Entitas;

namespace ECS.Systems.TimeManagement
{
    public class TimeLineSystem : IInitializeSystem
    {
        private readonly Contexts _contexts;

        public TimeLineSystem(Contexts contexts)
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
        }
    }
}