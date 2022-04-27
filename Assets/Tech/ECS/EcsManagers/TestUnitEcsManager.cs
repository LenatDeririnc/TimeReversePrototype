using ECS.Systems;
using ECS.Systems.Characters.Player;
using ECS.Systems.Characters.Player.PlayerControllerSystems;
using ECS.Systems.Input;

namespace ECS.EcsManagers
{
    public class TestUnitEcsManager : BaseEcsManager
    {
        public TestUnitEcsManager(Contexts contexts) : base(contexts)
        {
            //Data
            Systems.Add(new ColliderDataSystem(Contexts));
        
            //Input
            Systems.Add(new InputSettingsInitSystem(Contexts));
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
        }
    }
}