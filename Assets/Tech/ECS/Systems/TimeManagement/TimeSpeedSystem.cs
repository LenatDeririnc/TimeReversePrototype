using ECS.Extensions;
using Entitas;

namespace ECS.Systems.TimeManagement
{
    public class TimeSpeedSystem : IInitializeSystem, IExecuteSystem
    {
        private readonly TimeContext _timeContext;

        public TimeSpeedSystem(Contexts contexts)
        {
            _timeContext = contexts.time;
        }
    
        public void Initialize()
        {
            _timeContext.SetTimeSpeed(0);
        }

        public void Execute()
        {
            if (!_timeContext.hasTimeChanger)
                return;
            
            var timeChanger = _timeContext.timeChanger;
            var timeSpeed = _timeContext.timeSpeed;
            var rollbackValue = _timeContext.rollbackValue;
            var isRollback = _timeContext.isRollback;
                
            timeSpeed.Value = timeChanger.Value.Velocity().magnitude;
            
            if (isRollback)
                timeSpeed.Value = -rollbackValue.Value;
        }
    }
}