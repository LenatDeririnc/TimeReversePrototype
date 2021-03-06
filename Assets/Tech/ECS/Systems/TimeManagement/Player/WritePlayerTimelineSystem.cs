using Entitas;
using TimelineData;

namespace ECS.Systems.TimeManagement.Player
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
            if (_contexts.time.globalTimeSpeed.Value <= 0)
                return;
            
            var player = _contexts.game.playerEntity;
            var time = _contexts.time.time;
            var playerTransform = _contexts.game.playerEntity.transform.Value;
            var isDead = _contexts.game.playerEntity.isDead;
            var cameraPitch = _contexts.game.playerCameraEntity.cameraPitchAngle.Value;

            var saveData = new PlayerTimelineData(player.entityID.Value, time.Value)
            {
                isDead = isDead,
                playerPosition = playerTransform.position,
                playerRotation = playerTransform.rotation,
                cameraAngle = cameraPitch,
                playerRigidbodyImpulse = player.playerRigidBody.Value.velocity,
            };
            
            _contexts.time.timeLineStack.Value.Push(saveData);
        }
    }
}