using ECS.Systems;
using ECS.Systems.Characters.Enemy;
using ECS.Systems.Characters.Player;
using ECS.Systems.Input;
using ECS.Systems.Input.PlayerControllerSystems;
using ECS.Systems.TimeManagement;
using ECS.Systems.TimeManagement.PlayerTimeline;
using ECS.Systems.Triggers;
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
            _systems.Add(new InputSettingsInitSystem(Contexts));
            _systems.Add(new GamepadSwitcherSystem(Contexts));
            _systems.Add(new LookActionSwitcherSystem(Contexts));
            _systems.Add(new InputControllingReactiveSystem(Contexts));
            _systems.Add(new LookInputSystem(Contexts));
            _systems.Add(new MovementInputSystem(Contexts));
            _systems.Add(new InputControlSenderSystem(Contexts));

            //PlayerController
            _fixedSystems.Add(new FixedUpdatePlayerControllerSystem(Contexts));
            _lateSystems.Add(new LateUpdatePlayerControllerSystem(Contexts));
            _systems.Add(new UpdatePlayerControllerSystem(Contexts));
            _systems.Add(new CameraPitchReactiveSystem(Contexts));
            
            //Time
            _systems.Add(new TimeSpeedInputSystem(Contexts));
            _systems.Add(new TimeSystem(Contexts));
            _systems.Add(new TimeLineSystem(Contexts));

            //PlayerTimeline
            _systems.Add(new PlayerMovementTimeSpeedSystem(Contexts));
            _systems.Add(new UndoPlayerTimelineSystem(Contexts));
            _systems.Add(new WritePlayerTimelineSystem(Contexts));
            _systems.Add(new RewindStartPlayerTimelineSystem(Contexts));
            _systems.Add(new RewindEndPlayerTimelineReactiveSystem(Contexts));

            //Enemies
            _systems.Add(new EnemyAnimationRewindSystem(Contexts));
            
            //Signals
            _systems.Add(new TriggerSignalReactiveSystem(Contexts));
            _systems.Add(new ColliderDataSignalReactiveSystem(Contexts));
            
            //Triggers
            _systems.Add(new BulletTriggerSystem(Contexts));

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