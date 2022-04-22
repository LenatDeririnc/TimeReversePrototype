using Entitas;

namespace ECS.Systems.Input
{
    public class GamepadSwitcherSystem : IExecuteSystem
    {
        private readonly InputContext _inputContext;

        public GamepadSwitcherSystem(Contexts contexts)
        {
            _inputContext = contexts.input;
        }
    
        public void Execute()
        {
            var gamepadTriggered = _inputContext.inputSettings.Value.Gamepad.Look.triggered;
            var keyboardTriggered = _inputContext.inputSettings.Value.KeyboardMouse.Look.triggered;
            
            if (!gamepadTriggered && !keyboardTriggered)
                return;
                
            var gamepadEnabled = gamepadTriggered && !keyboardTriggered;

            if (gamepadEnabled != _inputContext.isGamepad)
                _inputContext.isGamepad = gamepadEnabled;
        }
    }
}