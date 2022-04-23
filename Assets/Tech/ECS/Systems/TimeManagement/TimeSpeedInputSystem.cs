using Entitas;
using UnityEngine;

namespace ECS.Systems.TimeManagement
{
    public class TimeSpeedInputSystem : IExecuteSystem, IInitializeSystem
    {
        private readonly TimeContext _timeContext;
        private readonly IGroup<TimeEntity> _timeSpeedGroup;

        public TimeSpeedInputSystem(Contexts contexts)
        {
            _timeContext = contexts.time;
            _timeSpeedGroup = contexts.time.GetGroup(TimeMatcher.TimeSpeed);
        }

        public void Initialize()
        {
            _timeContext.SetGlobalTimeSpeed(0);
        }

        public void Execute()
        {
            if (_timeSpeedGroup.count <= 0)
                return;
                
            float min = 0;
            float max = 0;
            
            var globalTimeSpeed = _timeContext.globalTimeSpeed;

            foreach (var e in _timeSpeedGroup)
            {
                var timeSpeed = e.timeSpeed.Value;
            
                if (timeSpeed > 0 && max < timeSpeed)
                    max = timeSpeed;
                
                if (timeSpeed < 0 && min > timeSpeed)
                    min = timeSpeed;
            }

            globalTimeSpeed.Value = max + min; // min < 0, поэтому +
            _timeContext.isRollback = globalTimeSpeed.Value < 0;
        }
    }
}