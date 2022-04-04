using System.Collections.Generic;
using Entitas;
using TimeSystem;

namespace ECS.Systems.TimeManagement
{
    public class TimeVelocityReactiveSystem : ReactiveSystem<InputEntity>
    {
        private readonly Contexts _contexts;

        public TimeVelocityReactiveSystem(Contexts contexts) : base(contexts.input)
        {
            _contexts = contexts;
        }

        protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context)
        {
            return context.CreateCollector(InputMatcher.VelocityConverter.Added());
        }

        protected override bool Filter(InputEntity entity)
        {
            return true;
        }

        protected override void Execute(List<InputEntity> entities)
        {
            foreach (var e in entities)
            {
                _contexts.time.timeManagerHandlerEntity.timeManagerHandler.Value.SetMovingObject(e.velocityConverter.Value);
                TimeManagerComponent.TimeManager.SetMovingObject(e.velocityConverter.Value);            
            }
        }
    }
}