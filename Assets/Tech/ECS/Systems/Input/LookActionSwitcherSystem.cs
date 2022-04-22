using System.Collections.Generic;
using Entitas;

namespace ECS.Systems.Input
{
    public class LookActionSwitcherSystem : ReactiveSystem<InputEntity>, IInitializeSystem
    {
        private readonly InputContext _inputContext;

        public LookActionSwitcherSystem(Contexts contexts) : base(contexts.input)
        {
            _inputContext = contexts.input;
        }

        protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context)
        {
            return context.CreateCollector(InputMatcher.Gamepad.AddedOrRemoved());
        }

        protected override bool Filter(InputEntity entity)
        {
            return true;
        }

        public void Initialize()
        {
            _inputContext.isGamepad = false;
            SwitchDevice();
        }

        protected override void Execute(List<InputEntity> entities)
        {
            SwitchDevice();
        }

        private void SwitchDevice()
        {
            var currentDevice = 
                _inputContext.isGamepad ? 
                _inputContext.inputSettings.Value.Gamepad.Look : 
                _inputContext.inputSettings.Value.KeyboardMouse.Look;
                
            _inputContext.ReplaceInputActionLook(currentDevice);
        }
    }
}