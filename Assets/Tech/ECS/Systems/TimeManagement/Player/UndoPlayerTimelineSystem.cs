using Entitas;
using TimelineData;

namespace ECS.Systems.TimeManagement.Player
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
        
            var player = _contexts.game.playerEntity.entityID.Value;
            var time = _contexts.time.time.Value;
            var timelineStack = _contexts.time.timeLineStack.Value;

            if (!(timelineStack.Peek(player) is PlayerTimelineData lastElement))
                return;

            _contexts.time.isTimelineLastPosition = true;
            _contexts.time.timelineLastPositionEntity.ReplacePlayerTimelineData(lastElement);
            var transformData = timelineStack.Pop(player, time) as PlayerTimelineData;
            
            _contexts.time.isTimelineRewindPosition = true;
            _contexts.time.timelineRewindPositionEntity.ReplacePlayerTimelineData(transformData);
        }
    }
}