using ECS.Systems;
using ECS.Systems.Input;
using ECS.Systems.Input.PlayerControllerSystems;
using ECS.Systems.TimeManagement;
using UnityEngine;

namespace ECS
{
    public class EcsBootstrapper : MonoBehaviour
    {
        public static Contexts Contexts;
        private Entitas.Systems _systems;
        private Entitas.Systems _fixedSystems;
        private Entitas.Systems _lateSystems;

        private void Awake()
        {
            Contexts = new Contexts();

            _systems = new Entitas.Systems();
            _fixedSystems = new Entitas.Systems();
            _lateSystems = new Entitas.Systems();
            
            //Input
            _systems.Add(new InputControllingReactiveSystem(Contexts));
            _systems.Add(new MouseLookInputSystem(Contexts));
            _systems.Add(new MovementInputSystem(Contexts));
            _systems.Add(new InputControlSenderSystem(Contexts));
            
            //PlayerController
            _fixedSystems.Add(new FixedUpdatePlayerControllerSystem(Contexts));
            _lateSystems.Add(new LateUpdatePlayerControllerSystem(Contexts));
            _systems.Add(new UpdatePlayerControllerSystem(Contexts));

            //Time
            _systems.Add(new TimeVelocityReactiveSystem(Contexts));

            //Test
            _systems.Add(new MovingObjectSystem(Contexts));
            _systems.Add(new TestSystem(Contexts));
        }

        public void Start()
        {
            _systems.Initialize();
        }

        public void LateUpdate()
        {
            _lateSystems.Execute();
        }
        
        public void FixedUpdate()
        {
            _fixedSystems.Execute();
        }

        public void Update()
        {
            _systems.Execute();
        }

        public void OnDestroy()
        {
            _systems.TearDown();
        }
    }
}