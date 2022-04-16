using CharacterSystem.Player.ECM.Scripts.Fields;
using Entitas;

namespace ECS.Systems.Input
{
    public class InputControlSenderSystem : IExecuteSystem
    {
        private readonly IGroup<InputEntity> _group;

        public InputControlSenderSystem(Contexts contexts)
        {
            _group = contexts.input.GetGroup(InputMatcher.InputControlling);
        }
        
        public void Execute()
        {
            foreach (var e in _group)
            {
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