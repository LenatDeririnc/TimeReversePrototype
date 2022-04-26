using Entitas;

namespace ECS.Systems.Signals
{
    public class EntityIdGenerateSystem : IInitializeSystem
    {
        private readonly IGroup<GameEntity> _group;

        public EntityIdGenerateSystem(Contexts contexts)
        {
            _group = contexts.game.GetGroup(GameMatcher.AddUniqueEntityIDSignal);
        }
    
        public void Initialize()
        {
            foreach (var signal in _group)
            {
                EcsManager.GameObjectEntityTools.SetEntityUniqueId(signal);
                signal.ReplaceSetFlagSignal(false, _ => signal.isAddUniqueEntityIDSignal = _);
            }
        }
    }
    
    
}