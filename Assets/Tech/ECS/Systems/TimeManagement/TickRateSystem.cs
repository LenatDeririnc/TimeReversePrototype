using Entitas;

namespace ECS.Systems.TimeManagement
{
    public class TickRateSystem : IInitializeSystem, IExecuteSystem
    {
        private readonly TimeContext _timeContext;

        public TickRateSystem(Contexts contexts)
        {
            _timeContext = contexts.time;
        }

        public void Initialize()
        {
            _timeContext.SetTickRate(0.1f);
            _timeContext.SetTickCount(0);
        }

        public void Execute()
        {
            var time = _timeContext.time;
            var tickRate = _timeContext.tickRate;
            var tickCount = _timeContext.tickCount;
            
            var tickRateCount = (int) (time.Value / tickRate.Value);
            
            if (tickRateCount != tickCount.Value)
            {
                _timeContext.ReplaceTickCount(tickRateCount);
            }
        }
    }
}