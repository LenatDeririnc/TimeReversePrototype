using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace ECS.Systems
{
    public class DestroySignalEntitySystem : ReactiveSystem<SignalsEntity>
    {
        public DestroySignalEntitySystem(Contexts contexts) : base(contexts.signals)
        {
        }

        protected override ICollector<SignalsEntity> GetTrigger(IContext<SignalsEntity> context)
        {
            return context.CreateCollector(SignalsMatcher.Destroy.Added());
        }

        protected override bool Filter(SignalsEntity entity)
        {
            return true;
        }

        protected override void Execute(List<SignalsEntity> entities)
        {
            foreach (var e in entities)
            {
                e.Destroy();
            }
        }
    }
}