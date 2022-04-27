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
            var timeSpeed = 1f;
            
            var movement = _contexts.input.inputEntity.moveDirection.Value;
        
            if (!_contexts.input.inputEntity.isForcedFullMovement && _contexts.time.hasGlobalTimeSpeed)
            {
                timeSpeed = _contexts.time.globalTimeSpeed.Value;
                movement = _contexts.input.inputEntity.forwardMovement.Value;
            }
        
            var data = new InputData()
            {
                look =  _contexts.input.inputEntity.look.Value,
                direction = movement,
                crouch = false,
                jump = _contexts.input.inputEntity.isJump,
                timeSpeed = timeSpeed,
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