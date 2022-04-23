using Entitas;

namespace ECS.Systems.Input
{
    public class FireInputSystem : IExecuteSystem
    {
        private readonly IGroup<InputEntity> _group;
        private readonly InputContext _inputContext;
        
        public FireInputSystem(Contexts contexts)
        {
            _inputContext = contexts.input;
            _group = contexts.input.GetGroup(InputMatcher.Input);
        }
    
        public void Execute()
        {
            var fireValue = _inputContext.inputSettings.Value.Game.Fire.ReadValue<float>();
        
            foreach (var e in _group)
            {
                e.fireInput.Value = fireValue;
            }
        }
    }
}