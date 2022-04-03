using Entitas;

namespace ECS.Systems
{
    public class TestSystem : IInitializeSystem
    {
        private readonly Contexts _contexts;

        public TestSystem(Contexts contexts)
        {
            _contexts = contexts;
        }
        
        public void Initialize()
        {
            _contexts.game.CreateEntity();
        }
    }
}