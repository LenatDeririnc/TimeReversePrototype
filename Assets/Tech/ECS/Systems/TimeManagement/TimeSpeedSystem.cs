using Entitas;
using UnityEngine;

namespace ECS.Systems.TimeManagement
{
    public class TimeSpeedSystem : IExecuteSystem, IInitializeSystem
    {
        private readonly TimeContext _timeContext;
        private readonly IGroup<TimeEntity> _timeSpeedGroup;

        public TimeSpeedSystem(Contexts contexts)
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
            var time = _timeContext.time;

            foreach (var e in _timeSpeedGroup)
            {
                var timeSpeed = e.timeSpeed.Value;
            
                if (timeSpeed > 0 && max < timeSpeed)
                    max = timeSpeed;
                
                if (timeSpeed < 0 && min > timeSpeed)
                    min = timeSpeed;
            }
            
            var resultValue = max + min; // min < 0, поэтому +
            
            if (time.Value + resultValue * Time.deltaTime < 0)
                resultValue = 0;

            globalTimeSpeed.Value = resultValue;
            
            var resultTimeScale = Mathf.Abs(_timeContext.globalTimeSpeed.Value);
            Time.timeScale = resultTimeScale;
        }
    }
}