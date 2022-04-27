using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace ECS.Systems
{
    public class DestroyEntitySystem : ReactiveSystem<SignalsEntity>
    {
        public DestroyEntitySystem(Contexts contexts) : base(contexts.signals)
        {
        }

        protected override ICollector<SignalsEntity> GetTrigger(IContext<SignalsEntity> context)
        {
            return context.CreateCollector(SignalsMatcher.DestroyEntitySignal.Added());
        }

        protected override bool Filter(SignalsEntity entity)
        {
            return true;
        }

        protected override void Execute(List<SignalsEntity> entities)
        {
            foreach (var e in entities)
            {
                var entityToDestroy = e.destroyEntitySignal.EntityToDestroy;
            
                if (entityToDestroy.hasGameObject)
                    Object.Destroy(entityToDestroy.gameObject.Value);
            
                entityToDestroy.Destroy();
            }
        }
    }
}