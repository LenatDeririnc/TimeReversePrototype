using System.Collections.Generic;
using ECS.Tools;
using Entitas;

namespace ECS.Systems.Signals
{
    public class TriggerSignalReactiveSystem : ReactiveSystem<SignalsEntity>
    {
        private readonly Contexts _contexts;

        public TriggerSignalReactiveSystem(Contexts contexts) : base(contexts.signals)
        {
            _contexts = contexts;
        }
    
        protected override ICollector<SignalsEntity> GetTrigger(IContext<SignalsEntity> context)
        {
            return context.CreateCollector(SignalsMatcher.TriggerColliderSignal.Added());
        }

        protected override bool Filter(SignalsEntity entity)
        {
            return true;
        }

        protected override void Execute(List<SignalsEntity> entities)
        {
            foreach (var e in entities)
            {
                var entity = GameObjectEntityTools.Instance.GetEntityByCollider(e.triggerColliderSignal.Getter);
                if (entity == null)
                {
                    if (e.GetComponents().Length <= 0)
                        e.isDestroy = true;
                    continue;
                }
                _contexts.signals.CreateEntity().ReplaceTriggerEntitySignal(e.triggerColliderSignal.Sender, entity);
                if (e.GetComponents().Length <= 0)
                    e.isDestroy = true;
            }
        }
    }
}