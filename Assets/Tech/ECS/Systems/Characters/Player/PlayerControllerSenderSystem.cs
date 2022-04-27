using CharacterSystem.Player.ECM.Scripts.Fields;
using Entitas;

namespace ECS.Systems.Characters.Player
{
    public class PlayerControllerSenderSystem : IExecuteSystem
    {
        private readonly IGroup<InputEntity> _group;
        private readonly Contexts _contexts;

        public PlayerControllerSenderSystem(Contexts contexts)
        {
            _contexts = contexts;
            _group = contexts.input.GetGroup(InputMatcher.InputControlling);
        }
        
        public void Execute()
        {
            var data = new InputData()
            {
                look =  _contexts.input.inputEntity.look.Value,
                direction = _contexts.input.inputEntity.forwardMovement.Value,
                crouch = false,
                jump = _contexts.input.inputEntity.isJump,
            };
        
            foreach (var e in _group)
            {
                if (_contexts.game.playerEntity.isDead)
                {
                    e.inputControlling.Value.SendInputData(new InputData());
                    return;
                }

                e.inputControlling.Value.SendInputData(data);
            }
        }
    }
}