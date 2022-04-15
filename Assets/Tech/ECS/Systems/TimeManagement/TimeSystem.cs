using Entitas;

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
            _contexts.time.SetTimeManagerHandler(new TimeManager(_contexts));
        }

        public void Execute()
        {
            _contexts.time.timeManagerHandlerEntity.timeManagerHandler.Value.Update();
        }
    }
}