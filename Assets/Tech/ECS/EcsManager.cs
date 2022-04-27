using ECS.Systems;
using ECS.Systems.Characters.Enemy;
using ECS.Systems.Characters.Player;
using ECS.Systems.Characters.Player.PlayerControllerSystems;
using ECS.Systems.Features;
using ECS.Systems.Input;
using ECS.Systems.Signals;
using ECS.Systems.TimeManagement;
using ECS.Systems.TimeManagement.Bullet;
using ECS.Systems.TimeManagement.Player;
using ECS.Systems.TimeManagement.Player.Input;
using ECS.Systems.TimeManagement.Rigidbody;
using ECS.Systems.Weapon;
using ECS.Tools;
using UnityEngine;

namespace ECS
{
    public class EcsManager : MonoBehaviour
    {
        public static Contexts Contexts;
        public static GameObjectEntityTools GameObjectEntityTools;
        
        private Entitas.Systems _systems;
        private Entitas.Systems _fixedSystems;
        private Entitas.Systems _lateSystems;


        private void Awake()
        {
            Contexts = new Contexts();

            _systems = new Entitas.Systems();
            _fixedSystems = new Entitas.Systems();
            _lateSystems = new Entitas.Systems();
            
            GameObjectEntityTools = new GameObjectEntityTools(Contexts);

            //Data
            _systems.Add(new ColliderDataSystem(Contexts));
            
            //TriggerSignals
            _systems.Add(new TriggerSignalReactiveSystem(Contexts));

            //Input
            _systems.Add(new InputSettingsInitSystem(Contexts));
            _systems.Add(new GamepadSwitcherSystem(Contexts));
            _systems.Add(new LookActionSwitcherSystem(Contexts));
            _systems.Add(new LookInputSystem(Contexts));
            _systems.Add(new MovementInputSystem(Contexts));
            _systems.Add(new FireInputSystem(Contexts));

            //PlayerController
            _systems.Add(new PlayerControllerSenderSystem(Contexts));
            _systems.Add(new InitPlayerReactiveSystem(Contexts));
            _fixedSystems.Add(new FixedUpdatePlayerControllerSystem(Contexts));
            _lateSystems.Add(new LateUpdatePlayerControllerSystem(Contexts));
            _systems.Add(new UpdatePlayerControllerSystem(Contexts));
            _systems.Add(new ShootSystem(Contexts));

            //Time
            _systems.Add(new TimeSpeedSystem(Contexts));
            _systems.Add(new TimeSystem(Contexts));
            _systems.Add(new TimelineFeature(Contexts));
            
            //After time updated systems

            //Weapon
            _systems.Add(new MovingObjectSystem(Contexts));
            _systems.Add(new BulletTriggerSystem(Contexts));

            //Enemies
            _systems.Add(new EnemyAnimationRewindSystem(Contexts));

            //CleanupSignals
            _systems.Add(new TriggerColliderSignalCleanupReactiveSystem(Contexts));
            _systems.Add(new TriggerEntityCleanupSReactiveSystem(Contexts));
            _systems.Add(new DestroyGameEntitySystem(Contexts));
            _systems.Add(new DestroySignalEntitySystem(Contexts));
            _systems.Add(new SetFlagSystem(Contexts));
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