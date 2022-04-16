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
            var mouseX = UnityEngine.Input.GetAxis("HorizontalLook");
            var mouseY = UnityEngine.Input.GetAxis("VerticalLook");
            
            var lookValue = new Vector2(mouseX, mouseY);
            
            if (lookValue.magnitude < 0.1f)
                return;
        
            foreach (var e in _group)
            {
                e.look.Value = lookValue;
            }
        }
    }
}