using Entitas;
using Tools.TimeLineStackTool;
using UnityEngine;

namespace ECS.Systems.TimeManagement
{
    public class TimeSystem : IInitializeSystem, IExecuteSystem
    {
        private readonly Contexts _contexts;
        private readonly TimeContext _timeContext;

        public TimeSystem(Contexts contexts)
        {
            _contexts = contexts;
            _timeContext = contexts.time;
        }
        
        public void Initialize()
        {
            _timeContext.SetTimeLineStack(new TimeLineStack(_contexts));
            _timeContext.SetTime(0);
            _timeContext.SetPreviousTime(0);
            _timeContext.SetTickRate(0.001f);
        }

        public void Execute()
        {
            var time = _timeContext.time;

            var resultTime = time.Value + _timeContext.globalTimeSpeed.Value * Time.deltaTime;

            if (resultTime < 0)
            {
                resultTime = 0;
            }

            time.Value = resultTime;
        }
    }
}