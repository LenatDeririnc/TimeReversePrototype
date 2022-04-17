using ECS.Systems;
using ECS.Systems.Characters.Enemy;
using ECS.Systems.Characters.Player;
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
            
            //Data
            _systems.Add(new ColliderDataSystem(Contexts));

            //Input
            _systems.Add(new InputControllingReactiveSystem(Contexts));
            _systems.Add(new MouseLookInputSystem(Contexts));
            _systems.Add(new GamepadLookInputSystem(Contexts));
            _systems.Add(new MovementInputSystem(Contexts));
            _systems.Add(new InputControlSenderSystem(Contexts));
            _systems.Add(new RollbackInputSystem(Contexts));

            //PlayerController
            _fixedSystems.Add(new FixedUpdatePlayerControllerSystem(Contexts));
            _lateSystems.Add(new LateUpdatePlayerControllerSystem(Contexts));
            _systems.Add(new UpdatePlayerControllerSystem(Contexts));
            _systems.Add(new CameraPitchReactiveSystem(Contexts));
            
            //Time
            _systems.Add(new TimeSpeedSystem(Contexts));
            _systems.Add(new TimeSystem(Contexts));
            _systems.Add(new TimeLineSystem(Contexts));

            //PlayerTimeline
            _systems.Add(new UndoPlayerTimelineSystem(Contexts));
            _systems.Add(new WritePlayerTimelineSystem(Contexts));
            _systems.Add(new ReplaceRewindPositionReactiveSystem(Contexts));
            _systems.Add(new RewindPlayerTimelineSystem(Contexts));

            //Enemies
            _systems.Add(new EnemyAnimationRewindSystem(Contexts));
            
            //Signals
            _systems.Add(new TriggerSignalReactiveSystem(Contexts));
            _systems.Add(new ColliderDataSignalReactiveSystem(Contexts));

            //Test
            _systems.Add(new MovingObjectSystem(Contexts));
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