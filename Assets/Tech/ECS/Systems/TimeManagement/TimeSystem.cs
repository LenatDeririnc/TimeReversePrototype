using Entitas;
using TimeSystem;

namespace ECS.Systems.TimeManagement
{
    public class TimeSystem : IInitializeSystem, IExecuteSystem
    {
        private readonly Contexts _contexts;
    
        public TimeSystem(Contexts contexts)
        {
            _contexts = contexts;
        }
        
        public void Initialize()
        {
            var handler = new TimeManager();
            _contexts.time.SetTimeManagerHandler(handler);
        }

        public void Execute()
        {
            _contexts.time.timeManagerHandlerEntity.timeManagerHandler.Value.Update();
        }
    }
}