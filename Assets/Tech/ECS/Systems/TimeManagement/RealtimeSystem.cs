using Entitas;

namespace ECS.Systems.TimeManagement
{
    public class RealtimeSystem : IInitializeSystem
    {
        private readonly TimeContext _timeContext;

        public RealtimeSystem(Contexts contexts)
        {
            _timeContext = contexts.time;
        }
    
        public void Initialize()
        {
            _timeContext.CreateEntity().ReplaceTimeSpeed(1);
        }
    }
}