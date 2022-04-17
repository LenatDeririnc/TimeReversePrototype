using Entitas;

namespace ECS.Systems.TimeManagement
{
    public class TimeSpeedSystem : IInitializeSystem, IExecuteSystem
    {
        private readonly TimeContext _timeContext;
        private readonly InputContext _inputContext;

        public TimeSpeedSystem(Contexts contexts)
        {
            _timeContext = contexts.time;
            _inputContext = contexts.input;
        }
    
        public void Initialize()
        {
            _timeContext.SetTimeSpeed(0);
        }

        public void Execute()
        {
            if (!_inputContext.hasInputControlling)
                return;
            
            var timeChanger = _inputContext.inputControllingEntity.forwardMovement.Value;
            var timeSpeed = _timeContext.timeSpeed;
            var rollbackValue = _timeContext.rollbackValue;
            var isRollback = _timeContext.isRollback;
            
            timeSpeed.Value = timeChanger.magnitude;
            
            if (isRollback)
                timeSpeed.Value = -rollbackValue.Value;
        }
    }
}