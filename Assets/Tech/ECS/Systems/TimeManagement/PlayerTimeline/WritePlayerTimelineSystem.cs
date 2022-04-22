using Entitas;
using TimelineData;

namespace ECS.Systems.TimeManagement
{
    public class WritePlayerTimelineSystem : IExecuteSystem
    {
        private readonly Contexts _contexts;

        public WritePlayerTimelineSystem(Contexts contexts)
        {
            _contexts = contexts;
        }

        public void Execute()
        {
            if (!_contexts.time.timeEntity.isTickRateIncreased)
                return;
            
            var time = _contexts.time.time;
            var playerTransform = _contexts.game.playerEntity.transform.Value;
            var isDead = _contexts.game.playerEntity.isDead;
            var cameraPitch = _contexts.game.playerCameraEntity.cameraPitchAngle.Value;

            var saveData = new PlayerTimelineData(time.Value)
            {
                isDead = isDead,
                playerPosition = playerTransform.position,
                playerRotation = playerTransform.rotation,
                cameraAngle = cameraPitch, 
            };
            
            _contexts.time.isTimelineLastPosition = true;
            _contexts.time.timelineLastPositionEntity.ReplacePlayerTimelineData(saveData);
            _contexts.time.timeLineStack.Value.Push(saveData);
        }
    }
}