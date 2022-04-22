using System;
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
            _timeContext.SetPreviousTime(0);
            _timeContext.SetTickRate(0.001f);
        }

        public void Execute()
        {
            var time = _timeContext.time;
            var previousTime = _timeContext.previousTime;
            var timeSpeed = _timeContext.globalTimeSpeed;
            var tickRate = _timeContext.tickRate;
            
            if (time.Value + _timeContext.ScaledTimeSpeed() < 0)
                timeSpeed.Value = 0;

            time.Value += _timeContext.ScaledTimeSpeed();

            if (Math.Abs(previousTime.Value - time.Value) < tickRate.Value)
            {
                _timeContext.timeEntity.isTickRateIncreased = false;
                _timeContext.timeEntity.isTickRateDecreased = false;
            }
            else
            {
                _timeContext.timeEntity.isTickRateIncreased = previousTime.Value < time.Value;
                _timeContext.timeEntity.isTickRateDecreased = previousTime.Value > time.Value;
                previousTime.Value = time.Value;
            }
        }
    }
}