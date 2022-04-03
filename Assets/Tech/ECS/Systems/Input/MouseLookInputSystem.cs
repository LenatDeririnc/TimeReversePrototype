using Entitas;
using UnityEngine;

namespace ECS.Systems.Input
{
    public class MouseLookInputSystem : IExecuteSystem
    {
        private readonly IGroup<InputEntity> _group;

        public MouseLookInputSystem(Contexts contexts)
        {
            _group = contexts.input.GetGroup(InputMatcher.Input);
        }

        public void Execute()
        {
            foreach (var e in _group)
            {
                //TODO: решить вопрос по поводу управления с геймпада 
                // var mouseX = Mathf.Clamp(UnityEngine.Input.GetAxis("Mouse X") + UnityEngine.Input.GetAxis("HorizontalLook") * Time.deltaTime * 100, -1, 1);
                // var mouseY = Mathf.Clamp(UnityEngine.Input.GetAxis("Mouse Y") + UnityEngine.Input.GetAxis("VerticalLook") * Time.deltaTime * 100, -1, 1);
                
                var mouseX = UnityEngine.Input.GetAxis("Mouse X");
                var mouseY = UnityEngine.Input.GetAxis("Mouse Y");

                e.look.Value = new Vector2(mouseX, mouseY);
            }
        }
    }
}