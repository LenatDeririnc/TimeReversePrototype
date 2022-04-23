using Entitas;

namespace ECS.Systems.TimeManagement.Player
{
    public class PlayerFireTimeSpeedSystem : IExecuteSystem
    {
        private readonly Contexts _contexts;

        public PlayerFireTimeSpeedSystem(Contexts contexts)
        {
            _contexts = contexts;
        }
    
        public void Execute()
        {
            if (!_contexts.game.hasPlayerModel)
                return;
                
            var model = _contexts.game.playerModel;

            var resultValue = _contexts.input.inputEntity.fireInput.Value;
            
            if (model.Value.playerEntity.isDead)
                resultValue = 0;
            
            model.Value.timeFireEntity.timeSpeed.Value = resultValue;
        }
    }
}