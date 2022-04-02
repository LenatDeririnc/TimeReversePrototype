using Entitas;

namespace ECS.Systems.Input
{
    public class InputSystem : IInitializeSystem
    {
        private readonly Contexts _contexts;

        public InputSystem(Contexts contexts)
        {
            _contexts = contexts;
        }
        
        public void Initialize()
        {
            var e = _contexts.input.CreateEntity();
            e.isInput = true;
        }
    }
}