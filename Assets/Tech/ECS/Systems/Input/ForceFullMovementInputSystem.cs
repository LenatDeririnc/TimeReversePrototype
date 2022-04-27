using Entitas;

namespace ECS.Systems.Input
{
    public class ForceFullMovementInputSystem : IInitializeSystem
    {
        private readonly InputContext _inputContext;

        public ForceFullMovementInputSystem(Contexts contexts)
        {
            _inputContext = contexts.input;
        }
    
        public void Initialize()
        {
            _inputContext.inputEntity.isForcedFullMovement = true;
        }
    }
}