using Entitas;

namespace ECS.Systems.Input
{
    public class RollbackInputSystem : IExecuteSystem, IInitializeSystem
    {
        private readonly IGroup<InputEntity> _group;
        private readonly Contexts _contexts;

        public RollbackInputSystem(Contexts contexts)
        {
            _contexts = contexts;
            _group = contexts.input.GetGroup(InputMatcher.Input);
        }

        public void Initialize()
        {
            _contexts.time.SetRollbackValue(0);
        }

        public void Execute()
        {
            foreach (var e in _group)
            {
                _contexts.time.isRollback = e.backMovement.Value.magnitude > 0;
                _contexts.time.rollbackValue.Value = e.backMovement.Value.magnitude;
            }
        }
    }
}