using Entitas;
using TimelineData;

namespace ECS.Systems.TimeManagement
{
    public class UndoPlayerTimelineSystem : IExecuteSystem
    {
        private readonly Contexts _contexts;

        public UndoPlayerTimelineSystem(Contexts contexts)
        {
            _contexts = contexts;
        }

        public void Execute()
        {
            if (!_contexts.time.timeEntity.isTickRateDecreased)
                return;
        
            var time = _contexts.time.time.Value;
            var timelineData = _contexts.time.timeLineStack.Value;

            if (!(timelineData.Peek() is PlayerTimelineData lastElement))
                return;

            _contexts.time.isTimelineLastPosition = true;
            _contexts.time.timelineLastPositionEntity.ReplacePlayerTimelineData(lastElement);
            var transformData = timelineData.Pop(time) as PlayerTimelineData;
            
            _contexts.time.isTimelineRewindPosition = true;
            _contexts.time.timelineRewindPositionEntity.ReplacePlayerTimelineData(transformData);
        }
    }
}