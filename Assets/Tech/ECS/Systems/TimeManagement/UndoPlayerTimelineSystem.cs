using System.Collections.Generic;
using Common;
using Entitas;
using TimelineData;
using Tools.TimeLineStackTool;

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
            var timelineData = _contexts.time.playerTimelineData.Value;

            if (!(timelineData.Peek() is PlayerTimelineData lastElement))
                return;

            _contexts.time.ReplaceTimelineLastPosition(lastElement);
            var transformData = timelineData.Pop(time) as PlayerTimelineData;
            _contexts.time.ReplaceTimelineRewindPosition(transformData);
        }
    }
}