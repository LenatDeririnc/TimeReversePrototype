using Entitas;
using UnityEngine;

namespace ECS.Systems.Input
{
    public class InputSettingsInitSystem : IInitializeSystem
    {
        private readonly InputContext _inputContext;

        public InputSettingsInitSystem(Contexts contexts)
        {
            _inputContext = contexts.input;
        }
    
        public void Initialize()
        {
            _inputContext.isInput = true;
        
            _inputContext.inputEntity.ReplaceInputSettings(new InputSettings());
            _inputContext.inputEntity.inputSettings.Value.Enable();
            
            _inputContext.inputEntity.ReplaceLook(new Vector2(0, 0));
            _inputContext.inputEntity.ReplaceMoveDirection(Vector3.zero);
            _inputContext.inputEntity.ReplaceForwardMovement(Vector3.zero);
            _inputContext.inputEntity.ReplaceBackMovement(Vector3.zero);
            _inputContext.inputEntity.ReplaceFireInput(0);
            _inputContext.inputEntity.ReplaceMouseLookSensitivity(0.05f);
            _inputContext.inputEntity.ReplaceGamepadLookSensitivity(0.8f);
        }
    }
}