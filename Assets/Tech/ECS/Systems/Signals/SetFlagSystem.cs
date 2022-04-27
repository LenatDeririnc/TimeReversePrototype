using System.Collections.Generic;
using Entitas;

namespace ECS.Systems.Signals
{
    public class SetFlagSystem : ReactiveSystem<SignalsEntity>
    {
        private readonly Contexts _contexts;

        public SetFlagSystem(Contexts contexts) : base(contexts.signals)
        {
            _contexts = contexts;
        }

        protected override ICollector<SignalsEntity> GetTrigger(IContext<SignalsEntity> context)
        {
            return context.CreateCollector(SignalsMatcher.SetFlagSignal.Added());
        }

        protected override bool Filter(SignalsEntity entity)
        {
            return entity.setFlagSignal.Delegate != null;
        }

        protected override void Execute(List<SignalsEntity> entities)
        {
            foreach (var flagSignal in entities)
            {
                flagSignal.setFlagSignal.Delegate.Invoke(flagSignal.setFlagSignal.Value);
                flagSignal.RemoveSetFlagSignal();
                if (flagSignal.GetComponents().Length <= 0)
                    flagSignal.isDestroy = true;
            }
        }
    }
}