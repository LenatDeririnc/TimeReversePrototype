using Entitas;
using UnityEngine;

namespace ECS.Systems.Input
{
    public class MovementInputSystem : IExecuteSystem
    {
        private readonly IGroup<InputEntity> _group;

        public MovementInputSystem(Contexts contexts)
        {
            _group = contexts.input.GetGroup(InputMatcher.Input);
        }

        public void Execute()
        {
            foreach (var e in _group)
            {
                var horizontal = UnityEngine.Input.GetAxisRaw("Horizontal");
                var vertical = UnityEngine.Input.GetAxisRaw("Vertical");
                e.isJump = UnityEngine.Input.GetButtonDown("Jump");

                var move = new Vector3()
                {
                    x = horizontal,
                    y = 0.0f,
                    z = vertical
                };
                
                Vector3.ClampMagnitude(move, 1);
                e.moveDirection.Value = move;
                
                var movement = e.moveDirection.Value;
                var forward = new Vector3(movement.x, movement.y, Mathf.Clamp(movement.z, 0, 1));
                e.forwardMovement.Value = forward;
                
                e.backMovement.Value = e.moveDirection.Value - e.forwardMovement.Value;
                e.velocityConverter.Value.Set(e.forwardMovement.Value);
            }
        }
    }
}