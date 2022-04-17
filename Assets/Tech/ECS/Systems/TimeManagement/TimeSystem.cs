using System;
using ECS.Extensions;
using Entitas;
using UnityEngine;

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
            var timeSpeed = _timeContext.timeSpeed;
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
                if (previousTime.Value > time.Value)
                {
                    _timeContext.timeEntity.isTickRateDecreased = true;
                }
                else if (previousTime.Value < time.Value)
                {
                    _timeContext.timeEntity.isTickRateIncreased = true;
                }
                previousTime.Value = time.Value;
            }
        }
    }
}