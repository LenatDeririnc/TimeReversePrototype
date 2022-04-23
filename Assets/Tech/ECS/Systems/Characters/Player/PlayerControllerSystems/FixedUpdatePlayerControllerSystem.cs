using Entitas;

namespace ECS.Systems.Characters.Player.PlayerControllerSystems
{
    public class FixedUpdatePlayerControllerSystem : IExecuteSystem
    {
        private readonly IGroup<InputEntity> _group;

        public FixedUpdatePlayerControllerSystem(Contexts contexts)
        {
            _group = contexts.input.GetGroup(InputMatcher.BasePlayerControllerHolder);
        }
        
        public void Execute()
        {
            foreach (var e in _group)
            {
                e.basePlayerControllerHolder.Value.FixedUpdate();
            }
        }
    }
}