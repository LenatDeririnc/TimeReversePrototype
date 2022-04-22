using Entitas;
using TimelineData;
using Tools.TimeLineStackTool;

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
            _contexts.time.SetTimeLineStack(new TimeLineStack());

            var player = _contexts.game.playerEntity;
            var camera = _contexts.game.playerCameraEntity;
            
            var timelineData = new PlayerTimelineData(0)
            {
                playerPosition = player.transform.Value.position,
                playerRotation = player.transform.Value.rotation,
                cameraAngle = camera.cameraPitchAngle.Value,
            };
            
            _contexts.time.isTimelineLastPosition = true;
            _contexts.time.timelineLastPositionEntity.ReplacePlayerTimelineData(timelineData);
        }
    }
}