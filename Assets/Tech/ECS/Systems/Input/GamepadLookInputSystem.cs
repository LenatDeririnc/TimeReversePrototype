using Entitas;
using UnityEngine;

namespace ECS.Systems.Input
{
    public class GamepadLookInputSystem : IExecuteSystem
    {
        private readonly IGroup<InputEntity> _group;

        public GamepadLookInputSystem(Contexts contexts)
        {
            _group = contexts.input.GetGroup(InputMatcher.Input);
        }
    
        public void Execute()
        {
            foreach (var e in _group)
            {
                var mouseX = UnityEngine.Input.GetAxis("HorizontalLook");
                var mouseY = UnityEngine.Input.GetAxis("VerticalLook");
                
                e.look.Value = new Vector2(mouseX, mouseY);
            }
        }
    }
}