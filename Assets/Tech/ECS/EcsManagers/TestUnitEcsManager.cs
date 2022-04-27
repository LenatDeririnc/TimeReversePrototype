using ECS.Systems;
using ECS.Systems.Characters.Enemy;
using ECS.Systems.Characters.Player;
using ECS.Systems.Characters.Player.PlayerControllerSystems;
using ECS.Systems.Features;
using ECS.Systems.Input;
using ECS.Systems.Signals;
using ECS.Systems.TimeManagement;
using ECS.Systems.Weapon;

namespace ECS.EcsManagers
{
    public class TestUnitEcsManager : BaseEcsManager
    {
        public TestUnitEcsManager(Contexts contexts) : base(contexts)
        {
            //Data
            Systems.Add(new ColliderDataSystem(Contexts));
            
            //TriggerSignals
            Systems.Add(new TriggerSignalReactiveSystem(Contexts));
        
            //Input
            Systems.Add(new InputSettingsInitSystem(Contexts));
            Systems.Add(new ForceFullMovementInputSystem(Contexts));
            Systems.Add(new GamepadSwitcherSystem(Contexts));
            Systems.Add(new LookActionSwitcherSystem(Contexts));
            Systems.Add(new LookInputSystem(Contexts));
            Systems.Add(new MovementInputSystem(Contexts));
            Systems.Add(new FireInputSystem(Contexts));

            //PlayerController
            Systems.Add(new PlayerControllerSenderSystem(Contexts));
            Systems.Add(new InitPlayerReactiveSystem(Contexts));
            FixedSystems.Add(new FixedUpdatePlayerControllerSystem(Contexts));
            LateSystems.Add(new LateUpdatePlayerControllerSystem(Contexts));
            Systems.Add(new UpdatePlayerControllerSystem(Contexts));
            Systems.Add(new ShootSystem(Contexts));
            
            //Time
            Systems.Add(new RealtimeSystem(Contexts));
            Systems.Add(new TimeSpeedSystem(Contexts));
            Systems.Add(new TimeSystem(Contexts));
            
            //Weapon
            Systems.Add(new MovingObjectSystem(Contexts));
            Systems.Add(new BulletTriggerSystem(Contexts));
            
            //Animation
            Systems.Add(new AnimatorRewindSystem(Contexts));
            
            //CleanupSignals
            Systems.Add(new TriggerColliderSignalCleanupReactiveSystem(Contexts));
            Systems.Add(new TriggerEntityCleanupSReactiveSystem(Contexts));
            Systems.Add(new DestroyGameEntitySystem(Contexts));
            Systems.Add(new DestroySignalEntitySystem(Contexts));
            Systems.Add(new SetFlagSystem(Contexts));
        }
    }
}