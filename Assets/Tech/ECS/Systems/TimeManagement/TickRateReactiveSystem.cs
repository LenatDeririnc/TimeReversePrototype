using System.Collections.Generic;
using Entitas;

namespace ECS.Systems.TimeManagement
{
    public class TickRateReactiveSystem : ReactiveSystem<TimeEntity>, IInitializeSystem
    {
        private readonly TimeContext _timeContext;

        public TickRateReactiveSystem(Contexts contexts) : base(contexts.time)
        {
            _timeContext = contexts.time;
        }

        protected override ICollector<TimeEntity> GetTrigger(IContext<TimeEntity> context)
        {
            return context.CreateCollector(TimeMatcher.TickCount.Added());
        }

        protected override bool Filter(TimeEntity entity)
        {
            return true;
        }

        public void Initialize()
        {
            _timeContext.SetPreviousTickCount(0);
        }

        protected override void Execute(List<TimeEntity> entities)
        {
            foreach (var e in entities)
            {
                var tickCount = _timeContext.tickCount;
                var previousTickCount = _timeContext.previousTickCount;
            
                if (previousTickCount.Value < tickCount.Value)
                {
                    _timeContext.tickCountEntity.isTickRateIncreased = true;
                }
                else if (previousTickCount.Value > tickCount.Value)
                {
                    _timeContext.tickCountEntity.isTickRateDecreased = true;
                }
                
                previousTickCount.Value = tickCount.Value;
            }
        }
    }
}