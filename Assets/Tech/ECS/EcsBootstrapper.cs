using ECS.Systems;
using ECS.Systems.Input;
using ECS.Systems.TimeManagement;
using TMPro;

namespace ECS
{
    public class EcsBootstrapper
    {
        private Contexts _contexts;
        private Entitas.Systems _systems;

        public EcsBootstrapper()
        {
            _contexts = Contexts.sharedInstance;

            _systems = new Entitas.Systems();
            
            //Input
            _systems.Add(new InputControllingReactiveSystem(_contexts));
            _systems.Add(new LookInputSystem(_contexts));
            _systems.Add(new MovementInputSystem(_contexts));
            _systems.Add(new InputControlSenderSystem(_contexts));
            
            //Time
            _systems.Add(new TimeVelocitySystem(_contexts));

            _systems.Add(new MovingObjectSystem(_contexts));
        }

        public void Initialize()
        {
            _systems.Initialize();
        }

        public void Execute()
        {
            _systems.Execute();
        }

        public void TearDown()
        {
            _systems.TearDown();
        }
    }
}