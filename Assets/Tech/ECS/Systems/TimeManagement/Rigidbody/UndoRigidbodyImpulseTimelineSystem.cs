using Entitas;
using TimelineData;

namespace ECS.Systems.TimeManagement.Rigidbody
{
    public class UndoRigidbodyImpulseTimelineSystem : IExecuteSystem
    {
        private readonly Contexts _contexts;
        private readonly IGroup<GameEntity> _group;
        
        public UndoRigidbodyImpulseTimelineSystem(Contexts contexts)
        {
            _contexts = contexts;
            _group = _contexts.game.GetGroup(GameMatcher.DynamicRigidbody);
        }

        public void Execute()
        {
            if (_contexts.time.globalTimeSpeed.Value > 0)
                return;

            var time = _contexts.time.time.Value;
            var timelineStack = _contexts.time.timeLineStack.Value;
            
            foreach (var entity in _group)
            {
                if (!(timelineStack.Pop(entity.entityID.Value, time) is RigidbodyTimelineData element))
                {
                    entity.gameObject.Value.SetActive(false);
                    continue;
                }
            
                entity.dynamicRigidbody.Value.isKinematic = true;
                entity.dynamicRigidbody.Value.velocity = element.Velocity;
                entity.transform.Value.position = element.Position;
                entity.transform.Value.rotation = element.Rotation;
            }
        }
    }
}