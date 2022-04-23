using Entitas;

namespace ECS.Systems.TimeManagement.PlayerTimeline
{
    public class PlayerMovementTimeSpeedSystem : IExecuteSystem
    {
        private readonly Contexts _contexts;

        public PlayerMovementTimeSpeedSystem(Contexts contexts)
        {
            _contexts = contexts;
        }
    
        public void Execute()
        {
            if (!_contexts.game.hasPlayerModel)
                return;
                
            var model = _contexts.game.playerModel;
        
            var forwardMovementValue = model.Value.inputEntity.forwardMovement.Value.magnitude;
            var backMovementValue = model.Value.inputEntity.backMovement.Value.magnitude;

            var resultValue = forwardMovementValue - backMovementValue;
            
            if (model.Value.playerEntity.isDead)
                resultValue = -backMovementValue;
            
            model.Value.timeEntity.timeSpeed.Value = resultValue;
        }
    }
}