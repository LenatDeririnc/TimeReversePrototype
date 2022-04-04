using System.Collections.Generic;
using Common;
using Entitas;

namespace ECS.Systems.TimeManagement
{
    public class TimelineSystem : IInitializeSystem
    {
        private readonly Contexts _contexts;

        public TimelineSystem(Contexts contexts)
        {
            _contexts = contexts;
        }
        
        public void Initialize()
        {
            var transform = new TransformInfo(_contexts.game.playerEntity.transform.Value);
            _contexts.time.SetTimelineData(new Stack<TransformInfo>());
            _contexts.time.SetTimelineLastPosition(transform);

            var timeManager = _contexts.time.timeManagerHandlerEntity.timeManagerHandler;
            timeManager.Value.OnTickAdd += AddPlayerInfo;
            timeManager.Value.OnTickRemove += ExecuteLastPlayerInfo;
        }

        private void AddPlayerInfo()
        {
            var data = new TransformInfo(_contexts.game.playerEntity.transform.Value);
            _contexts.time.ReplaceTimelineLastPosition(data);
            _contexts.time.timelineDataEntity.timelineData.Value.Push(data);
        }

        private void ExecuteLastPlayerInfo()
        {
            var timelineData = _contexts.time.timelineDataEntity.timelineData.Value;
            
            if (timelineData.Count <= 0)
                return;
            
            _contexts.time.ReplaceTimelineLastPosition(new TransformInfo(_contexts.game.playerEntity.transform.Value));
            TransformInfo transformData = timelineData.Pop();
            _contexts.time.ReplaceRewindPosition(transformData);
        }
    }
}