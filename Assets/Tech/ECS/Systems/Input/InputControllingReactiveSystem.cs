using System.Collections.Generic;
using Common;
using Entitas;
using UnityEngine;

namespace ECS.Systems.Input
{
    public class InputControllingReactiveSystem : ReactiveSystem<InputEntity>
    {
        public InputControllingReactiveSystem(Contexts contexts) : base(contexts.input)
        {
        }

        protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context)
        {
            return context.CreateCollector(InputMatcher.InputControlling.Added());
        }

        protected override bool Filter(InputEntity entity)
        {
            return !entity.isInput;
        }

        protected override void Execute(List<InputEntity> entities)
        {
            foreach (var e in entities)
            {
                e.isInput = true;
                e.ReplaceLook(new Vector2(0, 0));
                e.ReplaceMoveDirection(Vector3.zero);
                e.ReplaceForwardMovement(Vector3.zero);
                e.ReplaceBackMovement(Vector3.zero);
            }
        }
    }
}