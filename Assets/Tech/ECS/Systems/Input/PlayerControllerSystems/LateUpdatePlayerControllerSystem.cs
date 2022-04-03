using Entitas;

namespace ECS.Systems.Input.PlayerControllerSystems
{
    public class LateUpdatePlayerControllerSystem : IExecuteSystem
    {
        private readonly IGroup<InputEntity> _group;

        public LateUpdatePlayerControllerSystem(Contexts contexts)
        {
            _group = contexts.input.GetGroup(InputMatcher.BasePlayerControllerHolder);
        }
        
        public void Execute()
        {
            foreach (var e in _group)
            {
                e.basePlayerControllerHolder.Value.LateUpdate();
            }
        }
    }
}