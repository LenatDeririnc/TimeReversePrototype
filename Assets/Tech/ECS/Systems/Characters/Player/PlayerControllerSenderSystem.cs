using CharacterSystem.Player.ECM.Scripts.Fields;
using Entitas;

namespace ECS.Systems.Input
{
    public class PlayerControllerSenderSystem : IExecuteSystem
    {
        private readonly IGroup<InputEntity> _group;
        private readonly GameContext _gameContext;

        public PlayerControllerSenderSystem(Contexts contexts)
        {
            _gameContext = contexts.game;
            _group = contexts.input.GetGroup(InputMatcher.InputControlling);
        }
        
        public void Execute()
        {
            foreach (var e in _group)
            {
                if (_gameContext.playerEntity.isDead)
                {
                    e.inputControlling.Value.SendInputData(new InputData());
                    return;
                }
            
                var data = new InputData()
                {
                    look = e.look.Value,
                    direction = e.forwardMovement.Value,
                    crouch = false,
                    jump = e.isJump,
                };

                e.inputControlling.Value.SendInputData(data);
            }
        }
    }
}