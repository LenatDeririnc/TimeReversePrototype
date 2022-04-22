using Common;
using Entitas;
using UnityEngine;

namespace ECS.Systems.Input
{
    public class LookInputSystem : IExecuteSystem
    {
        private readonly IGroup<InputEntity> _group;
        private readonly InputContext _inputContext;

        public LookInputSystem(Contexts contexts)
        {
            _inputContext = contexts.input;
            _group = contexts.input.GetGroup(InputMatcher.Input);
        }

        public void Execute()
        {
            var lookValue = _inputContext.inputActionLook.Value;
            
            var rawValue = lookValue.ReadValue<Vector2>();
            
            var resultValue = 
                _inputContext.isGamepad ? 
                rawValue * _inputContext.gamepadLookSensitivity.Value: 
                rawValue * _inputContext.mouseLookSensitivity.Value;

            foreach (var e in _group)
            {
                e.look.Value = resultValue;
            }
        }
    }
}