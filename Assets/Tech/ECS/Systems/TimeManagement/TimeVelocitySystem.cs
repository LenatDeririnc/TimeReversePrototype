using System.Collections.Generic;
using Entitas;
using TimeSystem;

namespace ECS.Systems.TimeManagement
{
    public class TimeVelocitySystem : ReactiveSystem<InputEntity>
    {
        public TimeVelocitySystem(Contexts contexts) : base(contexts.input)
        {
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
                TimeManagerComponent.TimeManager.SetMovingObject(e.velocityConverter.Value);            
            }
        }
    }
}