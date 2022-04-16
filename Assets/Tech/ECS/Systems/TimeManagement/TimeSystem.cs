using ECS.Extensions;
using Entitas;

namespace ECS.Systems.TimeManagement
{
    public class TimeSystem : IInitializeSystem, IExecuteSystem
    {
        private readonly TimeContext _timeContext;
    
        public TimeSystem(Contexts contexts)
        {
            _timeContext = contexts.time;
        }
        
        public void Initialize()
        {
            _timeContext.SetTime(0);
        }

        public void Execute()
        {
            var time = _timeContext.time;
            var timeSpeed = _timeContext.timeSpeed;
            
            if (time.Value + _timeContext.ScaledTimeSpeed() < 0)
                timeSpeed.Value = 0;

            time.Value += _timeContext.ScaledTimeSpeed();
        }
    }
}