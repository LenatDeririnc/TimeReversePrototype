using System.Collections.Generic;
using Entitas;

namespace ECS.Systems.TimeManagement
{
    public class ReplaceRewindPositionReactiveSystem : ReactiveSystem<TimeEntity>
    {
        private readonly Contexts _contexts;

        public ReplaceRewindPositionReactiveSystem(Contexts contexts) : base(contexts.time)
        {
            _contexts = contexts;
        }
        
        protected override ICollector<TimeEntity> GetTrigger(IContext<TimeEntity> context)
        {
            return context.CreateCollector(TimeMatcher.Rollback.Removed());
        }

        protected override bool Filter(TimeEntity entity)
        {
            return true;
        }

        protected override void Execute(List<TimeEntity> entities)
        {
            _contexts.time.RemoveRewindPosition();
        }
    }
}