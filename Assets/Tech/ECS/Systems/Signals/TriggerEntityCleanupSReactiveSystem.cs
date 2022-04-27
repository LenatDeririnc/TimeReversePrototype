using System.Collections.Generic;
using Entitas;

namespace ECS.Systems.Signals
{
    public class TriggerEntityCleanupSReactiveSystem : ReactiveSystem<SignalsEntity>
    {
        private readonly Contexts _contexts;

        public TriggerEntityCleanupSReactiveSystem(Contexts contexts) : base(contexts.signals)
        {
            _contexts = contexts;
        }
    
        protected override ICollector<SignalsEntity> GetTrigger(IContext<SignalsEntity> context)
        {
            return context.CreateCollector(SignalsMatcher.TriggerEntitySignal.Added());
        }

        protected override bool Filter(SignalsEntity entity)
        {
            return true;
        }

        protected override void Execute(List<SignalsEntity> entities)
        {
            foreach (var e in entities)
            {
                e.RemoveTriggerEntitySignal();
            }
        }
    }
}