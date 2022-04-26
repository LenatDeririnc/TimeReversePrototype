using Entitas;
using TimelineData;

namespace ECS.Systems.TimeManagement.Player
{
    public class PlayerTimeLineSystem : IInitializeSystem
    {
        private readonly Contexts _contexts;

        public PlayerTimeLineSystem(Contexts contexts)
        {
            _contexts = contexts;
        }
    
        public void Initialize()
        {
            var player = _contexts.game.playerEntity;
            var camera = _contexts.game.playerCameraEntity;
            
            var timelineData = new PlayerTimelineData(player.entityID.Value, -100)
            {
                playerPosition = player.transform.Value.position,
                playerRotation = player.transform.Value.rotation,
                cameraAngle = camera.cameraPitchAngle.Value,
            };
            
            _contexts.time.isTimelineLastPosition = true;
            _contexts.time.timelineLastPositionEntity.ReplacePlayerTimelineData(timelineData);
            _contexts.time.timeLineStack.Value.Push(timelineData);
        }
    }
}