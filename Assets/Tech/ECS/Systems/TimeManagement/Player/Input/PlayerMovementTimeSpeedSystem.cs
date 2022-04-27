using Entitas;

namespace ECS.Systems.TimeManagement.Player.Input
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
        
            var forwardMovementValue = _contexts.input.inputEntity.forwardMovement.Value.magnitude;
            var backMovementValue = _contexts.input.inputEntity.backMovement.Value.magnitude;

            var resultValue = forwardMovementValue - backMovementValue;
            
            if (model.Value.playerEntity.isDead)
                resultValue = -backMovementValue;
            
            model.Value.timeMovementEntity.timeSpeed.Value = resultValue;
        }
    }
}