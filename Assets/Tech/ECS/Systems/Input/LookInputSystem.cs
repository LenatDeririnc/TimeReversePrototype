using Entitas;
using UnityEngine;

namespace ECS.Systems.Input
{
    public class LookInputSystem : IExecuteSystem
    {
        private readonly IGroup<InputEntity> _group;

        public LookInputSystem(Contexts contexts)
        {
            _group = contexts.input.GetGroup(InputMatcher.Input);
        }

        public void Execute()
        {
            foreach (var e in _group)
            {
                var mouseX = Mathf.Clamp(UnityEngine.Input.GetAxis("Mouse X") + UnityEngine.Input.GetAxis("HorizontalLook") * Time.deltaTime * 100, -1, 1);
                var mouseY = Mathf.Clamp(UnityEngine.Input.GetAxis("Mouse Y") + UnityEngine.Input.GetAxis("VerticalLook") * Time.deltaTime * 100, -1, 1);

                e.look.Value = new Vector2(mouseX, mouseY);
            }
        }
    }
}