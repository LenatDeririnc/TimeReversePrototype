using System.Collections.Generic;
using Entitas;

namespace ECS.Systems.Weapon
{
    public class BulletTriggerSystem : ReactiveSystem<SignalsEntity>
    {
        public BulletTriggerSystem(Contexts contexts) : base(contexts.signals)
        {
        }

        protected override ICollector<SignalsEntity> GetTrigger(IContext<SignalsEntity> context)
        {
            return context.CreateCollector(SignalsMatcher.TriggerEntitySignal.Added());
        }

        protected override bool Filter(SignalsEntity entity)
        {
            return entity.hasTriggerEntitySignal & entity.triggerEntitySignal.Sender.isBullet;
        }

        protected override void Execute(List<SignalsEntity> entities)
        {
            foreach (var signal in entities)
            {
                var sender = signal.triggerEntitySignal.Sender;
                var getter = signal.triggerEntitySignal.Getter;
                if (getter == null)
                {
                    sender.gameObject.Value.SetActive(false);
                    return;
                }
                getter.isDead = true;
            }
        }
    }
}