using Entitas;
using TimelineData;

namespace ECS.Systems.TimeManagement.Rigidbody
{
    public class WriteRigidbodyImpulseTimelineSystem : IExecuteSystem, IInitializeSystem
    {
        private readonly Contexts _contexts;
        private readonly IGroup<GameEntity> _group;

        public WriteRigidbodyImpulseTimelineSystem(Contexts contexts)
        {
            _contexts = contexts;
            _group = _contexts.game.GetGroup(GameMatcher.DynamicRigidbody);
        }
    
        public void Execute()
        {
            if (_contexts.time.globalTimeSpeed.Value <= 0)
                return;
                
            var time = _contexts.time.time.Value;
            var timelineStack = _contexts.time.timeLineStack.Value;
        
            foreach (var entity in _group)
            {
                entity.dynamicRigidbody.Value.isKinematic = false;
                var saveData = new RigidbodyTimelineData(entity.entityID.Value, time)
                {
                    Position = entity.transform.Value.position,
                    Rotation = entity.transform.Value.rotation,
                    Velocity = entity.dynamicRigidbody.Value.velocity,
                };
                timelineStack.Push(saveData);
            }
        }

        public void Initialize()
        {
            var timelineStack = _contexts.time.timeLineStack.Value;
        
            foreach (var entity in _group)
            {
                var saveData = new RigidbodyTimelineData(entity.entityID.Value, -100)
                {
                    Position = entity.transform.Value.position,
                    Rotation = entity.transform.Value.rotation,
                    Velocity = entity.dynamicRigidbody.Value.velocity,
                };
                timelineStack.Push(saveData);
            }
        }
    }
}