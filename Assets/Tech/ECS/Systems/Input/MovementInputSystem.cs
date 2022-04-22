using Common;
using Entitas;
using UnityEngine;

namespace ECS.Systems.Input
{
    public class MovementInputSystem : IExecuteSystem
    {
        private readonly IGroup<InputEntity> _group;
        private readonly InputContext _inputContext;
        public MovementInputSystem(Contexts contexts)
        {
            _inputContext = contexts.input;
            _group = contexts.input.GetGroup(InputMatcher.Input);
        }

        public void Execute()
        {
            foreach (var e in _group)
            {
                var movementValue = _inputContext.inputSettings.Value.Game.Movement.ReadValue<Vector2>();
                e.isJump = _inputContext.inputSettings.Value.Game.Jump.ReadValue<bool>();
            
                var move = new Vector3()
                {
                    x = movementValue.x,
                    y = 0.0f,
                    z = movementValue.y
                };
                
                Vector3.ClampMagnitude(move, 1);
                e.moveDirection.Value = move;
                
                var movement = e.moveDirection.Value;
                var forward = new Vector3(movement.x, movement.y, Mathf.Clamp(movement.z, 0, 1));
                e.forwardMovement.Value = forward;
                
                e.backMovement.Value = e.moveDirection.Value - e.forwardMovement.Value;
            }
        }
    }
}