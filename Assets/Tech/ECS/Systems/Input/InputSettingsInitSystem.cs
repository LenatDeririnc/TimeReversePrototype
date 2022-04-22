using Entitas;

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
            _inputContext.SetInputSettings(new InputSettings());
            _inputContext.inputSettings.Value.Enable();
            
            _inputContext.SetMouseLookSensitivity(0.05f);
            _inputContext.SetGamepadLookSensitivity(1f);
        }
    }
}